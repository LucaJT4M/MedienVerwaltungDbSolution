namespace medienVerwaltungDbSolution.Models.Movie
{
    public interface IMovieRepository
    {
        Task<Movie?> GetByIdAsync(int ID);
        Task AddAsync(Movie movie);
        void Update(Movie movie);
        void Remove(Movie movie);
    }
}