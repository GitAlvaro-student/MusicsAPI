using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicSoundAPI.Data.Musica
{
    public class CreateMusicDTO
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Nome da Música")]
        public string? Nome { get; set; }

        [Required]
        [Precision(3)]
        public int? Popularidade { get; set; }

        [Required]
        [Precision(10)]
        public int Duracao { get; set; }

        [Required]
        [Precision(4)]
        public int Ano { get; set; }

        [Required]
        public int? IdArtist { get; set; }

    }
}
