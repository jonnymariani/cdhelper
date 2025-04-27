using Xabbo.Messages;

namespace CDHelper.Models
{

    public class RoomReady : IParser<RoomReady>
    {
        public string Model { get; set; }
        public int Id { get; set; }
      

        public static RoomReady Parse(in PacketReader reader)
        {
            return new RoomReady
            {
                Model = reader.ReadString(),
                Id = reader.ReadInt(),
               
            };
        }
    }
}