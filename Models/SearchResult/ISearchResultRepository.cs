namespace medienVerwaltungDbSolution.Models.SearchResult
{
    public interface ISearchResultRepository
    {
        Task<SearchResult?> GetByIdAsync(int ID);
        Task AddAsync(SearchResult searchResult);
        void Update(SearchResult searchResult);
        void Remove(SearchResult searchResult);
    }
}