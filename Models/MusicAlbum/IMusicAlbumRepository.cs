using medienVerwaltungDbSolution.Models.MusicAlbum;

namespace medienVerwaltungDbSolution.Models.MusicAlbum
{
    public interface IMusicAlbumRepository
    {
        Task<MusicAlbum?> GetByIdAsync(int ID);
        Task AddAsync(MusicAlbum musicAlbum);
        void Update(MusicAlbum musicAlbum);
        void Remove(MusicAlbum musicAlbum);
    }
}