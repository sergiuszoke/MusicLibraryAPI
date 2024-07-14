using MusicLibraryDAL.Models;

namespace MusicLibraryDAL.Interfaces
{
    public interface IArtistRepository
    {
        Task AddArtistAsync(Artist artist);
        Task<List<Artist>> GetAllArtistsAsync();
        Task UpdateArtistAsync(Artist artistToUpdate);
        Task DeleteArtistByIdAsync(int artistId);
        Task<bool> ExistsByArtistIdAsync(int artistId);
        Task<bool> IsArtistUniqueAsync(Artist artistToCheck);
    }
}
