using CDHelper.Coordinators;
using CDHelper.Models.PacketData;
using Xabbo;

namespace CDHelper.Handlers
{
    public class RoomPacketHandler
    {
        /// <summary>
        /// Sets the current room ID  
        /// Define o ID do quarto atual
        /// </summary>
        public void HandleRoomReadyPacket(Intercept e)
        {
            // Reads the room data from the packet  
            // Le os dados do quarto a partir do pacote
            RoomReadyData roomData = e.Packet.Read<RoomReadyData>();

            // Sets the current room ID in the coordinator  
            // Define o ID do quarto atual no coordenador
            RoomCdCoordinator.SetCurrentRoomId(roomData.Id);
        }

    }
}
