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
    public class DbChanges : IDbChanges
    {
        private readonly List<SongUOW> SongList = [];
        private readonly List<BookUOW> BookList = [];
        private readonly List<MovieUOW> MovieList = [];
        private readonly List<MusicAlbumUOW> MusicAlbumsList = [];
        private readonly List<SearchResultUOW> ItemList = [];
        private readonly List<SearchActorResultUOW> ActorList = [];
        private readonly List<InterpretUOW> InterpretList = [];
        private readonly UnitOfWork unitOfWork;
        readonly DatabaseContext _context;
        public DbChanges(DatabaseContext context)
        {
            _context = context;
            unitOfWork = new(_context);
        }
        public void Add<T>(T entity) where T : class
        {
            switch (entity)
            {
                case Song:
                    var newSong = new SongUOW()
                    {
                        Session = SessionType.Add,
                        Song = entity as Song ?? throw new Exception("Entity is not a Song")
                    };
                    // newSong.Song.Id = _context.Songs.AsNoTracking().Count() + SongList.Count + 1;

                    var newSongItem = new SearchResultUOW()
                    {
                        Session = SessionType.Add,
                        SearchResult = new SearchResult()
                        {
                            // Id = _context.Items.AsNoTracking().Count() + ItemList.Count + 1,
                            Location = newSong.Song.Location,
                            MediaId = newSong.Song.Id,
                            MediaType = MediaType.Song,
                            Title = newSong.Song.Title
                        }
                    };

                    ItemList.Add(newSongItem);
                    SongList.Add(newSong);
                    break;
                case Book:
                    var newBook = new BookUOW()
                    {
                        Session = SessionType.Add,
                        Book = entity as Book ?? throw new Exception("Entity is not a Book")
                    };
                    // newBook.Book.Isbn = _context.Books.AsNoTracking().Count() + BookList.Count + 1;

                    var newBookItem = new SearchResultUOW()
                    {
                        Session = SessionType.Add,
                        SearchResult = new SearchResult()
                        {
                            // Id = _context.Items.AsNoTracking().Count() + ItemList.Count + 1,
                            Location = newBook.Book.Location,
                            MediaId = newBook.Book.Isbn,
                            MediaType = MediaType.Buch,
                            Title = newBook.Book.Title
                        }
                    };

                    ItemList.Add(newBookItem);
                    BookList.Add(newBook);
                    break;
                case Movie:
                    var newMovie = new MovieUOW()
                    {
                        Session = SessionType.Add,
                        Movie = entity as Movie ?? throw new Exception("Entity is not a Movie")
                    };
                    // newMovie.Movie.Id = _context.Movies.AsNoTracking().Count() + MovieList.Count + 1;

                    var newMovieItem = new SearchResultUOW()
                    {
                        Session = SessionType.Add,
                        SearchResult = new SearchResult()
                        {
                            // Id = _context.Items.AsNoTracking().Count() + ItemList.Count + 1,
                            Location = newMovie.Movie.Location,
                            MediaId = newMovie.Movie.Id,
                            MediaType = MediaType.Film,
                            Title = newMovie.Movie.Title
                        }
                    };

                    ItemList.Add(newMovieItem);
                    MovieList.Add(newMovie);
                    break;
                case MusicAlbum:
                    var newMusicAlbum = new MusicAlbumUOW()
                    {
                        Session = SessionType.Add,
                        Album = entity as MusicAlbum ?? throw new Exception("Entity is not a MusicAlbum")
                    };

                    var newAlbumItem = new SearchResultUOW()
                    {
                        Session = SessionType.Add,
                        SearchResult = new SearchResult()
                        {
                            Location = newMusicAlbum.Album.Location,
                            MediaId = newMusicAlbum.Album.Id,
                            MediaType = MediaType.Musikalbum,
                            Title = newMusicAlbum.Album.Title
                        }
                    };

                    ItemList.Add(newAlbumItem);
                    MusicAlbumsList.Add(newMusicAlbum);
                    break;
                case SearchActorsResult:
                    var newActor = new SearchActorResultUOW()
                    {
                        Session = SessionType.Add,
                        SearchActorsResult = entity as SearchActorsResult ?? throw new Exception("Entity is not a SearchActorsResult")
                    };
                    ActorList.Add(newActor);
                    break;
                case SearchResult:
                    var newItem = new SearchResultUOW()
                    {
                        Session = SessionType.Add,
                        SearchResult = entity as SearchResult ?? throw new Exception("Entity is not a SearchResult")
                    };
                    ItemList.Add(newItem);
                    break;
                case Interpret:
                    var newInterpret = new InterpretUOW()
                    {
                        Session = SessionType.Add,
                        Interpret = entity as Interpret ?? throw new Exception("Entity is not Interpret!")
                    };
                    InterpretList.Add(newInterpret);
                    break;
                default:
                    break;
            }
        }
        public void Remove<T>(T entity) where T : class
        {
            var itemList = _context.Items.AsNoTracking().ToList();
            switch (entity)
            {
                case Song:
                    var newSong = new SongUOW()
                    {
                        Session = SessionType.Remove,
                        Song = entity as Song ?? throw new Exception("Entity is not a Song")
                    };

                    var newSongItem = new SearchResultUOW()
                    {
                        SearchResult = itemList.FirstOrDefault(i => i.MediaId == newSong.Song.Id)
                                                ?? throw new Exception("No Song Item found"),
                        Session = SessionType.Remove
                    };

                    ItemList.Add(newSongItem);
                    SongList.Add(newSong);
                    break;
                case Book:
                    var newBook = new BookUOW()
                    {
                        Session = SessionType.Remove,
                        Book = entity as Book ?? throw new Exception("Entity is not a Book")
                    };

                    var newBookItem = new SearchResultUOW()
                    {
                        SearchResult = _context.Items.AsNoTracking()
                                                        .FirstOrDefault(i => i.MediaId == newBook.Book.Isbn)
                                                        ?? throw new Exception("Book item not found"),
                        Session = SessionType.Remove
                    };

                    ItemList.Add(newBookItem);
                    BookList.Add(newBook);
                    break;
                case Movie:
                    var newMovie = new MovieUOW()
                    {
                        Session = SessionType.Remove,
                        Movie = entity as Movie ?? throw new Exception("Entity is not a Movie")
                    };

                    var newMovieItem = new SearchResultUOW()
                    {
                        SearchResult = _context.Items.AsNoTracking()
                                                        .FirstOrDefault(i => i.MediaId == newMovie.Movie.Id)
                                                        ?? throw new Exception("Movie Item not found"),
                        Session = SessionType.Remove
                    };

                    ItemList.Add(newMovieItem);
                    MovieList.Add(newMovie);
                    break;
                case MusicAlbum:
                    var newMusicAlbum = new MusicAlbumUOW()
                    {
                        Session = SessionType.Remove,
                        Album = entity as MusicAlbum ?? throw new Exception("Entity is not a MusicAlbum")
                    };
                    var newAlbumItem = new SearchResultUOW()
                    {
                        SearchResult = _context.Items.AsNoTracking()
                                                        .FirstOrDefault(i => i.MediaId == newMusicAlbum.Album.Id)
                                                        ?? throw new Exception("Album item not found"),
                        Session = SessionType.Remove
                    };

                    ItemList.Add(newAlbumItem);
                    MusicAlbumsList.Add(newMusicAlbum);
                    break;
                case SearchActorsResult:
                    var newActor = new SearchActorResultUOW()
                    {
                        Session = SessionType.Remove,
                        SearchActorsResult = entity as SearchActorsResult ?? throw new Exception("Entity is not a SearchActorsResult")
                    };
                    ActorList.Add(newActor);
                    break;
                case SearchResult:
                    var newItem = new SearchResultUOW()
                    {
                        Session = SessionType.Remove,
                        SearchResult = entity as SearchResult ?? throw new Exception("Entity is not a SearchResult")
                    };
                    ItemList.Add(newItem);
                    break;
                case Interpret:
                    var newInterpret = new InterpretUOW()
                    {
                        Session = SessionType.Remove,
                        Interpret = entity as Interpret ?? throw new Exception("Entity is not a Interpret")
                    };
                    InterpretList.Add(newInterpret);
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
                    var newSong = new SongUOW()
                    {
                        Session = SessionType.Update,
                        Song = entity as Song ?? throw new Exception("Entity is not a Song")
                    };

                    var newSongItem = new SearchResultUOW()
                    {
                        SearchResult = _context.Items
                                               .AsNoTracking()
                                               .FirstOrDefault(i => i.MediaId == newSong.Song.Id && i.MediaType == MediaType.Song)
                                               ?? throw new Exception("Songitem was not found"),
                        Session = SessionType.Update
                    };
                    newSongItem.SearchResult.Location = newSong.Song.Location;
                    newSongItem.SearchResult.Title = newSong.Song.Title;

                    ItemList.Add(newSongItem);
                    SongList.Add(newSong);
                    break;
                case Book:
                    var newBook = new BookUOW()
                    {
                        Session = SessionType.Update,
                        Book = entity as Book ?? throw new Exception("Entity is not a Book")
                    };
                    var newBookItem = new SearchResultUOW()
                    {
                        SearchResult = _context.Items
                                               .AsNoTracking()
                                               .FirstOrDefault(i => i.MediaId == newBook.Book.Isbn && i.MediaType == MediaType.Buch)
                                               ?? throw new Exception("Book Item was not found"),
                        Session = SessionType.Update
                    };
                    newBookItem.SearchResult.Title = newBook.Book.Title;
                    newBookItem.SearchResult.Location = newBook.Book.Location;

                    ItemList.Add(newBookItem);
                    BookList.Add(newBook);
                    break;
                case Movie:
                    var newMovie = new MovieUOW()
                    {
                        Session = SessionType.Update,
                        Movie = entity as Movie ?? throw new Exception("Entity is not a Movie")
                    };
                    var newMovieItem = new SearchResultUOW()
                    {
                        SearchResult = _context.Items
                                               .AsNoTracking()
                                               .FirstOrDefault(i => i.MediaId == newMovie.Movie.Id && i.MediaType == MediaType.Film)
                                               ?? throw new Exception("Movie Item was not found"),
                        Session = SessionType.Update
                    };

                    newMovieItem.SearchResult.Title = newMovie.Movie.Title;
                    newMovieItem.SearchResult.Location = newMovie.Movie.Location;

                    ItemList.Add(newMovieItem);
                    MovieList.Add(newMovie);
                    break;
                case MusicAlbum:
                    var newMusicAlbum = new MusicAlbumUOW()
                    {
                        Session = SessionType.Update,
                        Album = entity as MusicAlbum ?? throw new Exception("Entity is not a MusicAlbum")
                    };
                    var newMusicAlbumItem = new SearchResultUOW()
                    {
                        SearchResult = _context.Items
                                               .AsNoTracking()
                                               .FirstOrDefault(i => i.MediaId == newMusicAlbum.Album.Id && i.MediaType == MediaType.Musikalbum)
                                               ?? throw new Exception("Album Item was not found"),
                        Session = SessionType.Update
                    };

                    newMusicAlbumItem.SearchResult.Title = newMusicAlbum.Album.Title;
                    newMusicAlbumItem.SearchResult.Location = newMusicAlbum.Album.Location;

                    ItemList.Add(newMusicAlbumItem);
                    MusicAlbumsList.Add(newMusicAlbum);
                    break;
                case SearchActorsResult:
                    var newActor = new SearchActorResultUOW()
                    {
                        Session = SessionType.Update,
                        SearchActorsResult = entity as SearchActorsResult ?? throw new Exception("Entity is not a SearchActorsResult")
                    };
                    ActorList.Add(newActor);
                    break;
                case SearchResult:
                    var newItem = new SearchResultUOW()
                    {
                        Session = SessionType.Update,
                        SearchResult = entity as SearchResult ?? throw new Exception("Entity is not a SearchResult")
                    };
                    ItemList.Add(newItem);
                    break;
                default:
                    break;
            }
        }
        public async Task<Book> GetBookByIdAsync(int Id)
        {
            return await unitOfWork.Books.GetByIdAsync(Id) ?? throw new Exception("Book not found");
        }
        public async Task<Song> GetSongByIdAsync(int Id)
        {
            return await unitOfWork.Songs.GetByIdAsync(Id) ?? throw new Exception("Song not found");
        }
        public async Task<Movie> GetMovieByIdAsync(int Id)
        {
            return await unitOfWork.Movies.GetByIdAsync(Id) ?? throw new Exception("Movie not found");
        }
        public async Task<MusicAlbum> GetMusicAlbumByIdAsync(int Id)
        {
            return await unitOfWork.MusicAlbums.GetByIdAsync(Id) ?? throw new Exception("MusicAlbum not found");
        }
        public async Task<Interpret> GetInterpretByIdAsync(int Id)
        {
            return await unitOfWork.Interprets.GetByIdAsync(Id) ?? throw new Exception("Interpret not found");
        }
        public async Task<SearchResult> GetItemByIdAsync(int Id)
        {
            return await unitOfWork.Items.GetByIdAsync(Id) ?? throw new Exception("Item not found");
        }
        public async Task<SearchActorsResult> GetActorByIdAsync(int Id)
        {
            return await unitOfWork.Actors.GetByIdAsync(Id) ?? throw new Exception("Actors not found");
        }
        private async Task AddToDb()
        {
            // Adding the Items to the Db
            foreach (SongUOW song in SongList)
            {
                if (song.Session == SessionType.Add)
                {
                    await unitOfWork.Songs.AddAsync(song.Song);
                }
            }
            foreach (BookUOW book in BookList)
            {
                if (book.Session == SessionType.Add)
                {
                    await unitOfWork.Books.AddAsync(book.Book);
                }
            }
            foreach (MusicAlbumUOW musicAlbum in MusicAlbumsList)
            {
                if (musicAlbum.Session == SessionType.Add)
                {
                    await unitOfWork.MusicAlbums.AddAsync(musicAlbum.Album);
                }
            }
            foreach (MovieUOW movie in MovieList)
            {
                if (movie.Session == SessionType.Add)
                {
                    await unitOfWork.Movies.AddAsync(movie.Movie);
                }
            }
            foreach (InterpretUOW interpret in InterpretList)
            {
                if (interpret.Session == SessionType.Add)
                {
                    await unitOfWork.Interprets.AddAsync(interpret.Interpret);
                }
            }
            foreach (SearchActorResultUOW actor in ActorList)
            {
                if (actor.Session == SessionType.Add)
                {
                    await unitOfWork.Actors.AddAsync(actor.SearchActorsResult);
                }
            }
        }
        private void RemoveFromDb()
        {
            // Removing the Items to the Db
            foreach (SongUOW song in SongList)
            {
                if (song.Session == SessionType.Remove)
                {
                    unitOfWork.Songs.Remove(song.Song);
                }
            }
            foreach (BookUOW book in BookList)
            {
                if (book.Session == SessionType.Remove)
                {
                    unitOfWork.Books.Remove(book.Book);
                }
            }
            foreach (MusicAlbumUOW musicAlbum in MusicAlbumsList)
            {
                if (musicAlbum.Session == SessionType.Remove)
                {
                    unitOfWork.MusicAlbums.Remove(musicAlbum.Album);
                }
            }
            foreach (MovieUOW movie in MovieList)
            {
                if (movie.Session == SessionType.Remove)
                {
                    unitOfWork.Movies.Remove(movie.Movie);
                }
            }
            foreach (InterpretUOW interpret in InterpretList)
            {
                if (interpret.Session == SessionType.Remove)
                {
                    unitOfWork.Interprets.Remove(interpret.Interpret);
                }
            }
            foreach (SearchResultUOW item in ItemList)
            {
                if (item.Session == SessionType.Remove)
                {
                    unitOfWork.Items.Remove(item.SearchResult);
                }
            }
            foreach (SearchActorResultUOW actor in ActorList)
            {
                if (actor.Session == SessionType.Remove)
                {
                    unitOfWork.Actors.Remove(actor.SearchActorsResult);
                }
            }

            // Clearing the Sessionlists
            SongList.RemoveAll(s => s.Session == SessionType.Remove);
            BookList.RemoveAll(b => b.Session == SessionType.Remove);
            MusicAlbumsList.RemoveAll(ma => ma.Session == SessionType.Remove);
            MovieList.RemoveAll(m => m.Session == SessionType.Remove);
            InterpretList.RemoveAll(i => i.Session == SessionType.Remove);
            ItemList.RemoveAll(sr => sr.Session == SessionType.Remove);
            ActorList.RemoveAll(sa => sa.Session == SessionType.Remove);
        }
        private void UpdateToDb()
        {
            // Updating the Items to the Db
            foreach (SongUOW song in SongList)
            {
                if (song.Session == SessionType.Update)
                {
                    unitOfWork.Songs.Update(song.Song);
                }
            }
            foreach (BookUOW book in BookList)
            {
                if (book.Session == SessionType.Update)
                {
                    unitOfWork.Books.Update(book.Book);
                }
            }
            foreach (MusicAlbumUOW musicAlbum in MusicAlbumsList)
            {
                if (musicAlbum.Session == SessionType.Update)
                {
                    unitOfWork.MusicAlbums.Update(musicAlbum.Album);
                }
            }
            foreach (MovieUOW movie in MovieList)
            {
                if (movie.Session == SessionType.Update)
                {
                    unitOfWork.Movies.Update(movie.Movie);
                }
            }
            foreach (InterpretUOW interpret in InterpretList)
            {
                if (interpret.Session == SessionType.Update)
                {
                    unitOfWork.Interprets.Update(interpret.Interpret);
                }
            }
            foreach (SearchResultUOW item in ItemList)
            {
                if (item.Session == SessionType.Update)
                {
                    unitOfWork.Items.Update(item.SearchResult);
                }
            }
            foreach (SearchActorResultUOW actor in ActorList)
            {
                if (actor.Session == SessionType.Update)
                {
                    unitOfWork.Actors.Update(actor.SearchActorsResult);
                }
            }

            // Clearing the Sessionlists
            SongList.RemoveAll(s => s.Session == SessionType.Update);
            BookList.RemoveAll(b => b.Session == SessionType.Update);
            MusicAlbumsList.RemoveAll(ma => ma.Session == SessionType.Update);
            MovieList.RemoveAll(m => m.Session == SessionType.Update);
            InterpretList.RemoveAll(i => i.Session == SessionType.Update);
            ItemList.RemoveAll(sr => sr.Session == SessionType.Update);
            ActorList.RemoveAll(sa => sa.Session == SessionType.Update);
        }
        private void DetachingEntrys()
        {
            foreach (var song in SongList)
            {
                _context.Entry(song.Song).State = EntityState.Detached;
            }

            foreach (var book in BookList)
            {
                _context.Entry(book.Book).State = EntityState.Detached;
            }

            foreach (var movie in MovieList)
            {
                _context.Entry(movie.Movie).State = EntityState.Detached;
            }

            foreach (var musicAlbum in MusicAlbumsList)
            {
                _context.Entry(musicAlbum.Album).State = EntityState.Detached;
            }

            foreach (var interpret in InterpretList)
            {
                _context.Entry(interpret.Interpret).State = EntityState.Detached;
            }

            foreach (var actor in ActorList)
            {
                _context.Entry(actor.SearchActorsResult).State = EntityState.Detached;
            }

            foreach (var item in ItemList)
            {
                _context.Entry(item.SearchResult).State = EntityState.Detached;
            }

            // Clearing the Sessionlists
            SongList.RemoveAll(s => s.Session == SessionType.Add);
            BookList.RemoveAll(b => b.Session == SessionType.Add);
            MusicAlbumsList.RemoveAll(ma => ma.Session == SessionType.Add);
            MovieList.RemoveAll(m => m.Session == SessionType.Add);
            InterpretList.RemoveAll(i => i.Session == SessionType.Add);
            ActorList.RemoveAll(sa => sa.Session == SessionType.Add);
            ItemList.RemoveAll(it => it.Session == SessionType.Add);
        }
        public async Task ExecuteTransactionAsync()
        {
            // using var transaction = await unitOfWork.BeginTransactionAsync();

            try
            {
                await AddToDb();
                RemoveFromDb();
                UpdateToDb();

                // var output = await unitOfWork.SaveChangesAsync();
                await AddItems();
                // var itemsOutput = await unitOfWork.SaveChangesAsync();

                // await transaction.CommitAsync();
                // System.Console.WriteLine("DbChanges: " + output + " ItemsChanges: " + itemsOutput);
                DetachingEntrys();
            }
            catch (System.Exception)
            {
                // await transaction.RollbackAsync();
                System.Console.WriteLine("Rollback");
                throw;
            }
        }
        private async Task AddItems()
        {
            var songs = await _context.Songs.AsNoTracking().Select(s => new Song
            {
                Id = s.Id,
                Title = s.Title
            }).ToListAsync();
            var books = await _context.Books.AsNoTracking().Select(b => new Book
            {
                Isbn = b.Isbn,
                Title = b.Title
            }).ToListAsync();
            var movies = await _context.Movies.AsNoTracking().Select(m => new Movie
            {
                Id = m.Id,
                Title = m.Title
            }).ToListAsync();
            var musicAlbums = await _context.MusicAlbums.AsNoTracking().Select(ma => new MusicAlbum
            {
                Id = ma.Id,
                Title = ma.Title
            }).ToListAsync();

            foreach (SearchResultUOW item in ItemList)
            {
                if (item.Session == SessionType.Add)
                {
                    switch (item.SearchResult.MediaType)
                    {
                        case MediaType.Song:
                            var songWID = songs.FirstOrDefault(s => s.Title == item.SearchResult.Title) ?? throw new Exception("Song for Item not found");
                            item.SearchResult.MediaId = songWID.Id;
                            await unitOfWork.Items.AddAsync(item.SearchResult);
                            break;

                        case MediaType.Buch:
                            var bookWID = books.FirstOrDefault(s => s.Title == item.SearchResult.Title) ?? throw new Exception("Book for Item not found");
                            item.SearchResult.MediaId = bookWID.Isbn;
                            await unitOfWork.Items.AddAsync(item.SearchResult);
                            break;

                        case MediaType.Film:
                            var movieWID = movies.FirstOrDefault(s => s.Title == item.SearchResult.Title) ?? throw new Exception("Movie for Item not found");
                            item.SearchResult.MediaId = movieWID.Id;
                            await unitOfWork.Items.AddAsync(item.SearchResult);
                            break;

                        case MediaType.Musikalbum:
                            var albumWID = musicAlbums.FirstOrDefault(s => s.Title == item.SearchResult.Title) ?? throw new Exception("Album for Item not found");
                            item.SearchResult.MediaId = albumWID.Id;
                            await unitOfWork.Items.AddAsync(item.SearchResult);
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }
}