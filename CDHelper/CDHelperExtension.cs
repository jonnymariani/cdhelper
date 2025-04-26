using CDHelper.Structs;
using Xabbo;
using Xabbo.GEarth;
using Xabbo.Messages.Flash;

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

        /// <summary>
        /// Subscribes to g-earth events
        /// Assina os eventos do g-earth
        /// </summary>
        private void SubscribeToEvents()
        {
            _extension.Initialized += OnExtensionInitialized;
            _extension.Connected += OnGameConnected;
            _extension.Disconnected += OnGameDisconnected;
            _extension.Activated += OnExtensionActivated;
            _extension.Intercept(Out.Sign, OnSignPacketIntercepted);
        }

        /// <summary>
        /// Handles the extension initialized event
        /// Manipula o evento de inicializacao da extensao
        /// </summary>
        private void OnExtensionInitialized(InitializedEventArgs e)
        {
            Console.WriteLine("Extension initialized.");
        }

        /// <summary>
        /// Handles the game connected event
        /// Manipula o evento de conexao
        /// </summary>
        private void OnGameConnected(ConnectedEventArgs e)
        {
            Console.WriteLine($"Game connected. {e.Session}");
        }

        /// <summary>
        /// Handles the game disconnected event
        /// Manipula o evento de desconexao
        /// </summary>
        private void OnGameDisconnected()
        {
            Console.WriteLine("Game disconnected.");
        }

        /// <summary>
        /// Handles the extension activated event
        /// Manipula o evento de ativacao da extensao
        /// </summary>
        private void OnExtensionActivated()
        {
            Console.WriteLine("MarketplaceDiskPreview extension activated!");

            _notificationHandler.SendNotification($"{_options.Name} successfully loaded.", NotificationBadges.Loaded);
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
            OfferData offer = OfferData.Parse(packetArgs.Reader());

            // Extract user and cd name
            // Extrai o nome do usuario e do cd
            string[] nameParts = offer.Name.Split('\n');
            string user = nameParts.First().Trim();
            string cdName = nameParts.Last().Trim();

            // Create the notification message
            // Cria a mensagem de notificação
            string notification = $"{cdName}\n\nOffer by {user}";

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