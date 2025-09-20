using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MusicSoundAPI.Models;

namespace MusicSoundAPI.ApplicationDbContext;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Artista> Artistas { get; set; }

    public virtual DbSet<Musica> Musicas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XEPDB1)));User Id=ALVARO;Password=ALVARO_2004;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("ALVARO")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Artista>(entity =>
        {
            entity.HasKey(e => e.IdArtist).HasName("SYS_C008222");

            entity.Property(e => e.IdArtist).HasDefaultValueSql("MUSICA.SQ_ARTISTA_ID.NEXTVAL ");
        });

        modelBuilder.Entity<Musica>(entity =>
        {
            entity.HasKey(e => e.IdMusic).HasName("PK_ID_MUSIC");

            entity.Property(e => e.IdMusic).ValueGeneratedOnAdd();
            entity.Property(e => e.Popularidade).HasDefaultValueSql("0");

            entity.HasOne(d => d.IdArtistNavigation).WithMany(p => p.Musicas).HasConstraintName("FK_ID_ARTISTA");
        });
        modelBuilder.HasSequence("SQ_ARTISTA_ID", "MUSICA");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
