
using medienVerwaltungDbSolution.Services;

namespace medienVerwaltungDbSolution.Models.Movie
{
    public class MovieRepository(DatabaseContext context) : IMovieRepository
    {
        private readonly DatabaseContext _context = context;

        public async Task<Movie?> GetByIdAsync(int ID)
        {
            return await _context.Movies.FindAsync(ID);
        }
        public async Task AddAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
        }

        public void Update(Movie movie)
        {
            _context.Movies.Update(movie);
        }

        public void Remove(Movie movie)
        {
            _context.Movies.Remove(movie);
        }
    }
}