namespace medienVerwaltungDbSolution.Models.SearchResult
{
    public class SearchResultUOW
    {
        public SessionType Session { get; set; }
        public SearchResult SearchResult { get; set; } = new();
    }
}