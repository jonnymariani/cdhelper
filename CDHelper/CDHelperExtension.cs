using System;
using CDHelper.Helpers;
using CDHelper.Interceptors;
using CDHelper.Models;
using CDHelper.Structs;
using Xabbo;
using Xabbo.Core;
using Xabbo.Core.Messages.Outgoing;
using Xabbo.GEarth;
using Xabbo.Messages;
using Xabbo.Messages.Flash;
using static System.Net.Mime.MediaTypeNames;

namespace CDHelper
{
    public class CDHelperExtension
    {
        private readonly GEarthExtension _extension;
        private readonly GEarthOptions _options;
        private readonly NotificationHandler _notificationHandler;
        public CDHelperExtension()
        {
            _options = new GEarthOptions
            {
                Name = "CD Helper",
                Description = "A handy extension for Habbo that adds useful tools to manage your CDs in-game!",
                Author = "jonny7k",
                Version = "0.1"
            };

            _extension = new GEarthExtension(_options);

            _notificationHandler = new NotificationHandler(_extension);

            SubscribeToEvents();
        }

        public void SubscribeToEvents()
        {
            _extension.Initialized += GEarthEvents.OnExtensionInitialized;
            _extension.Connected += GEarthEvents.OnGameConnected;
            _extension.Disconnected += GEarthEvents.OnGameDisconnected;
            _extension.Activated += () => GEarthEvents.OnExtensionActivated(_notificationHandler);


            _extension.Intercept(Out.Sign, OnSignPacketIntercepted);

            _extension.Intercept(Out.Chat, OnChatPacketIntercepted);

            _extension.Intercept(In.RoomReady, OnRoomReadyPacketIntercepted);

        }




        private void OnChatPacketIntercepted(Intercept e)
        {
            string message = e.Packet.Read<string>();

            if (message == ":jukebox")
            {
                e.Block();

                _ = GetTraxSongInfoDataAsync();
            }
        }

        private void OnRoomReadyPacketIntercepted(Intercept e)
        {
            RoomReady roomData = e.Packet.Read<RoomReady>();

            RoomCdManager.SetCurrentRoomId(roomData.Id);
        }

        private async Task GetTraxSongInfoDataAsync()
        {

            _extension.Send(Out.GetJukeboxPlayList);

            IPacket? packetArgs = null;

            try
            {               
                packetArgs = await _extension.ReceiveAsync(In.TraxSongInfo,2000);

                if (packetArgs is not null)
                {
                    TraxSongInfo[] jukeInfo = packetArgs.Read<TraxSongInfo[]>();

                    var data = jukeInfo.Select(x => new CdData(x));
                    RoomCdManager.AddCdsToRoom(data);
                }
            }
            catch (Exception)
            {
                
            }

            var cds = RoomCdManager.GetCurrentRoomCds();

            if (cds is not null)
            {
                string msg = "\n";
                string title = string.Empty;
                title = "CD's Found!";

                foreach (var cd in cds)
                {
                    msg += $"\t<b>{cd.Name}{(string.IsNullOrEmpty(cd.User) ? "" : $" - {cd.User}")}</b>\n\n";
                }

                msg = msg.TrimEnd('\n');
                msg += "\n  ";

                _extension.Send(In.NotificationDialog, "", 3, "title", $"{cds.Count()} CD's Found!", "message", msg, "image", NotificationBadges.Jukebox);
            }
            else
            {
                _notificationHandler.SendNotification($"No CD's found in the jukebox!", NotificationBadges.NotFound);
            }


        }



        /// <summary>
        /// Intercepts 'Sign' packet to trigger marketplace offer retrieval
        /// Intercepta o pacote 'Sign' para acionar a recuperacao de ofertas da feira
        /// </summary>
        private void OnSignPacketIntercepted(Intercept e)
        {
            e.Block();
            int signNumber = e.Packet.Read<int>();

            // Trigger marketplace offer request when a specific sign is used 
            // Aciona a requisicao de ofertas do marketplace quando um sinal específico e usado
            if (signNumber == 1)
            {
                _extension.Send(Out.GetMarketplaceOffers, -1, -1, "Epic Flail", 1);
                _ = GetDiskDataAsync();
            }
        }

        /// <summary>
        /// Retrieves and processes marketplace cds offer data
        /// Recupera e processa os dados de ofertas de cds do marketplace
        /// </summary>
        private async Task GetDiskDataAsync()
        {
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

            _notificationHandler.SendNotification(notification, NotificationBadges.CdName);
        }


        /// <summary>
        /// Starts the extension
        /// Inicia a extensao
        /// </summary>
        public void Run()
        {
            _extension.Run();
        }
    }
}