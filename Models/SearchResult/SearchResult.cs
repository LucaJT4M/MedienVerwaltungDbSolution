using System.ComponentModel.DataAnnotations;

namespace medienVerwaltungDbSolution.Models.SearchResult
{
    public class SearchResult
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public MediaType MediaType { get; set; }
        public string? Location { get; set; } = string.Empty;
        public int MediaId { get; set; }
    }
}