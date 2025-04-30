using Xabbo;
using Xabbo.Messages;

namespace CDHelper.Models
{
    /// <summary>
    /// Represents the data for a marketplace offer
    /// Representa os dados de uma oferta do marketplace
    /// </summary>
    public class TraxSongInfo : IParser<TraxSongInfo>
    {
        public Id Id { get; set; }
        public string? a { get; set; }
        public string? CdName { get; set; }
        public string? b { get; set; }
        public int c { get; set; }
        public string? User { get; set; }
      

        /// <summary>
        /// Parses the OfferData from a PacketReader
        /// Analisa o OfferData a partir de um PacketReader
        /// </summary>
        /// <param name="reader">The PacketReader to read from.</param>
        /// <returns>An instance of OfferData</returns>
        public static TraxSongInfo Parse(in PacketReader reader)
        {
            return new TraxSongInfo
            {
                Id = reader.ReadId(),
                a = reader.ReadString(),
                CdName = reader.ReadString(),
                b = reader.ReadString(),
                c = reader.ReadInt(),
                User = reader.ReadString()
            };
        }
    }
}