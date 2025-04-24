
using medienVerwaltungDbSolution.Services;

namespace medienVerwaltungDbSolution.Models.SearchResult
{
    public class SearchResultRepository(DatabaseContext context) : ISearchResultRepository
    {
        private readonly DatabaseContext _context = context;

        public async Task<SearchResult?> GetByIdAsync(int ID)
        {
            return await _context.Items.FindAsync(ID);
        }
        public async Task AddAsync(SearchResult item)
        {
            await _context.Items.AddAsync(item);
        }

        public void Update(SearchResult item)
        {
            _context.Items.Update(item);
        }

        public void Remove(SearchResult item)
        {
            _context.Items.Remove(item);
        }
    }
}