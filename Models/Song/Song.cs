using System.ComponentModel.DataAnnotations;

namespace medienVerwaltungDbSolution.Models.Song
{
    public class Song : Media
    {
        [Key]
        public int ID { get; set; }
        public int Length { get; set; }
    }
}