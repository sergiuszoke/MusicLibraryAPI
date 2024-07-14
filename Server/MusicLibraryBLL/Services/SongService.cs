using AutoMapper;
using MusicLibraryBLL.DTOs;
using MusicLibraryBLL.Interfaces;
using MusicLibraryDAL.Interfaces;
using MusicLibraryDAL.Models;

namespace MusicLibraryBLL.Services
{
    public class SongService : ISongService
    {
        private readonly string _songServiceException = "Song service exception. "; 
        private readonly string _uniqueSongErrorMessage = "Song already exists.";
        private readonly string _invalidSongErrorMessage = "Song does not exist.";
        private readonly string _invalidLengthErrorMessage = "Invalid lenght.";
        private readonly string _invalidSongTitleErrorMessage = "Song title cannot be null or empty.";
        private readonly string _invalidSongLengthErrorMessage = "Song length cannot be null or empty.";
        private readonly string _lengthFormatErrorMessage = "Invalid length format. Accepted formats are hh:mm:ss and mm:ss.";

        private readonly ISongRepository _songRepository;
        private readonly IMapper _mapper;

        public SongService(ISongRepository songRepository, IMapper mapper) 
        {
            _songRepository = songRepository;
            _mapper = mapper;
        }

        public async Task AddSongAsync(CreateSongDTO song)
        {
            // TODO Verify if album exists
            Song newSong = _mapper.Map<Song>(song);

            var message = await ValidateSongAsync(newSong);
            if (message is not null)
            {
                throw new ServiceException(_songServiceException + message);
            }

            await _songRepository.AddSongAsync(newSong);
        }

        public async Task<List<SongDTO>> GetAllSongsFromAlbumAsync(int albumId)
        {
            // TODO Verify if album exists

            var songList = await _songRepository.GetAllSongsFromAlbumAsync(albumId);

            return songList
                .Select(song => _mapper.Map<SongDTO>(song))
                .ToList();
        }

        public async Task UpdateSongAsync(SongDTO song)
        {
            // TODO Verify if album exists

            if (await _songRepository.ExistsBySongIdAsync(song.Id))
            {
                Song newSong = _mapper.Map<Song>(song);

                var message = await ValidateSongAsync(newSong);
                if (message is not null)
                {
                    throw new ServiceException(_songServiceException + message);
                }

                await _songRepository.UpdateSongAsync(newSong);
            }
            else
            {
                throw new ServiceException(_songServiceException + _invalidSongErrorMessage);
            }
        }

        public async Task DeleteSongByIdAsync(int songId)
        {
            if (await _songRepository.ExistsBySongIdAsync(songId))
            {
                await _songRepository.DeleteSongByIdAsync(songId);
            }
            else
            {
                throw new ServiceException(_songServiceException + _invalidSongErrorMessage);
            }    
        }

        private async Task<string> ValidateSongAsync(Song song)
        {
            if (song.Title.TrimEnd().Length == 0)
            {
                return _invalidSongTitleErrorMessage;
            }

            if (song.Length.TrimEnd().Length == 0)
            {
                return _invalidSongLengthErrorMessage;
            }

            var time = song.Length.Split(':');
            if (time.Length == 3 || time.Length == 2)
            {
                if (time.All(part => int.TryParse(part, out _)))
                {
                    var timeParts = time.Select(int.Parse).ToArray();

                    int hours = timeParts.Length == 3 ? timeParts[0] : 0;
                    int minutes = timeParts.Length == 3 ? timeParts[1] : timeParts[0];
                    int seconds = timeParts.Length == 3 ? timeParts[2] : timeParts[1];

                    if (hours < 0 || minutes < 0 || seconds < 0)
                    {
                        return _invalidLengthErrorMessage;
                    }

                    if (hours > 0 && (minutes >= 60 || seconds >= 60))
                    {
                        return _invalidLengthErrorMessage;
                    }

                    if (minutes >= 60 || seconds >= 60)
                    {
                        return _invalidLengthErrorMessage;
                    }

                    if (hours == 0 && minutes == 0 && seconds == 0)
                    {
                        return _invalidLengthErrorMessage;
                    }
                }
                else
                {
                    return _lengthFormatErrorMessage;
                }
            }
            else
            {
                return _lengthFormatErrorMessage;
            }

            if (!await _songRepository.IsSongUniqueAsync(song))
            {
                return _uniqueSongErrorMessage;
            }

            return null;
        }
    }
}
