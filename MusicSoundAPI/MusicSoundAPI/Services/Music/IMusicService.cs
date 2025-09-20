using MusicSoundAPI.Data.Musica;
using MusicSoundAPI.Models;

namespace MusicSoundAPI.Services.Music
{
    public interface IMusicService
    {
        Task<Musica> GetTbdSongById(int id);
        Task<ReadMusicDTO> GetMusicById(int id);
        Task<IEnumerable<ReadMusicDTO>> GetMusics();
        Task InsertMusic(CreateMusicDTO music);
    }
}
