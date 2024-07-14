using Microsoft.EntityFrameworkCore;
using MusicLibraryDAL.Interfaces;
using MusicLibraryDAL.Models;

namespace MusicLibraryDAL.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly string _exceptionMessage = "Artist repository exception";

        private readonly DataContext _dataContext;

        public ArtistRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddArtistAsync(Artist artist)
        {
            try
            {
                await _dataContext.AddAsync(artist);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(_exceptionMessage, ex);
            }
        }

        public async Task<List<Artist>> GetAllArtistsAsync()
        {
            try
            {
                return await _dataContext.Artists.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(_exceptionMessage, ex);
            }
        }

        public async Task UpdateArtistAsync(Artist artistToUpdate)
        {
            try
            {
                var currentArtist = await GetArtistByIdAsync(artistToUpdate.Id);

                currentArtist.Name = artistToUpdate.Name;

                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(_exceptionMessage, ex);
            }
        }

        public async Task DeleteArtistByIdAsync(int artistId)
        {
            try
            {
                var artist = await GetArtistByIdAsync(artistId);

                _dataContext.Artists.Remove(artist);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(_exceptionMessage, ex);
            }
        }

        public async Task<bool> ExistsByArtistIdAsync(int artistId)
        {
            try
            {
                return await _dataContext.Artists
                    .AnyAsync(artist => artist.Id == artistId);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(_exceptionMessage, ex);
            }
        }

        public async Task<bool> IsArtistUniqueAsync(Artist artistToCheck)
        {
            try
            {
                return !await _dataContext.Artists
                    .AnyAsync(artist => artist.Name == artistToCheck.Name);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(_exceptionMessage, ex);
            }
        }

        private async Task<Artist> GetArtistByIdAsync(int artistId)
        {
            return await _dataContext.Artists
                .Where(artist => artist.Id == artistId)
                .FirstOrDefaultAsync();
        }
    }
}
