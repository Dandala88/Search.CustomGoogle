using Search.CustomGoogle.Models;

namespace Search.CustomGoogle.Interfaces
{
    public interface ICustomGoogleService
    {
        public SearchResponse Search(string query, int limit);
        public Task<List<string>> GetPages(SearchResponse searchResponse);
    }
}
