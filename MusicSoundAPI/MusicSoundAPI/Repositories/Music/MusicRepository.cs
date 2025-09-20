using Microsoft.EntityFrameworkCore;
using MusicSoundAPI.ApplicationDbContext;

namespace MusicSoundAPI.Repositories.Music
{
    /// <summary>
    /// Representa o Repositório de Músicas
    /// </summary>
    public class MusicRepository : IMusicRepository
    {

        private ModelContext appDbContext;
        public MusicRepository(ModelContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Models.Musica>> GetMusics()
        {
            return await appDbContext.Musicas.ToListAsync();
        }

        public async Task<Models.Musica> GetMusicById(int id)
        {
            return await appDbContext.Musicas.FindAsync(id);
        }

        public async Task<IEnumerable<Models.Musica>> GetMusicsByArtist(int idArtist)
        {
            return await appDbContext.Musicas.Where(x => x.IdArtist == idArtist)
                .Select(x => new Models.Musica
                {
                    IdMusic = x.IdMusic,
                    Nome = x.Nome,
                    Popularidade = x.Popularidade,
                    Duracao = x.Duracao,
                    Ano = x.Ano,
                    IdArtist = x.IdArtist
                })
                .ToListAsync();
        }
        public async Task InsertMusic(Models.Musica song)
        {
            appDbContext.Musicas.Add(song);
            await appDbContext.SaveChangesAsync();
        }

    }
}

