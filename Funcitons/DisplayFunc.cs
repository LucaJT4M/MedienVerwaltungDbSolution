using medienVerwaltungDbSolution.Models.Song;
using medienVerwaltungDbSolution.Services;

namespace medienVerwaltungDbSolution.Funcitons
{
    public class DisplayFunc
    {
        private readonly DbChanges _dbChanges;
        private readonly DatabaseContext _context;
        public DisplayFunc(DatabaseContext context, DbChanges dbChanges)
        {
            _dbChanges = dbChanges;
            _context = context;
            foreach (Song song in GetSongs())
            {
                System.Console.WriteLine("ID: " + song.ID + " Title: " + song.Title);
            }
        }
        private List<Song> GetSongs()
        {
            return [.. _context.Songs];
        }
    }
}