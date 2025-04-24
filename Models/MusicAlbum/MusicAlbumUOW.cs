namespace medienVerwaltungDbSolution.Models.MusicAlbum
{
    public class MusicAlbumUOW
    {
        public SessionType Session { get; set; }
        public MusicAlbum Album { get; set; } = new();
    }
}