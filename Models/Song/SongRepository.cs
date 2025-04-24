using medienVerwaltungDbSolution.Services;

namespace medienVerwaltungDbSolution.Models.Song
{
    public class SongRepository(DatabaseContext context) : ISongRepository
    {
        private readonly DatabaseContext _context = context;

        public async Task<Song?> GetByIdAsync(int ID)
        {
            return await _context.Songs.FindAsync(ID);
        }
        public async Task AddAsync(Song song)
        {
            await _context.Songs.AddAsync(song);
        }

        public void Update(Song song)
        {
            _context.Songs.Update(song);
        }

        public void Remove(Song song)
        {
            _context.Songs.Remove(song);
        }
    }
}