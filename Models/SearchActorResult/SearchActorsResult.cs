using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace medienVerwaltungDbSolution.Models.SearchActorResult
{
    public class SearchActorsResult
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public ObservableCollection<int> MovieIDs { get; set; } = [];
        public DateTime BirthDate { get; set; }
        public string FormattedBirthDate => $"{BirthDate.Date.Day}.{BirthDate.Date.Month}.{BirthDate.Date.Year}";
        public string Gender { get; set; } = string.Empty;
    }
}