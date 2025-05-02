using CDHelper.Services;
using CDHelper.Structs;
using Xabbo;

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
        }

        /// <summary>
        /// Handles the game connected event
        /// Manipula o evento de conexao
        /// </summary>
        public static void OnGameConnected(ConnectedEventArgs e)
        {
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
        public static void OnExtensionActivated(NotificationService notificationHandler)
        {
            Console.WriteLine("Extension activated!");

            notificationHandler.SendNotification($"CD HELPER successfully loaded.", NotificationBadges.Loaded);
        }
    }
}
