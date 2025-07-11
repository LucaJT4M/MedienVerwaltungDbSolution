using Mapster;
using medienVerwaltungDbSolution.Models;
using medienVerwaltungDbSolution.Models.Book;
using medienVerwaltungDbSolution.Models.Interpret;
using medienVerwaltungDbSolution.Models.Movie;
using medienVerwaltungDbSolution.Models.MusicAlbum;
using medienVerwaltungDbSolution.Models.SearchActorResult;
using medienVerwaltungDbSolution.Models.SearchResult;
using medienVerwaltungDbSolution.Models.Song;
using Microsoft.EntityFrameworkCore;

namespace medienVerwaltungDbSolution.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        public ISongRepository Songs { get; }
        public IBookRepository Books { get; }
        public IMovieRepository Movies { get; }
        public IMusicAlbumRepository MusicAlbums { get; }
        public IInterpretRepository Interprets { get; }
        public ISearchResultRepository Items { get; }
        public ISearchActorResultRepository Actors { get; }
        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
            Songs = new SongRepository(_context);
            Books = new BookRepository(_context);
            Movies = new MovieRepository(_context);
            MusicAlbums = new MusicAlbumRepository(_context);
            Interprets = new InterpretRepository(_context);
            Items = new SearchResultRepository(_context);
            Actors = new SearchActorResultRepository(_context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        private async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task BeginTransactionAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var output = await SaveChangesAsync();
                await AddItems();
                var itemsOutput = await SaveChangesAsync();

                await transaction.CommitAsync();
                System.Console.WriteLine($"DB Changes: {output}, ItemChanges: {itemsOutput}");
            }
            catch (System.Exception)
            {
                await transaction.RollbackAsync();
                System.Console.WriteLine("Rollback");
                throw;
            }
        }
        public async void Add<T>(T entity) where T : class
        {
            switch (entity)
            {
                case Song:
                    var newSong = entity as Song ?? throw new Exception("Entity is not a Song");
                    await Songs.AddAsync(newSong);
                    break;

                case Book:
                    var newBook = entity as Book ?? throw new Exception("Entity is not a Book");
                    await Books.AddAsync(newBook);
                    break;

                case Movie:
                    var newMovie = entity as Movie ?? throw new Exception("Entity is not a Movie");
                    await Movies.AddAsync(newMovie);
                    break;

                case MusicAlbum:
                    var newAlbum = entity as MusicAlbum ?? throw new Exception("Entity is not a MusicAlbum");
                    await MusicAlbums.AddAsync(newAlbum);
                    break;

                case Interpret:
                    var newInterpret = entity as Interpret ?? throw new Exception("Entity is not a Interpret");
                    await Interprets.AddAsync(newInterpret);
                    break;

                case SearchActorsResult:
                    var newActor = entity as SearchActorsResult ?? throw new Exception("Entity is not a Actor");
                    await Actors.AddAsync(newActor);
                    break;

                default:
                    break;
            }
        }
        public void Remove<T>(T entity) where T : class
        {
            switch (entity)
            {
                case Song:
                    var toRemoveSong = entity as Song ?? throw new Exception("Entity is not a Song");
                    Songs.Remove(toRemoveSong);
                    var toRemoveSongItem = _context.Items.AsNoTracking().FirstOrDefault(i => i.MediaId == toRemoveSong.Id);
                    if (toRemoveSongItem != null)
                    {
                        Items.Remove(toRemoveSongItem);
                    }

                    break;

                case Book:
                    var toRemoveBook = entity as Book ?? throw new Exception("Entity is not a Book");
                    Books.Remove(toRemoveBook);
                    var toRemoveBookItem = _context.Items.AsNoTracking().FirstOrDefault(i => i.MediaId == toRemoveBook.Isbn);
                    if (toRemoveBookItem != null)
                    {
                        Items.Remove(toRemoveBookItem);
                    }

                    break;

                case Movie:
                    var toRemoveMovie = entity as Movie ?? throw new Exception("Entity is not a Movie");
                    Movies.Remove(toRemoveMovie);
                    var toRemoveMovieItem = _context.Items.AsNoTracking().FirstOrDefault(i => i.MediaId == toRemoveMovie.Id);
                    if (toRemoveMovieItem != null)
                    {
                        Items.Remove(toRemoveMovieItem);
                    }

                    break;

                case MusicAlbum:
                    var toRemoveAlbum = entity as MusicAlbum ?? throw new Exception("Entity is not a MusicAlbum");
                    MusicAlbums.Remove(toRemoveAlbum);
                    var toRemoveAlbumItem = _context.Items.AsNoTracking().FirstOrDefault(i => i.MediaId == toRemoveAlbum.Id);
                    if (toRemoveAlbumItem != null)
                    {
                        Items.Remove(toRemoveAlbumItem);
                    }

                    break;

                case Interpret:
                    var toRemoveInterpret = entity as Interpret ?? throw new Exception("Entity is not a Interpret");
                    Interprets.Remove(toRemoveInterpret);
                    break;

                case SearchActorsResult:
                    var toRemoveActor = entity as SearchActorsResult ?? throw new Exception("Entity is not a Actor");
                    Actors.Remove(toRemoveActor);
                    break;

                case SearchResult:
                    var toRemoveItem = entity as SearchResult ?? throw new Exception("Entity is not a Item");
                    Items.Remove(toRemoveItem);
                    break;

                default:
                    break;
            }
        }
        public void Update<T>(T entity) where T : class
        {
            switch (entity)
            {
                case Song:
                    var toUpdateSong = entity as Song ?? throw new Exception("Entity is not a Song");
                    Songs.Update(toUpdateSong);
                    var toUpdateItem = _context.Items.AsNoTracking().FirstOrDefault(i => i.MediaId == toUpdateSong.Id);

                    if (toUpdateItem != null)
                    {
                        toUpdateItem.Location = toUpdateSong.Location;
                        toUpdateItem.Title = toUpdateSong.Title;
                        Items.Update(toUpdateItem);
                    }

                    break;

                case Book:
                    var toUpdateBook = entity as Book ?? throw new Exception("Entity is not a Book");
                    Books.Update(toUpdateBook);
                    var toUpdateBookItem = _context.Items.AsNoTracking().FirstOrDefault(i => i.MediaId == toUpdateBook.Isbn);
                    if (toUpdateBookItem != null)
                    {
                        toUpdateBookItem.Title = toUpdateBook.Title;
                        toUpdateBookItem.Location = toUpdateBook.Location;
                        Items.Update(toUpdateBookItem);
                    }

                    break;

                case Movie:
                    var toUpdateMovie = entity as Movie ?? throw new Exception("Entity is not a Movie");
                    Movies.Update(toUpdateMovie);
                    var toUpdateMovieItem = _context.Items.AsNoTracking().FirstOrDefault(i => i.MediaId == toUpdateMovie.Id);
                    if (toUpdateMovieItem != null)
                    {
                        toUpdateMovieItem.Title = toUpdateMovie.Title;
                        toUpdateMovieItem.Location = toUpdateMovie.Location;
                        Items.Update(toUpdateMovieItem);
                    }

                    break;

                case MusicAlbum:
                    var toUpdateAlbum = entity as MusicAlbum ?? throw new Exception("Entity is not a MusicAlbum");
                    MusicAlbums.Update(toUpdateAlbum);
                    var toUpdateAlbumItem = _context.Items.AsNoTracking().FirstOrDefault(i => i.MediaId == toUpdateAlbum.Id);
                    if (toUpdateAlbumItem != null)
                    {
                        toUpdateAlbumItem.Title = toUpdateAlbum.Title;
                        toUpdateAlbumItem.Location = toUpdateAlbum.Location;
                        Items.Update(toUpdateAlbumItem);
                    }

                    break;

                case Interpret:
                    var toUpdateInterpret = entity as Interpret ?? throw new Exception("Entity is not a Interpret");
                    Interprets.Update(toUpdateInterpret);
                    break;


                default:
                    break;
            }
        }
        private async Task AddItems()
        {
            var songList = _context.Songs.AsNoTracking().Select(s => new Song
            {
                Id = s.Id,
                Title = s.Title,
                Location = s.Location
            });
            var bookList = _context.Books.AsNoTracking().Select(b => new Book
            {
                Isbn = b.Isbn,
                Title = b.Title,
                Location = b.Location
            });
            var movieList = _context.Movies.AsNoTracking().Select(m => new Movie
            {
                Id = m.Id,
                Title = m.Title,
                Location = m.Location
            });
            var musicAlbumList = _context.MusicAlbums.AsNoTracking().Select(m => new MusicAlbum
            {
                Id = m.Id,
                Title = m.Title,
                Location = m.Location
            });
            var itemList = await _context.Items.AsNoTracking().ToListAsync();

            foreach (var song in songList)
            {
                if (!itemList.Any(i => i.MediaId == song.Id))
                {
                    var newItem = new SearchResult();
                    newItem = song.Adapt<SearchResult>();
                    newItem.Id = 0;
                    newItem.MediaId = song.Id;
                    await Items.AddAsync(newItem);
                }
            }
            foreach (var book in bookList)
            {
                if (!itemList.Any(i => i.MediaId == book.Isbn))
                {
                    var newItem = new SearchResult();
                    newItem = book.Adapt<SearchResult>();
                    newItem.Id = 0;
                    newItem.MediaId = book.Isbn;
                    await Items.AddAsync(newItem);
                }
            }
            foreach (var movie in movieList)
            {
                if (!itemList.Any(i => i.MediaId == movie.Id))
                {
                    var newItem = new SearchResult();
                    newItem = movie.Adapt<SearchResult>();
                    newItem.Id = 0;
                    newItem.MediaId = movie.Id;
                    await Items.AddAsync(newItem);
                }
            }
            foreach (var musicAlbum in musicAlbumList)
            {
                if (!itemList.Any(i => i.MediaId == musicAlbum.Id))
                {
                    var newItem = new SearchResult();
                    newItem = musicAlbum.Adapt<SearchResult>();
                    newItem.Id = 0;
                    newItem.MediaId = musicAlbum.Id;
                    await Items.AddAsync(newItem);
                }
            }
        }
    }
}