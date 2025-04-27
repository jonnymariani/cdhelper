namespace CDHelper.Models
{
    public class RoomCdData
    {
        public RoomCdData()
        {
        }

        public int RoomId { get; set; }
        public IEnumerable<CdData> CDS { get; set; } = [];

        public RoomCdData(int roomId)
        {
            RoomId = roomId;
        }
    }
}
