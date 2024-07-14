using AutoMapper;
using MusicLibraryBLL.DTOs;
using MusicLibraryBLL.Interfaces;
using MusicLibraryDAL.Interfaces;
using MusicLibraryDAL.Models;

namespace MusicLibraryBLL.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly string _albumServiceException = "Album service exception. ";
        private readonly string _invalidAlbumTitleErrorMessage = "Album title cannot be null or empty.";
        private readonly string _invalidAlbumDescriptionErrorMessage = "Album description cannot be null or empty.";
        private readonly string _uniqueAlbumErrorMessage = "Album already Exists.";
        private readonly string _invalidAlbumErrorMessage = "Album does not exist.";
        private readonly string _invalidArtistErrorMessage = "Artist does not exist.";

        private readonly IArtistRepository _artistRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;

        public AlbumService(
            IArtistRepository artistRepository,
            IAlbumRepository albumService, 
            IMapper mapper)
        {
            _artistRepository = artistRepository;
            _albumRepository = albumService;
            _mapper = mapper;
        }

        public async Task AddAlbumAsync(CreateAlbumDTO album)
        {
            if (!await _artistRepository.ExistsByArtistIdAsync(album.ArtistId))
            {
                throw new ServiceException(_albumServiceException + _invalidArtistErrorMessage);
            }

            Album newAlbum = _mapper.Map<Album>(album);

            var message = await ValidateAlbumAsync(newAlbum);
            if (message is not null)
            {
                throw new ServiceException(_albumServiceException + message);
            }

            await _albumRepository.AddAlbumAsync(newAlbum);
        }

        public async Task<List<AlbumDTO>> GetAllAlbumsFromArtistAsync(int artistId)
        {
            if (!await _artistRepository.ExistsByArtistIdAsync(artistId))
            {
                throw new ServiceException(_albumServiceException + _invalidArtistErrorMessage);
            }

            var albumList = await _albumRepository.GetAllAlbumsFromArtistAsync(artistId);

            return albumList
                .Select(album => _mapper.Map<AlbumDTO>(album))
                .ToList();
        }

        public async Task UpdateAlbumAsync(AlbumDTO album)
        {
            if (!await _artistRepository.ExistsByArtistIdAsync(album.ArtistId))
            {
                throw new ServiceException(_albumServiceException + _invalidArtistErrorMessage);
            }

            if (await _albumRepository.ExistsByAlbumIdAsync(album.Id))
            {
                Album newAlbum = _mapper.Map<Album>(album);

                var message = await ValidateAlbumAsync(newAlbum);
                if (message is not null)
                {
                    throw new ServiceException(_albumServiceException + message);
                }

                await _albumRepository.UpdateAlbumAsync(newAlbum);
            }
            else
            {
                throw new Exception(_albumServiceException + _invalidAlbumErrorMessage);
            }
        }

        public async Task DeleteAlbumByIdAsync(int albumId)
        {
            if (await _albumRepository.ExistsByAlbumIdAsync(albumId))
            {
                await _albumRepository.DeleteAlbumByIdAsync(albumId);
            }
            else
            {
                throw new ServiceException(_albumServiceException + _invalidAlbumErrorMessage);
            }    
        }

        private async Task<string> ValidateAlbumAsync(Album album)
        {
            if (album.Title.TrimEnd().Length == 0)
            {
                return _invalidAlbumTitleErrorMessage;
            }

            if (album.Description.TrimEnd().Length == 0)
            {
                return _invalidAlbumDescriptionErrorMessage;
            }

            if (!await _albumRepository.IsAlbumUniqueAsync(album))
            {
                return _uniqueAlbumErrorMessage;
            }

            return null;
        }
    }
}
