namespace MusicLibraryBLL.DTOs
{
    public class SongDTO
    {
        public int Id { get; set; }
        public string Title {  get; set; }
        public string Length { get; set; } 
        public int AlbumId { get; set; }
    }
}
