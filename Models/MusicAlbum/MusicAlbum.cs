using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace medienVerwaltungDbSolution.Models.MusicAlbum
{
    public class MusicAlbum : Media
    {
        [Key]
        public int ID { get; set; }
        public ObservableCollection<int> SongIDList { get; set; } = [];
    }
}