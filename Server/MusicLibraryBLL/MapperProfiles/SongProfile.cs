using AutoMapper;
using MusicLibraryDAL.Models;
using MusicLibraryBLL.DTOs;

namespace MusicLibraryBLL.MapperProfiles
{
    public class SongProfile : Profile
    {
        public SongProfile() 
        {
            CreateMap<Song, SongDTO>();
            CreateMap<SongDTO, Song>();
            CreateMap<CreateSongDTO, Song>();
        }
    }
}
