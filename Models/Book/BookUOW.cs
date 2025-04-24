namespace medienVerwaltungDbSolution.Models.Book
{
    public class BookUOW
    {
        public Book Book { get; set; } = new();
        public SessionType Session { get; set; }
    }
}