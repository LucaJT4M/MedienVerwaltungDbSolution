
using medienVerwaltungDbSolution.Services;

namespace medienVerwaltungDbSolution.Models.MusicAlbum
{
    public class MusicAlbumRepository(DatabaseContext context) : IMusicAlbumRepository
    {
        private readonly DatabaseContext _context = context;

        public async Task<MusicAlbum?> GetByIdAsync(int ID)
        {
            return await _context.MusicAlbums.FindAsync(ID);
        }
        public async Task AddAsync(MusicAlbum musicAlbum)
        {
            await _context.MusicAlbums.AddAsync(musicAlbum);
        }

        public void Update(MusicAlbum musicAlbum)
        {
            _context.MusicAlbums.Update(musicAlbum);
        }

        public void Remove(MusicAlbum musicAlbum)
        {
            _context.MusicAlbums.Remove(musicAlbum);
        }
    }
}