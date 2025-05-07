using CDHelper.Models.PacketData;
using CDHelper.Resources;
using CDHelper.Structs;
using CDHelper.Utils;
using Xabbo.Core;
using Xabbo.Core.Game;

namespace CDHelper.Services
{
    public class RoomDataService
    {
        private readonly NotificationService _notificationService;
        private readonly RoomManager _roomManager;
        private readonly FurniHelper _furniHelper;

        public RoomDataService(NotificationService notificationService, RoomManager roomManager, FurniHelper furniHelper)
        {
            _notificationService = notificationService;
            _roomManager = roomManager;
            _furniHelper = furniHelper;
        }

        /// <summary>
        /// Retrieves the list of CDs from the current room  
        /// Obtém a lista de CDs do quarto atual  
        /// </summary>
        public List<CdData>? GetRoomCds()
        {
            // Ensures that the user is in a room  
            // Garante que o usuario esteja em um quarto  
            if (!_roomManager.EnsureInRoom(out var room))
            {
                _notificationService.SendToastNotification(Messages.RoomNotBeingTracked, NotificationBadges.Error);
                return null;
            }

            // Filters the furni to get only the CDs
            // Filtra os furnis para pegar apenas os CDs
            IFurni[] cdFurni = room.Furni
                .Where(x => x.Kind == FurniIds.CD)
                .ToArray();

            // Retrieves the CD data from the furni  
            // Recupera os dados dos CDs a partir dos furnis 
            List<CdData> list = _furniHelper.GetCdData(cdFurni);

            return list;
        }
        
    }
}
