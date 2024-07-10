namespace MusicLibraryDAL.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Song>? Songs { get; set; }
        public Artist Artist { get; set; }
        public int ArtistId { get; set; }
    }
}
