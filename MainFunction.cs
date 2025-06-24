using medienVerwaltungDbSolution.Models.Book;
using medienVerwaltungDbSolution.Models.Interpret;
using medienVerwaltungDbSolution.Models.Movie;
using medienVerwaltungDbSolution.Models.MusicAlbum;
using medienVerwaltungDbSolution.Models.SearchActorResult;
using medienVerwaltungDbSolution.Models.Song;
using medienVerwaltungDbSolution.Services;
using Microsoft.EntityFrameworkCore;

namespace medienVerwaltungDbSolution
{
    public class MainFunction
    {
        private static DatabaseContext context = new();
        public UnitOfWork unitOfWork = new(context);
        public MainFunction()
        {
            // ClearWholeDb();
            // Seeding();
            var newActor = new SearchActorsResult()
            {
                FirstName = "Test",
                LastName = "TEst2",
                Gender = "man",
                BirthDate = DateTime.Now,
                MovieIDs = [1, 2],
            };

            unitOfWork.Add(newActor);
            unitOfWork.BeginTransactionAsync().Wait();

            System.Console.WriteLine(context.Actors.AsNoTracking().ToList().Count());
        }

        private readonly Func<DatabaseContext, IEnumerable<Song>> _songCompiledQuery =
        EF.CompileQuery((DatabaseContext context) =>
        context.Songs.AsNoTracking()
        .Select(s => new Song
        {
            Id = s.Id,
            Title = s.Title
        })
        );
        private string ShowMenu()
        {
            System.Console.WriteLine("\nMenü:");
            System.Console.WriteLine("1. Hinzufügen");
            System.Console.WriteLine("2. Updaten");
            System.Console.WriteLine("3. Entfernen");
            System.Console.WriteLine("4. Liste anzeigen");
            System.Console.WriteLine("5. Beenden\n");

            var input = Console.ReadLine() ?? string.Empty;
            System.Console.WriteLine();

            return input;
        }
        private void Seeding()
        {
            var Books = context.Books.AsNoTracking().ToList();
            var Songs = context.Songs.AsNoTracking().ToList();
            var Movies = context.Movies.AsNoTracking().ToList();
            var MusicAlbums = context.MusicAlbums.AsNoTracking().ToList();
            var Interprets = context.Interprets.AsNoTracking().ToList();
            var Actors = context.Actors.AsNoTracking().ToList();

            if (Interprets.Count == 0)
            {
                var newInterpret = new Interpret()
                {
                    FirstName = "Max",
                    Name = "Mustermann",
                    BirthDate = new DateTime(2000, 1, 1),
                    Gender = "Mann"
                };

                unitOfWork.Add(newInterpret);
            }
            if (Books.Count == 0)
            {
                var newBook = new Book()
                {
                    InterpretID = 1,
                    Description = "Test",
                    Title = "TestBuch",
                    Location = "TestOrt",
                    PageCount = 250,
                    ReleaseYear = 2020,
                    InterpretFullName = "Max Mustermann"
                };
                unitOfWork.Add(newBook);
            }
            if (Songs.Count == 0)
            {
                var newSong = new Song()
                {
                    InterpretID = 1,
                    Length = 1,
                    Title = "testTitel",
                    Location = "Regal 5",
                    InterpretFullName = "Max Mustermann"
                };
                unitOfWork.Add(newSong);
            }
            if (Movies.Count == 0)
            {
                var newMovie = new Movie()
                {
                    Description = "TestMovie",
                    Genre = "Testen",
                    Length = 50,
                    ReleaseYear = 2000,
                    Title = "Test",
                    Location = "Test Location",
                    ActorIDs = { 1, 2 }
                };
                unitOfWork.Add(newMovie);
            }
            if (MusicAlbums.Count == 0)
            {
                var newMusicAlbum = new MusicAlbum()
                {
                    Title = "Test",
                    Location = "Test",
                    InterpretID = 1,
                    InterpretFullName = "Max Mustermann",
                    SongIdList = { 1, 2 }
                };
                unitOfWork.Add(newMusicAlbum);
            }
            if (Actors.Count == 0)
            {
                var newActor = new SearchActorsResult()
                {
                    FirstName = "Max",
                    LastName = "Mustermann",
                    BirthDate = new DateTime(2000, 1, 1),
                    Gender = "Mann",
                    MovieIDs = { 1 }
                };
                unitOfWork.Add(newActor);
            }

            unitOfWork.BeginTransactionAsync().Wait();
        }
        private void ClearWholeDb()
        {
            foreach (var song in context.Songs.AsNoTracking().ToList())
            {
                unitOfWork.Remove(song);
            }
            foreach (var book in context.Books.AsNoTracking().ToList())
            {
                unitOfWork.Remove(book);
            }
            foreach (var movie in context.Movies.AsNoTracking().ToList())
            {
                unitOfWork.Remove(movie);
            }
            foreach (var musicAlbum in context.MusicAlbums.AsNoTracking().ToList())
            {
                unitOfWork.Remove(musicAlbum);
            }
            foreach (var actor in context.Actors.AsNoTracking().ToList())
            {
                unitOfWork.Remove(actor);
            }
            foreach (var interpret in context.Interprets.AsNoTracking().ToList())
            {
                unitOfWork.Remove(interpret);
            }
            foreach (var item in context.Items.AsNoTracking().ToList())
            {
                unitOfWork.Remove(item);
            }

            unitOfWork.BeginTransactionAsync().Wait();
            System.Console.WriteLine("Cleared everything");
        }
        private void ClearWholeDbManually()
        {
            foreach (var song in context.Songs.AsNoTracking().ToList())
            {
                context.Remove(song);
            }
            foreach (var book in context.Books.AsNoTracking().ToList())
            {
                context.Remove(book);
            }
            foreach (var movie in context.Movies.AsNoTracking().ToList())
            {
                context.Remove(movie);
            }
            foreach (var album in context.MusicAlbums.AsNoTracking().ToList())
            {
                context.Remove(album);
            }
            foreach (var interpret in context.Interprets.AsNoTracking().ToList())
            {
                context.Remove(interpret);
            }
            foreach (var actor in context.Actors.AsNoTracking().ToList())
            {
                context.Remove(actor);
            }
            foreach (var item in context.Items.AsNoTracking().ToList())
            {
                context.Remove(item);
            }
            var output = context.SaveChanges();
            System.Console.WriteLine("Changes: " + output);
        }
        private void UpdateList(object? sender, EventArgs e)
        {
            System.Console.WriteLine("List got updated");
        }
        private void Seed100Songs()
        {
            var interpret = new Interpret();
            try
            {
                interpret = context.Interprets.AsNoTracking().FirstOrDefault();
                if (interpret == null)
                {
                    throw new Exception("There is no Interpret");
                }
            }
            catch (System.Exception)
            {
                interpret = new Interpret
                {
                    FirstName = "Max",
                    Name = "Mustermann",
                    BirthDate = new DateTime(2000, 1, 1),
                    Gender = "Mann"
                };
                System.Console.WriteLine("Adding Interpret");
                unitOfWork.Add(interpret);
                unitOfWork.BeginTransactionAsync().Wait();
            }

            System.Console.WriteLine("Seeding 99 Songs");
            for (int i = 1; i < 100; i++)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var newSong = new Song()
                {
                    Title = "TestSong " + i,
                    Location = "TestLocation " + i,
                    Length = i,
                    InterpretFullName = "Max Mustermann",
                    InterpretID = interpret.Id
                };
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                unitOfWork.Add(newSong);
            }
            unitOfWork.BeginTransactionAsync().Wait();
        }
        private List<Song> PaginationTest(int selectedPage = 1, int itemsPerPage = 10)
        {
            var songs = _songCompiledQuery(context).ToList();
            var paginatedSongs = songs.Skip((selectedPage - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            return paginatedSongs;
        }
    }
}