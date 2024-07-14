using MusicLibraryDAL.Models;

namespace MusicLibraryDAL.Interfaces
{
    public interface ISongRepository
    {
        Task AddSongAsync(Song song);
        Task<List<Song>> GetAllSongsFromAlbumAsync(int albumId);
        Task UpdateSongAsync(Song songToUpdate);
        Task DeleteSongByIdAsync(int songId);
        Task<bool> ExistsBySongId(int songId);
        Task<bool> IsSongUnique(Song songToCheck);
    }
}
