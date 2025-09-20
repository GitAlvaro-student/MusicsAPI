using AutoMapper;
using MusicSoundAPI.Data.Artista;
using MusicSoundAPI.Models;

namespace MusicSoundAPI.Profiles
{
    public class ArtistProfile : Profile
    {
        public ArtistProfile()
        {
            CreateMap<Artista, CreateArtistDTO>();
            CreateMap<CreateArtistDTO, Artista>();
            CreateMap<ReadArtistDTO, Artista>();
            CreateMap<Artista, ReadArtistDTO>();
        }
    }
}
