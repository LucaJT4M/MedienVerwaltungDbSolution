namespace medienVerwaltungDbSolution.Models.Book
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(int ID);
        Task AddAsync(Book book);
        void Update(Book book);
        void Remove(Book book);
    }
}