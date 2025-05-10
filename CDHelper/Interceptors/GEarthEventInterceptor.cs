using CDHelper.Resources;
using CDHelper.Services;
using CDHelper.Structs;
using CDHelper.Utils;
using Xabbo;
using Xabbo.Core.GameData;

namespace CDHelper.Interceptors
{
    public static class GEarthEventInterceptor
    {
        /// <summary>
        /// Handles the extension initialized event
        /// Manipula o evento de inicializacao da extensao
        /// </summary>
        public static void OnExtensionInitialized(InitializedEventArgs e)
        {
            Console.WriteLine("Extension initialized.");
            ConfigManager.Load();
        }

        /// <summary>
        /// Handles the game connected event
        /// Manipula o evento de conexao
        /// </summary>
        public static async Task OnGameConnected(ConnectedEventArgs e, GameDataManager gameDataManager)
        {
            Console.WriteLine($"Loading game data for hotel: {e.Session.Hotel}...");

            try
            {
                // Load game data for the current hotel.
                await gameDataManager.LoadAsync(e.Session.Hotel);

                //Load language

                string? lang = ConfigManager.Get<string>(ConfigKeys.Language, string.Empty);
                LanguageHelper.SetLanguage(string.IsNullOrEmpty(lang) ? e.Session.Hotel.Domain : lang);

                CatalogMusicData.SetData(e.Session.Hotel.Domain);

                Console.WriteLine($"Loaded {gameDataManager.Furni?.Count ?? 0} furni info");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load game data: {ex.Message}");
            }

            Console.WriteLine($"Game connected. {e.Session}");
        }

        /// <summary>
        /// Handles the game disconnected event
        /// Manipula o evento de desconexao
        /// </summary>
        public static void OnGameDisconnected()
        {
            Console.WriteLine("Game disconnected.");
        }

        /// <summary>
        /// Handles the extension activated event
        /// Manipula o evento de ativacao da extensao
        /// </summary>
        public static void OnExtensionActivated(NotificationService notificationService)
        {
            Console.WriteLine("Extension activated!");

            notificationService.SendToastNotification($"{LanguageHelper.Get(Messages.LoadedSuccessfully)}!", NotificationBadges.Loaded);

            //Config
            bool AutoSearchEnabled = ConfigManager.Get<bool>(ConfigKeys.AutoSearchEnabled);

            if (AutoSearchEnabled)
            {
                notificationService.SendToastNotification(LanguageHelper.Get(Messages.AutoSearchEnabled), NotificationBadges.Alert);
            }




        }
    }
}
