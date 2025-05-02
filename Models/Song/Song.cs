using System.ComponentModel.DataAnnotations;

namespace medienVerwaltungDbSolution.Models.Song
{
    public class Song : Media
    {
        [Key]
        public int Id { get; set; }
        public int Length { get; set; }
    }
}