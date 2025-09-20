using AutoMapper;
using MusicSoundAPI.Data.Musica;
using MusicSoundAPI.Models;

namespace MusicSoundAPI.Profiles
{
    public class MusicProfile : Profile
    {
        public MusicProfile()
        {
            CreateMap<Musica, CreateMusicDTO>();
            CreateMap<CreateMusicDTO, Musica>();
            CreateMap<ReadMusicDTO, Musica>();
            CreateMap<Musica, ReadMusicDTO>();
        }
    }
}
