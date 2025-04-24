
using medienVerwaltungDbSolution.Services;

namespace medienVerwaltungDbSolution.Models.SearchActorResult
{
    public class SearchActorResultRepository(DatabaseContext context) : ISearchActorResultRepository
    {
        private readonly DatabaseContext _context = context;

        public async Task<SearchActorsResult?> GetByIdAsync(int ID)
        {
            return await _context.Actors.FindAsync(ID);
        }
        public async Task AddAsync(SearchActorsResult actor)
        {
            await _context.Actors.AddAsync(actor);
        }

        public void Update(SearchActorsResult actor)
        {
            _context.Actors.Update(actor);
        }

        public void Remove(SearchActorsResult actor)
        {
            _context.Actors.Remove(actor);
        }
    }
}