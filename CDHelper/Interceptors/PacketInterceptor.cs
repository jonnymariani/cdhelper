using System.Text;
using CDHelper.Coordinators;
using CDHelper.Models.PacketData;
using CDHelper.Services;
using CDHelper.Structs;
using Xabbo;
using Xabbo.GEarth;
using Xabbo.Messages;
using Xabbo.Messages.Flash;

namespace CDHelper.Interceptors
{
    public class PacketInterceptor
    {
        private readonly GEarthExtension _extension;
        private readonly NotificationService _notificationService;

        public PacketInterceptor(GEarthExtension extension, NotificationService notificationService)
        {
            _extension = extension;
            _notificationService = notificationService;

            // Subscribe to specific packet interceptions
            // Inscreve-se nas interceptações de pacotes específicas
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _extension.Initialized += GEarthEventInterceptor.OnExtensionInitialized;
            _extension.Connected += GEarthEventInterceptor.OnGameConnected;
            _extension.Disconnected += GEarthEventInterceptor.OnGameDisconnected;
            _extension.Activated += () => GEarthEventInterceptor.OnExtensionActivated(_notificationService);

            _extension.Intercept(Out.Chat, OnChatPacketIntercepted);

            _extension.Intercept(In.RoomReady, OnRoomReadyPacketIntercepted);

        }
        private void OnChatPacketIntercepted(Intercept e)
        {
            string message = e.Packet.Read<string>();

            message = message.ToLower();

            if (message.StartsWith(Commands.PreFix))
            {
                e.Block();

                // Divide o comando em partes separadas por espaço
                // Splits the command into parts separated by space
                var parts = message.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length >= 1)
                {
                    string arg1 = parts[1].ToLower();

                    switch (arg1)
                    {
                        case Commands.GetJukeboxCds:
                            _ = GetTraxSongInfoDataAsync();
                            break;

                        case Commands.GetCdInfoFromMarket:
                            _ = GetMarketCDDataAsync();
                            break;

                        case Commands.Help:
                            _ = DisplayHelpAsync();
                            break;

                        case Commands.Export:
                            {
                                string arg2 = parts.Length > 2 ? parts[2] : ExportSuffix.All;

                                switch (arg2)
                                {
                                    case ExportSuffix.All:
                                        // exporta tudo
                                        break;

                                    case ExportSuffix.Room:
                                        // exporta apenas room
                                        break;

                                    case ExportSuffix.Inventory:
                                        // exporta apenas inventario
                                        break;

                                    case ExportSuffix.Jukebox:
                                        // exporta apenas jukebox
                                        break;

                                    default:
                                        // argumento inválido, pode notificar se quiser
                                        break;
                                }

                                break;
                            }

                        default:
                            _notificationService.SendNotification($"Unknown command: {arg1}. Try: :cdhelper help", NotificationBadges.NotFound);
                            break;
                    }
                }
                else
                {
                    _notificationService.SendNotification("Usage: :cdhelper help", NotificationBadges.NotFound);
                }


            }
        }

        /// <summary>
        /// Retrieves and processes marketplace cds offer data
        /// Recupera e processa os dados de ofertas de cds do marketplace
        /// </summary>
        private async Task GetMarketCDDataAsync()
        {
            _extension.Send(Out.GetMarketplaceOffers, -1, -1, "Epic Flail", 1);

            // Capture the marketplace offers packet
            // Captura o pacote de ofertas do marketplace
            var packetArgs = await _extension.ReceiveAsync(In.MarketPlaceOffers);
            Console.WriteLine("Received marketplace data.");

            // Parse the offer data from the packet
            // Converte os dados da oferta do pacote
            OfferData offer = packetArgs.Read<OfferData>();

            // Extract user and cd name
            // Extrai o nome do usuario e do cd
            string[] nameParts = offer.Name.Split('\n');
            string user = nameParts.First().Trim();
            string cdName = nameParts.Last().Trim();

            CdData cdData = new CdData(cdName, user);

            // Create the notification message
            // Cria a mensagem de notificação
            string notification = $"{cdData.Name}\n\nOffer by {cdData.User}";

            _notificationService.SendNotification(notification, NotificationBadges.CdName);
        }


