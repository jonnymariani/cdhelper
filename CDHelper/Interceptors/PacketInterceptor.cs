using CDHelper.Handlers;
using CDHelper.Services;
using Xabbo;
using Xabbo.Core.GameData;
using Xabbo.GEarth;
using Xabbo.Messages.Flash;

namespace CDHelper.Interceptors
{
    public class PacketInterceptor
    {
        private readonly GEarthExtension _extension;
        private readonly NotificationService _notificationService;
        private readonly GameDataManager _gameDataManager;

        private readonly ChatCommandHandler _chatHandler;
        private readonly RoomPacketHandler _roomPacketHandler;

        public PacketInterceptor(GEarthExtension extension, NotificationService notificationService, GameDataManager gameDataManager, ChatCommandHandler chatHandler, RoomPacketHandler roomPacketHandler)
        {
            _extension = extension;
            _notificationService = notificationService;
            _gameDataManager = gameDataManager;
            _chatHandler = chatHandler;
            _roomPacketHandler = roomPacketHandler;

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _extension.Initialized += GEarthEventInterceptor.OnExtensionInitialized;
            _extension.Disconnected += GEarthEventInterceptor.OnGameDisconnected;
            _extension.Activated += () => GEarthEventInterceptor.OnExtensionActivated(_notificationService);
            _extension.Connected += async (e) => await GEarthEventInterceptor.OnGameConnected(e, _gameDataManager);

            _extension.Intercept(Out.Chat, _chatHandler.HandleChatPacket);
            _extension.Intercept(In.RoomReady, _roomPacketHandler.HandleRoomReadyPacket);

            //Using this packet as a trigger after the room has been loaded
            //Usando este pacote como gatilho apos o quarto ser carregado
            _extension.Intercept(In.RoomRating, _roomPacketHandler.HandleRoomEntryInfoPacket);
        }
    }
}
