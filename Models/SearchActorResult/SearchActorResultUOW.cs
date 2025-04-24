namespace medienVerwaltungDbSolution.Models.SearchActorResult
{
    public class SearchActorResultUOW
    {
        public SessionType Session { get; set; }
        public SearchActorsResult SearchActorsResult { get; set; } = new();
    }
}