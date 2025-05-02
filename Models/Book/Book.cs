using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace medienVerwaltungDbSolution.Models.Book
{
    public class Book : Media
    {
        [Key]
        public int Isbn { get; set; }
        public string Description { get; set; } = string.Empty;
        public int PageCount { get; set; }
        public int ReleaseYear { get; set; }
    }
}