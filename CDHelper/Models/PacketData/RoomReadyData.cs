using Xabbo.Messages;

namespace CDHelper.Models.PacketData
{
    public class RoomReadyData : IParser<RoomReadyData>
    {
        public string? Model { get; set; }
        public int Id { get; set; }

        public static RoomReadyData Parse(in PacketReader reader)
        {
            return new RoomReadyData
            {
                Model = reader.ReadString(),
                Id = reader.ReadInt(),
            };
        }
    }
}