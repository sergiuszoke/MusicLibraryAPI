using AutoMapper;
using MusicLibraryDAL.Models;
using MusicLibraryBLL.DTOs;

namespace MusicLibraryBLL.MapperProfiles
{
    public class ArtistProfile : Profile
    {
        public ArtistProfile() 
        { 
            CreateMap<Artist, ArtistDTO>();
            CreateMap<ArtistDTO, Artist>();
            CreateMap<CreateArtistDTO, Artist>();
        }
    }
}
