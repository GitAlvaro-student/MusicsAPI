using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicSoundAPI.Models;

[Table("ARTISTAS", Schema = "MUSICA")]
public partial class Artista
{
    [Key]
    [Column("ID_ARTIST", TypeName = "NUMBER")]
    public int IdArtist { get; set; }

    [Column("ARTISTA")]
    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [Column("GENERO")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Genero { get; set; }

    [Column("NACIONALIDADE")]
    [StringLength(100)]
    [Unicode(false)]
    public string Nacionalidade { get; set; } = null!;

    [Column("NASCIMENTO", TypeName = "DATE")]
    public DateTime? Nascimento { get; set; }

    [InverseProperty("IdArtistNavigation")]
    public virtual ICollection<Musica> Musicas { get; set; } = new List<Musica>();
}
