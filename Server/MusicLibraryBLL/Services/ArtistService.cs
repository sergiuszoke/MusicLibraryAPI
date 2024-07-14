using AutoMapper;
using MusicLibraryBLL.DTOs;
using MusicLibraryBLL.Interfaces;
using MusicLibraryDAL.Interfaces;
using MusicLibraryDAL.Models;

namespace MusicLibraryBLL.Services
{
    public class ArtistService : IArtistService
    {
        private readonly string _artistServiceException = "Artist service exception. ";
        private readonly string _invalidArtistErrorMessage = "Artist does not exist.";
        private readonly string _invalidArtistNameErrorMessage = "Name cannot be null or empty.";
        private readonly string _uniqueArtistErrorMessage = "Artist already exists.";

        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public ArtistService(IArtistRepository artistRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        public async Task AddArtistAsync(CreateArtistDTO artist)
        {
            Artist newArtist = _mapper.Map<Artist>(artist);

            var message = await ValidateArtistAsync(newArtist);
            if (message is not null)
            {
                throw new Exception(_artistServiceException + message);
            }

            await _artistRepository.AddArtistAsync(newArtist);
        }

        public async Task<List<ArtistDTO>> GetAllArtistsAsync()
        {
            var artistList = await _artistRepository.GetAllArtistsAsync();
            
            return artistList
                .Select(artist => _mapper.Map<ArtistDTO>(artist))
                .ToList();
        }

        public async Task UpdateArtistAsync(ArtistDTO artist)
        {
            if (await _artistRepository.ExistsByArtistIdAsync(artist.Id))
            {
                Artist newArtist = _mapper.Map<Artist>(artist);

                var message = await ValidateArtistAsync(newArtist);
                if (message is not null)
                {
                    throw new ServiceException(_artistServiceException + message);
                }

                await _artistRepository.UpdateArtistAsync(newArtist);
            }
            else
            {
                throw new ServiceException(_artistRepository + _invalidArtistErrorMessage);
            }
        }

        public async Task DeleteArtistByIdAsync(int artistId)
        {
            if (await _artistRepository.ExistsByArtistIdAsync(artistId))
            {
                await _artistRepository.DeleteArtistByIdAsync(artistId);
            }
            else
            {
                throw new ServiceException(_artistRepository + _invalidArtistErrorMessage);
            }
        }

        private async Task<string> ValidateArtistAsync(Artist artist)
        {
            if (artist.Name.TrimEnd().Length == 0)
            {
                return _invalidArtistNameErrorMessage;
            }
            
            if(!await _artistRepository.IsArtistUniqueAsync(artist))
            {
                return _uniqueArtistErrorMessage;
            }

            return null;
        }
    }
}
