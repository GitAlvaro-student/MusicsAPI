using AutoMapper;
using MusicSoundAPI.Data.Artista;
using MusicSoundAPI.Repositories.Artist;

namespace MusicSoundAPI.Services.Artist
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public ArtistService(IArtistRepository artistRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        public async Task<Models.Artista> GetTbdArtistaById(int idArtist)
        {
            var artist = await _artistRepository.GetArtistById(idArtist);
            return artist;
        }

        public async Task<ReadArtistDTO> GetArtistaById(int idArtist)
        {
            var artist = await _artistRepository.GetArtistById(idArtist);
            return _mapper.Map<ReadArtistDTO>(artist);
        }

        public async Task<IEnumerable<ReadArtistDTO>> GetAllArtistas()
        {
            var artists = await _artistRepository.GetAllArtists();
            return _mapper.Map<IEnumerable<ReadArtistDTO>>(artists);
        }

        public async Task InsertArtista(CreateArtistDTO artist)
        {
            var artista = _mapper.Map<Models.Artista>(artist);
            await _artistRepository.InsertArtist(artista);
        }

    }
}
