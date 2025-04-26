using Xabbo.Messages;

namespace CDHelper
{
    /// <summary>
    /// Represents the data for a marketplace offer
    /// Representa os dados de uma oferta do marketplace
    /// </summary>
    public class OfferData : IParser<OfferData>
    {
        public int Z { get; set; }
        public int OfferIdPacket { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        public int FurniId { get; set; }
        public int C { get; set; }
        public string Name { get; set; }
        public int D { get; set; }
        public int E { get; set; }
        public int F { get; set; }
        public int G { get; set; }
        public int H { get; set; }

        /// <summary>
        /// Parses the OfferData from a PacketReader
        /// Analisa o OfferData a partir de um PacketReader
        /// </summary>
        /// <param name="reader">The PacketReader to read from.</param>
        /// <returns>An instance of OfferData</returns>
        public static OfferData Parse(in PacketReader reader)
        {
            return new OfferData
            {
                Z = reader.ReadInt(),
                OfferIdPacket = reader.ReadInt(),
                A = reader.ReadInt(),
                B = reader.ReadInt(),
                FurniId = reader.ReadInt(),
                C = reader.ReadInt(),
                Name = reader.ReadString(),
                D = reader.ReadInt(),
                E = reader.ReadInt(),
                F = reader.ReadInt(),
                G = reader.ReadInt(),
                H = reader.ReadInt()
            };
        }
    }
}