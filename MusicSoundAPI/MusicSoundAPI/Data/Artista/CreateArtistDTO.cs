using MusicSoundAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicSoundAPI.Data.Artista
{
    public class CreateArtistDTO
    {
        [Required]
        [StringLength(100)]
        public string? Nome { get; set; }

        [StringLength(50)]
        public string? Genero { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nacionalidade { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? Nascimento { get; set; }


    }
}
