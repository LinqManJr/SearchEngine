using Google;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using SearchEngine.Core.Configurations;
using SearchEngine.Core.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.Core.Engines
{
    public class GoogleSearchEngine : ISearchEngine
    {        
        private readonly GoogleSearchOptions _options;
                
        public GoogleSearchEngine(GoogleSearchOptions options)
        {
            _options = options;
        }

        public SearchResult Search(string pattern)
        {
            var searchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = _options.Apikey });
            var listOfRequest = searchService.Cse.List(pattern);
            listOfRequest.Cx = _options.AppId;

            Google.Apis.Customsearch.v1.Data.Search data;
            try
            {
                data = listOfRequest.Execute();
            }
            catch (GoogleApiException gEx)
            {
                return new SearchResult { Error = new ErrorItem(gEx.HttpStatusCode.ToString(), gEx.Error.Errors[0].ToString()) };
            }   
            catch(Exception ex)
            {
                return new SearchResult { Error = new ErrorItem("Unsupported Error", ex.Message) };
            }

            return new SearchResult
            {
                SearchTitle = _options.Name,                
                CountResult = data.SearchInformation.TotalResults,                
                Results = data.Items.Select(x => new ItemResult(x.Title, x.Link)).ToList()
            };
        }

        public async Task<SearchResult> SearchAsync(string pattern)
        {
            var searchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = _options.Apikey });
            var listOfRequest = searchService.Cse.List(pattern);
            listOfRequest.Cx = _options.AppId;

            Google.Apis.Customsearch.v1.Data.Search data;
            try
            {
                data = await listOfRequest.ExecuteAsync();
            }
            catch (GoogleApiException gEx)
            {
                return new SearchResult { Error = new ErrorItem(gEx.HttpStatusCode.ToString(), gEx.Error.Errors[0].ToString()) };
            }
            catch (Exception ex)
            {
                return new SearchResult { Error = new ErrorItem("Unsupported Error", ex.Message) };
            }

            return new SearchResult
            {
                SearchTitle = _options.Name,                
                CountResult = data.SearchInformation.TotalResults,                
                Results = data.Items.Select(x => new ItemResult(x.Title, x.Link)).ToList()
            };
        }
    }
}
