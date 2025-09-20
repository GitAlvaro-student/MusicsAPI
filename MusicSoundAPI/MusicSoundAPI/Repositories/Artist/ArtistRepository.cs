using Microsoft.EntityFrameworkCore;
using MusicSoundAPI.ApplicationDbContext;

namespace MusicSoundAPI.Repositories.Artist
{
    public class ArtistRepository : IArtistRepository
    {
        private ModelContext appDbContext;
        public ArtistRepository(ModelContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Models.Artista>> GetAllArtists()
        {
            return await appDbContext.Artistas.ToListAsync();
        }

        public async Task<Models.Artista> GetArtistById(int id)
        {
            return await appDbContext.Artistas.FindAsync(id);
        }

        public async Task InsertArtist(Models.Artista artist)
        {
            appDbContext.Artistas.Add(artist);
            await appDbContext.SaveChangesAsync();
        }
    }
}
