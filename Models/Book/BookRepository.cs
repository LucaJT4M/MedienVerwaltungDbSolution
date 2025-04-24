using medienVerwaltungDbSolution.Services;

namespace medienVerwaltungDbSolution.Models.Book
{
    public class BookRepository(DatabaseContext context) : IBookRepository
    {
        private readonly DatabaseContext _context = context;

        public async Task<Book?> GetByIdAsync(int ID)
        {
            return await _context.Books.FindAsync(ID);
        }
        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
        }

        public void Remove(Book book)
        {
            _context.Books.Remove(book);
        }
    }
}