using Core.HttpDynamo;
using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.Services;
using Search.CustomGoogle.Interfaces;
using Search.CustomGoogle.Models;

namespace Search.CustomGoogle.Services
{
    public class CustomGoogleService : ICustomGoogleService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string googleApiKey;
        private readonly string googleCseId;

        public CustomGoogleService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            googleApiKey = configuration.GetSection("GoogleSearch:ApiKey").Value;
            googleCseId = configuration.GetSection("GoogleSearch:EngineId").Value;
        }

        public async Task<List<string>> GetPages(SearchResponse searchResponse)
        {
            List<string> pages = new List<string>();

            foreach(var item in searchResponse.Items)
            {
                var response = await HttpDynamo.GetRequestAsync(_httpClientFactory, item.Link);

                var streamReader = new StreamReader(response);
                var read = streamReader.ReadToEnd();

                if(response != null)
                    pages.Add(read);
            }

            return pages;
        }

        public SearchResponse Search(string query)
        {
            var svc = new CustomSearchAPIService(new BaseClientService.Initializer { ApiKey = googleApiKey });
            var listRequest = svc.Cse.List();
            listRequest.Q = query;
            listRequest.Cx = googleCseId;

            var result = listRequest.Execute();

            var searchResponse = new SearchResponse()
            {
                Items = new List<Item>()
            };

            foreach (var item in result.Items)
            {
                var newItem = new Item();
                newItem.DisplayLink = item.DisplayLink;
                newItem.FormattedUrl = item.FormattedUrl;
                newItem.HtmlFormattedUrl = item.HtmlFormattedUrl;
                newItem.HtmlSnippet = item.HtmlSnippet;
                newItem.HtmlTitle = item.HtmlTitle;
                newItem.Link = item.Link;
                newItem.Snippet = item.Snippet;
                newItem.Title = item.Title;

                searchResponse.Items.Add(newItem);
            }
            
            return searchResponse;
        }
    }
}
