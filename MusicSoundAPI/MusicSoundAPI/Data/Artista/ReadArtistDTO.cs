using MusicSoundAPI.Models;

namespace MusicSoundAPI.Data.Artista
{
    public class ReadArtistDTO
    {
        public string? Nome { get; set; }
        public string? Genero { get; set; }
        public string? Nacionalidade { get; set; }
        public DateTime? Nascimento { get; set; }
        public virtual ICollection<MusicSoundAPI.Models.Musica>? Musica { get; set; }
    }
}
