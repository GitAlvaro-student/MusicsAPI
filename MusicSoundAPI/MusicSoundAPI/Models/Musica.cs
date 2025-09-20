using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicSoundAPI.Models;

[Table("MUSICAS", Schema = "MUSICA")]
public partial class Musica
{
    [Key]
    [Column("ID_MUSIC", TypeName = "NUMBER")]
    public int IdMusic { get; set; }

    [Column("MUSICA")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Nome { get; set; }

    [Column("POPULARIDADE")]
    [Precision(3)]
    public int? Popularidade { get; set; }

    [Column("DURACAO")]
    [Precision(10)]
    public int? Duracao { get; set; }

    [Column("ANO")]
    [Precision(4)]
    public int? Ano { get; set; }

    [Column("ID_ARTIST", TypeName = "NUMBER")]
    public int? IdArtist { get; set; }

    [ForeignKey("IdArtist")]
    [InverseProperty("Musicas")]
    public virtual Artista? IdArtistNavigation { get; set; }
}
