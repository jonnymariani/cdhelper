using CDHelper.Models.PacketData;

namespace CDHelper.Models
{
    public class RoomCdConfiguration
    {
        public RoomCdConfiguration()
        {
        }

        public int RoomId { get; set; }
        public IEnumerable<CdData> CDS { get; set; } = [];

        public RoomCdConfiguration(int roomId)
        {
            RoomId = roomId;
        }
    }
}
