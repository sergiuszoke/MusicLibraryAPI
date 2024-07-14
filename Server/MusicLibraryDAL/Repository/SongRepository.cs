using Microsoft.EntityFrameworkCore;
using MusicLibraryDAL.Interfaces;
using MusicLibraryDAL.Models;

namespace MusicLibraryDAL.Repository
{
    public class SongRepository : ISongRepository
    {
        private readonly string _exceptionMessage = "Song repository exception";

        private readonly DataContext _dataContext;

        public SongRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddSongAsync(Song song)
        {
            try
            {
                await _dataContext.AddAsync(song);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(_exceptionMessage, ex);
            }
        }

        public async Task<List<Song>> GetAllSongsFromAlbumAsync(int albumId)
        {
            try
            {
                return await _dataContext.Songs
                    .Where(song => song.AlbumId == albumId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(_exceptionMessage, ex);
            }
        }

        public async Task UpdateSongAsync(Song songToUpdate)
        {
            try
            {
                var currentSong = await GetSongByIdAsnyc(songToUpdate.Id);

                currentSong.Title = songToUpdate.Title;
                currentSong.Length = songToUpdate.Length;
                currentSong.AlbumId = songToUpdate.AlbumId;

                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(_exceptionMessage, ex);
            }
        }

        public async Task DeleteSongByIdAsync(int songId)
        {
            try
            {
                var song = await GetSongByIdAsnyc(songId);

                _dataContext.Songs.Remove(song);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(_exceptionMessage, ex);
            }
        }

        public async Task<bool> ExistsBySongIdAsync(int songId)
        {
            try
            {
                return await _dataContext.Songs
                    .AnyAsync(song => song.Id == songId);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(_exceptionMessage, ex);
            }
        }

        public async Task<bool> IsSongUniqueAsync(Song songToCheck)
        {
            try
            {
                return !await _dataContext.Songs
                    .AnyAsync(song => song.Title == songToCheck.Title && 
                              song.AlbumId == songToCheck.AlbumId);
            }
            catch (Exception ex)
            {
                throw new RepositoryException(_exceptionMessage, ex);
            }
        }

        private async Task<Song> GetSongByIdAsnyc(int songId)
        {
            return await _dataContext.Songs
                .Where(song => song.Id == songId)
                .FirstOrDefaultAsync();
        }
    }
}
