using CDHelper.Handlers;
using CDHelper.Interceptors;
using CDHelper.Services;
using CDHelper.Utils;
using Xabbo.Core.Game;
using Xabbo.Core.GameData;
using Xabbo.GEarth;

namespace CDHelper
{
    public class CDHelperExtension
    {
        private readonly GEarthExtension _extension;
        private readonly GEarthOptions _options;

        private readonly NotificationService _notificationService;
        private readonly GameDataManager _gameDataManager;

        private readonly RoomManager _roomManager;
        private readonly InventoryManager _inventoryManager;
        private readonly TradeManager _tradeManager;
        private readonly ProfileManager _profileManager;

        private readonly JukeboxService _jukeboxService;
        private readonly ExportService _exportService;
        private readonly RoomDataService _roomDataService;
        private readonly InventoryDataService _inventoryService;
        private readonly FurniHelper _furniHelper;

        private readonly ChatCommandHandler _chatHandler;
        private readonly RoomPacketHandler _roomPacketHandler;

        private readonly PacketInterceptor _packetInterceptor;

        public CDHelperExtension()
        {
            // Inicializando configurações do GEarth
            _options = new GEarthOptions
            {
                Name = "CD Helper",
                Description = "A handy extension for Habbo that adds useful tools to manage your CDs in-game!",
                Author = "jonny7k",
                Version = "1.0"
            };
            _extension = new GEarthExtension(_options);


            _notificationService = new NotificationService(_extension);
            _gameDataManager = new GameDataManager();

            _roomManager = new RoomManager(_extension);
            _profileManager = new ProfileManager(_extension);
            _tradeManager = new TradeManager(_extension, _profileManager, _roomManager);
            _inventoryManager = new InventoryManager(_extension);

            _jukeboxService = new JukeboxService(_extension, _notificationService);
            _furniHelper = new FurniHelper();
            _inventoryService = new InventoryDataService(_extension, _notificationService, _inventoryManager, _jukeboxService, _furniHelper);
            _roomDataService = new RoomDataService(_notificationService, _roomManager, _furniHelper, _jukeboxService);
            _exportService = new ExportService(_notificationService, _roomDataService, _jukeboxService, _inventoryService, _extension);

            _chatHandler = new ChatCommandHandler(_notificationService, _jukeboxService, _exportService);
            _roomPacketHandler = new RoomPacketHandler(_roomDataService);

            _packetInterceptor = new PacketInterceptor(_extension, _notificationService, _gameDataManager, _chatHandler, _roomPacketHandler);
        }

        public void Run()
        {
            _extension.Run();
        }
    }
}
