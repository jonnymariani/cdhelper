namespace CDHelper.Models.PacketData
{
    public class CdData
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int SongId { get; set; }
        public string? SongData { get; set; }

        public CdData(string title, string author)
        {
            Title = title;
            Author = author;
        }

        public CdData(TraxSongData info)
        {
            Title = info.Title ?? string.Empty;
            Author = info.Author ?? string.Empty;
            SongData = info.b;
            SongId = (int)info.Id;
        }
    }
}
