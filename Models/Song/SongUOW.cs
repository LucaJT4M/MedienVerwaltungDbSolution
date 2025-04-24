namespace medienVerwaltungDbSolution.Models.Song
{
    public class SongUOW
    {
        public SessionType Session { get; set; }
        public Song Song { get; set; } = new();
    }
}