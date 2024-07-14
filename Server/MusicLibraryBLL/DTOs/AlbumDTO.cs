using MusicLibraryDAL.Models;

namespace MusicLibraryBLL.DTOs
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ArtistId { get; set; }
    }
}
