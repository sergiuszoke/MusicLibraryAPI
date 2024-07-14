using MusicLibraryBLL.DTOs;

namespace MusicLibraryBLL.Interfaces
{
    public interface ISongService
    {
        Task AddSongAsync(CreateSongDTO song);
        Task<List<SongDTO>> GetAllSongsFromAlbumAsync(int albumId);
        Task UpdateSongAsync(SongDTO song);
        Task DeleteSongByIdAsync(int songId);
    }
}
