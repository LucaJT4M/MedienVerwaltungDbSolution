using medienVerwaltungDbSolution.Models.Book;
using medienVerwaltungDbSolution.Models.Interpret;
using medienVerwaltungDbSolution.Models.Movie;
using medienVerwaltungDbSolution.Models.MusicAlbum;
using medienVerwaltungDbSolution.Models.SearchActorResult;
using medienVerwaltungDbSolution.Models.SearchResult;
using medienVerwaltungDbSolution.Models.Song;
using Microsoft.EntityFrameworkCore.Storage;

namespace medienVerwaltungDbSolution.Models
{
    public interface IUnitOfWork : IDisposable
    {
        public ISongRepository Songs { get; }
        public IBookRepository Books { get; }
        public IMovieRepository Movies { get; }
        public IMusicAlbumRepository MusicAlbums { get; }
        public IInterpretRepository Interprets { get; }
        public ISearchResultRepository Items { get; }
        public ISearchActorResultRepository Actors { get; }
        void Add<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        Task BeginTransactionAsync();
    }
}