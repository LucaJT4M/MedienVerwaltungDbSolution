using medienVerwaltungDbSolution.Models.SearchActorResult;

namespace medienVerwaltungDbSolution.Models
{
    public interface IDbChanges
    {
        void Add<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        Task<Book.Book> GetBookByIdAsync(int ID);
        Task<Song.Song> GetSongByIdAsync(int ID);
        Task<Movie.Movie> GetMovieByIdAsync(int ID);
        Task<MusicAlbum.MusicAlbum> GetMusicAlbumByIdAsync(int ID);
        Task<Interpret.Interpret> GetInterpretByIdAsync(int ID);
        Task<SearchResult.SearchResult> GetItemByIdAsync(int ID);
        Task<SearchActorsResult> GetActorByIdAsync(int ID);
        Task ExecuteTransactionAsync();
    }
}