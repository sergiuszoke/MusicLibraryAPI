using MusicLibraryBLL.DTOs;

namespace MusicLibraryBLL.Interfaces
{
    public interface IArtistService
    {
        Task AddArtistAsync(CreateArtistDTO artist);
        Task<List<ArtistDTO>> GetAllArtistsAsync();
        Task UpdateArtistAsync(ArtistDTO artist);
        Task DeleteArtistByIdAsync(int artistId);
    }
}
