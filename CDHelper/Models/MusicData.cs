namespace CDHelper.Models
{
    public class MusicData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public MusicData(int id, string title, string author)
        {
            Id = id;
            Title = title;
            Author = author;
        }

        public MusicData()
        {
        }
    }
}
