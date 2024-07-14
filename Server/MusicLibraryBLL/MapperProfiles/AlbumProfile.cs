using AutoMapper;
using MusicLibraryDAL.Models;
using MusicLibraryBLL.DTOs;

namespace MusicLibraryBLL.MapperProfiles
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile() 
        {
            CreateMap<Album, AlbumDTO>();
            CreateMap<AlbumDTO, Album>();
            CreateMap<CreateAlbumDTO, Album>();
        }
    }
}
