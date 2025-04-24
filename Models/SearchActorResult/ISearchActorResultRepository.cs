namespace medienVerwaltungDbSolution.Models.SearchActorResult
{
    public interface ISearchActorResultRepository
    {
        Task<SearchActorsResult?> GetByIdAsync(int ID);
        Task AddAsync(SearchActorsResult searchActorsResult);
        void Update(SearchActorsResult searchActorsResult);
        void Remove(SearchActorsResult searchActorsResult);
    }
}