namespace medienVerwaltungDbSolution.Models.Movie
{
    public class MovieUOW
    {
        public Movie Movie { get; set; } = new();
        public SessionType Session { get; set; }
    }
}