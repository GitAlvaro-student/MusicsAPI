using MusicSoundAPI.Data.Artista;
using MusicSoundAPI.Models;

namespace MusicSoundAPI.Services.Artist
{
    public interface IArtistService
    {
        Task<Models.Artista> GetTbdArtistaById(int id);
        Task<ReadArtistDTO> GetArtistaById(int id);
        Task<IEnumerable<ReadArtistDTO>> GetAllArtistas();
        Task InsertArtista(CreateArtistDTO artista);

    }
}
