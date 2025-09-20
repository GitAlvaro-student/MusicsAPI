namespace MusicSoundAPI.Repositories.Music
{
    public interface IMusicRepository
    {
        Task<IEnumerable<Models.Musica>> GetMusics();
        Task<Models.Musica> GetMusicById(int idMusic);
        Task<IEnumerable<Models.Musica>> GetMusicsByArtist(int idArtist);
        Task InsertMusic(Models.Musica music);
    }
}
