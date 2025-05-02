using System.ComponentModel.DataAnnotations;

namespace medienVerwaltungDbSolution.Models.Interpret
{
    sealed public class Interpret
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string FormattedBirthDate => $"Geb.: {BirthDate.Day}.{BirthDate.Month}.{BirthDate.Year}";
        public string FullName => $"{FirstName} {Name}";

        public Interpret()
        {
            BirthDate = DateTime.Now.Date;
        }
    }
}