        /// <summary>
        /// Retrieves and processes the CDs data from the room's jukebox
        /// Recupera e processa os dados de CDs do jukebox do quarto
        /// </summary>
        private async Task GetTraxSongInfoDataAsync()
        {
            // Requests the jukebox playlist
            // Solicita a lista de músicas do jukebox

            _extension.Send(Out.GetJukeboxPlayList);

            IPacket? packetArgs = null;

            // If it times out, it means the packet did not return
            // It seems that it only returns once per room, then it stays in some kind of cache

            // Se der timeout significa que o pacote nao retornou
            // Parece que só retorna uma vez por quarto depois fica em algum tipo de cache

            try
            {
                // Waits for the packet with song information
                // Espera o pacote com as informacoes da musica
                packetArgs = await _extension.ReceiveAsync(In.TraxSongInfo, 2000);

                if (packetArgs is not null)
                {
                    // Adds the CDs to the room coordinator
                    // Adiciona os CDs no coordenador do quarto

                    TraxSongData[] jukeInfo = packetArgs.Read<TraxSongData[]>();

                    var data = jukeInfo.Select(x => new CdData(x));
                    RoomCdCoordinator.AddCdsToRoom(data);
                }
            }
            catch (Exception)
            {
            }

            // Retrieves the CDs from the current room
            // Obtém os CDs do quarto atual
            var cds = RoomCdCoordinator.GetCurrentRoomCds();

            string message;

            if (cds?.Any() == true) // Checks if there are any CDs
            {
                // Builds the message with the list of found CDs
                // Monta a mensagem com a lista de CDs encontrados
                var messageBuilder = new StringBuilder("\n");

                foreach (var cd in cds)
                {
                    // Add each CD to the message with proper formatting
                    // Adiciona cada CD à mensagem com a formatação adequada
                    messageBuilder.AppendLine($"\t<b>{cd.Name}{(string.IsNullOrEmpty(cd.User) ? "" : $" - {cd.User}")}</b>");
                }

                messageBuilder.Append("\n  ");

                message = messageBuilder.ToString(); // Converts the StringBuilder to a string

                // Sends the notification with the found CDs
                // Envia a notificação com os CDs encontrados
                _extension.Send(In.NotificationDialog, "", 3, "title", $"{cds.Count()} CD's Found!", "message", message, "image", NotificationBadges.Jukebox);
            }
            else
            {
                // Sends a notification if no CDs are found
                // Envia uma notificação caso nenhum CD seja encontrado
                _notificationService.SendNotification("No CD's found in the jukebox!", NotificationBadges.NotFound);
            }

        }

        /// <summary>
        /// Displays extension info
        /// Exibe informacoes da extensao
        /// </summary>
        private async Task DisplayHelpAsync()
        {
            string badge = NotificationBadges.NotFound;
            string title = "Info";

            string message = "\n";

            //message += "<b>CD Helper</b>\n";
            message += "<small>CD Helper is an extension to help you manage and discover CDs in Habbo more easily.</small>\n\n";

            message += "<b>Command List:</b>\n\n";

            message += $"<b>{Commands.GetJukeboxCds}</b> - Retrieves the list of CD's from the room's jukebox.\n\n";
            message += $"<b>{Commands.GetCdInfoFromMarket}</b> - Retrieves the name of the current CD from the marketplace.\n\n";
            message += $"<b>{Commands.Help}</b> - Uh... well... opens this very screen. What did you expect?\n\n";

            // Add section with generic usage example
            message += "<b>Example Usage:</b>\n\n";
            message += "To use the commands, type them in the chat as follows:\n\n";
            message += $"<i><b>{Commands.PreFix} command</b></i>\n";

            _extension.Send(In.NotificationDialog, "", 3, "title", title, "message", message, "image", badge);
        }


        // Sets the current room ID
        // Define o ID do quarto atual
        private void OnRoomReadyPacketIntercepted(Intercept e)
        {
            RoomReadyData roomData = e.Packet.Read<RoomReadyData>();

            RoomCdCoordinator.SetCurrentRoomId(roomData.Id);
        }

    }
}
