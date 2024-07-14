using MusicLibraryDAL.Models;

namespace MusicLibraryDAL.Interfaces
{
    public interface IAlbumRepository
    {
        Task AddAlbumAsync(Album album);
        Task<List<Album>> GetAllAlbumsFromArtistAsync(int artistId);
        Task UpdateAlbumAsync(Album albumToUpdate);
        Task DeleteAlbumByIdAsync(int albumId);
        Task<bool> ExistsByAlbumIdAsync(int albumId);
        Task<bool> IsAlbumUniqueAsync(Album albumToCheck);
    }
}
