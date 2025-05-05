using Xabbo;
using Xabbo.Messages;

namespace CDHelper.Models.PacketData
{
    /// <summary>
    /// Represents the data for a marketplace offer
    /// Representa os dados de uma oferta do marketplace
    /// </summary>
    public class TraxSongData : IParser<TraxSongData>
    {
        public Id Id { get; set; }
        public string? a { get; set; }
        public string? Title { get; set; }
        public string? b { get; set; }
        public int c { get; set; }
        public string? Author { get; set; }
      

        /// <summary>
        /// Parses the OfferData from a PacketReader
        /// Analisa o OfferData a partir de um PacketReader
        /// </summary>
        /// <param name="reader">The PacketReader to read from.</param>
        /// <returns>An instance of OfferData</returns>
        public static TraxSongData Parse(in PacketReader reader)
        {
            return new TraxSongData
            {
                Id = reader.ReadId(),
                a = reader.ReadString(),
                Title = reader.ReadString(),
                b = reader.ReadString(),
                c = reader.ReadInt(),
                Author = reader.ReadString()
            };
        }
    }
}