using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace medienVerwaltungDbSolution.Models.MusicAlbum
{
    public class MusicAlbum : Media
    {
        [Key]
        public int Id { get; set; }
        public ObservableCollection<int> SongIdList { get; set; } = [];
    }
}