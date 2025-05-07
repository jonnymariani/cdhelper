using CDHelper.Resources;
using CDHelper.Services;
using CDHelper.Structs;
using CDHelper.Utils;
using Xabbo;

namespace CDHelper.Handlers
{
    public class ChatCommandHandler
    {
        private readonly NotificationService _notificationService;
        private readonly JukeboxService _jukeboxService;
        private readonly ExportService _exportService;

        public ChatCommandHandler(NotificationService notificationService, JukeboxService jukeboxService, ExportService exportService)
        {
            _notificationService = notificationService;
            _jukeboxService = jukeboxService;
            _exportService = exportService;
        }

        //Intercepts and processes chat messages if they are commands
        //Intercepta e processa mensagens do chat, caso forem comandos
        public void HandleChatPacket(Intercept e)
        {
            string message = e.Packet.Read<string>().ToLower();

            if (!message.StartsWith(Commands.PreFix))
                return;

            e.Block();

            // Splits the command into parts separated by space
            // Divide o comando em partes separadas por espaco
            var parts = message.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2)
            {
                _notificationService.SendToastNotification(
                    $"{LanguageHelper.Get(Messages.Usage)}: {Commands.PreFix} {Commands.Help}",
                    NotificationBadges.Alert
                );

                return;
            }

            string arg1 = parts[1].ToLower();

            switch (arg1)
            {
                //Lists CDs from the jukebox
                //Lista CD's do jukebox
                case Commands.GetJukeboxCds:
                    _ = _jukeboxService.GetJukeboxCdsAsync();
                    break;

                //Fetches info from the marketplace
                //Busca info do marketplace
                case Commands.GetCdInfoFromMarket:
                    _ = _jukeboxService.GetMarketCDDataAsync();
                    break;

                //Displays the help screen
                //Mostra a tela de ajuda
                case Commands.Help:
                    _notificationService.SendHelpNotification();
                    break;

                //Exports a list of CDs to a file
                //Exporta lista de CD's para um arquivo
                case Commands.Export:
                    {
                        string arg2 = parts.Length > 2 ? parts[2] : ExportSuffix.All;

                        switch (arg2)
                        {
                            // Exports everything (room + inventory + jukebox)
                            // Exporta tudo (quarto + inventario + jukebox)
                            case ExportSuffix.All:
                                _ = _exportService.ExportAll();
                                break;

                            //Exports only from the room
                            //Exporta apenas do quarto
                            case ExportSuffix.Room:
                                _exportService.ExportRoom();
                                break;

                            //Exports only from the inventory
                            //Exporta apenas do inventario
                            case ExportSuffix.Inventory:
                                _ = _exportService.ExportInventory();
                                break;

                            //Exports only from the jukebox
                            //Exporta apenas do jukebox
                            case ExportSuffix.Jukebox:
                                _ = _exportService.ExportJukebox();
                                break;

                            // Argumento invalido
                            // Invalid argument
                            default:
                                _notificationService.SendToastNotification(
                                    $"{LanguageHelper.Get(Messages.UnknownCommand, $"{Commands.PreFix} {arg1} {arg2}", $"{Commands.PreFix} {Commands.Help}")}",
                                    NotificationBadges.Alert
                                );
                                break;
                        }

                        break;
                    }

                default:
                    _notificationService.SendToastNotification(
                        LanguageHelper.Get(Messages.UnknownCommand, arg1, $"{Commands.PreFix} {Commands.Help}"),                      
                        NotificationBadges.Alert
                    );
                    break;
            }

        }



    }
}
