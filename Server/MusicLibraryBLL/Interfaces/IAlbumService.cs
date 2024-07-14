using MusicLibraryBLL.DTOs;

namespace MusicLibraryBLL.Interfaces
{
    public interface IAlbumService
    {
        Task AddAlbumAsync(CreateAlbumDTO album);
        Task<List<AlbumDTO>> GetAllAlbumsFromArtistAsync(int artistId);
        Task UpdateAlbumAsync(AlbumDTO album);
        Task DeleteAlbumByIdAsync(int albumId);
    }
}
