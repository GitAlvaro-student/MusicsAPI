using AutoMapper;
using MusicSoundAPI.Data.Musica;
using MusicSoundAPI.Models;
using MusicSoundAPI.Repositories.Music;

namespace MusicSoundAPI.Services.Music
{
    public class MusicService : IMusicService
    {
        private readonly IMusicRepository _musicRepository;
        private readonly IMapper _mapper;

        public MusicService(IMusicRepository musicRepository, IMapper mapper)
        {
            _musicRepository = musicRepository;
            _mapper = mapper;
        }

        public async Task<Musica> GetTbdSongById(int idTbdSong)
        {
            return await _musicRepository.GetMusicById(idTbdSong);
        }

        public async Task<ReadMusicDTO> GetMusicById(int idMusic)
        {
            var songs = await _musicRepository.GetMusicById(idMusic);
            return _mapper.Map<ReadMusicDTO>(songs);
        }
        public async Task<IEnumerable<ReadMusicDTO>> GetMusics()
        {
            var songs = await _musicRepository.GetMusics();
            return _mapper.Map<List<ReadMusicDTO>>(songs);

        }

        public async Task InsertMusic(CreateMusicDTO music)
        {
            var song = _mapper.Map<Musica>(music);
            await _musicRepository.InsertMusic(song);
        }
    }
}
