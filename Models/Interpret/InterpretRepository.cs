
using medienVerwaltungDbSolution.Services;

namespace medienVerwaltungDbSolution.Models.Interpret
{
    public class InterpretRepository(DatabaseContext context) : IInterpretRepository
    {
        private readonly DatabaseContext _context = context;

        public async Task<Interpret?> GetByIdAsync(int ID)
        {
            return await _context.Interprets.FindAsync(ID);
        }
        public async Task AddAsync(Interpret interpret)
        {
            await _context.Interprets.AddAsync(interpret);
        }

        public void Update(Interpret interpret)
        {
            _context.Interprets.Update(interpret);
        }

        public void Remove(Interpret interpret)
        {
            _context.Interprets.Remove(interpret);
        }
    }
}