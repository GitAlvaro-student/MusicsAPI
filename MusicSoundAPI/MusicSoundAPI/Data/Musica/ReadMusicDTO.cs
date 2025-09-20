namespace MusicSoundAPI.Data.Musica
{
    public class ReadMusicDTO
    {
        public string? Nome { get; set; }
        public int? Popularidade { get; set; }
        public int Duracao { get; set; }
        public int Ano { get; set; }
        public int? IdArtist { get; set; }
    }
}
