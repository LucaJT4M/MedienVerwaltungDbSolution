using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace medienVerwaltungDbSolution.Models.Movie
{
    public class Movie
    {
        [Key]
        public int ID { get; set; }
        public int Length { get; set; }
        public int ReleaseYear { get; set; }
        public string? Description { get; set; }
        public string? Genre { get; set; }
        public ObservableCollection<int> ActorIDs { get; set; } = [];
        public string ActorCountDisplay => "Schauspieler: " + ActorIDs.Count;
        public string? Title { get; set; }
        public string? Location { get; set; }
    }
}