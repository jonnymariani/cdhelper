using CDHelper.Interceptors;
using CDHelper.Services;
using Xabbo.GEarth;

namespace CDHelper
{
    public class CDHelperExtension
    {
        private readonly GEarthExtension _extension;
        private readonly GEarthOptions _options;
        private readonly NotificationService _notificationService;
        private readonly PacketInterceptor _packetInterceptor;

        public CDHelperExtension()
        {
            _options = new GEarthOptions
            {
                Name = "CD Helper",
                Description = "A handy extension for Habbo that adds useful tools to manage your CDs in-game!",
                Author = "jonny7k",
                Version = "1.0"
            };

            _extension = new GEarthExtension(_options);

            _notificationService = new NotificationService(_extension);

            // Instancia e inicializa o PacketInterceptor
            _packetInterceptor = new PacketInterceptor(_extension, _notificationService);

        }
      
        public void Run()
        {
            _extension.Run();
        }
    }
}