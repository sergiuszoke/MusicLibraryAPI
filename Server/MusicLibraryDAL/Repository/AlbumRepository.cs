using Microsoft.EntityFrameworkCore;
using MusicLibraryDAL.Interfaces;
using MusicLibraryDAL.Models;

namespace MusicLibraryDAL.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly string _exceptionMessage = "Album repository exception";

        private readonly DataContext _dataContext;

        public AlbumRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddAlbumAsync(Album album)
        {
            try
            {
                await _dataContext.AddAsync(album);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(_exceptionMessage, ex);
            }
        }

        public async Task<List<Album>> GetAllAlbumsFromArtistAsync(int artistId)
        {
            try
            {
                return await _dataContext.Albums
                    .Where(album => album.ArtistId == artistId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(_exceptionMessage, ex);
            }
        }

        public async Task UpdateAlbumAsync(Album albumToUpdate)
        {
            try
            {
                var currentAlbum = await GetAlbumByIdAsync(albumToUpdate.Id);

                currentAlbum.Title = albumToUpdate.Title;
                currentAlbum.Description = albumToUpdate.Description;
                currentAlbum.ArtistId = albumToUpdate.ArtistId;

                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(_exceptionMessage, ex);
            }
        }

        public async Task DeleteAlbumByIdAsync(int albumId)
        {
            try
            { 
                var album = await GetAlbumByIdAsync(albumId);

                _dataContext.Albums.Remove(album);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(_exceptionMessage, ex);
            }
        }

        public async Task<bool> ExistsByAlbumIdAsync(int albumId)
        {
            try
            {
                return await _dataContext.Albums
                    .AnyAsync(album => album.Id == albumId);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(_exceptionMessage, ex);
            }
        }

        public async Task<bool> IsAlbumUniqueAsync(Album albumToCheck)
        {
            try
            {
                return !await _dataContext.Albums
                    .AnyAsync(album => album.Title == albumToCheck.Title && 
                              album.ArtistId == albumToCheck.ArtistId);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(_exceptionMessage, ex);
            }
        }

        private async Task<Album> GetAlbumByIdAsync(int albumId)
        {
            return await _dataContext.Albums
                .Where(album => album.Id == albumId)
                .FirstOrDefaultAsync();
        }
    }
}
