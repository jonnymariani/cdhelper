namespace CDHelper.Models.PacketData
{
    public class CdData
    {
        public string Name { get; set; }
        public string User { get; set; }

        public CdData(string name, string user)
        {
            Name = name;
            User = user;
        }

        public CdData(TraxSongData info)
        {
            Name = info.CdName ?? string.Empty;
            User = info.User ?? string.Empty;
        }
    }
}
