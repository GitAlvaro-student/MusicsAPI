using MusicSoundAPI.Models;

namespace MusicSoundAPI.Repositories.Artist
{
    public interface IArtistRepository
    {
        Task<IEnumerable<Models.Artista>> GetAllArtists();
        Task<Models.Artista> GetArtistById(int idArtist);
        Task InsertArtist(Models.Artista artist);
    }
}
