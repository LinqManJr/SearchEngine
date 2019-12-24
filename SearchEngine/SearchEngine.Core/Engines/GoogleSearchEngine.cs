using Google;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using Microsoft.Extensions.Options;
using SearchEngine.Core.Configurations;
using SearchEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Google.Apis.Customsearch.v1.CseResource;

namespace SearchEngine.Core.Engines
{
    public class GoogleSearchEngine : ISearchEngine
    {        
        private readonly GoogleSearchOptions _options;        

        public GoogleSearchEngine(GoogleSearchOptions options)
        {
            _options = options;
        }

        public GoogleSearchEngine(IOptions<GoogleSearchOptions> options)
        {
            _options = options.Value;
        }

        public SearchResult Search(string pattern)
        {
            var searchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = _options.Apikey });
            var listOfRequest = searchService.Cse.List(pattern);
            listOfRequest.Cx = _options.AppId;
            var listOfItems = new List<ItemResult>();

            long? totalResults;            

            try
            {
                var tuple = GetResults(listOfRequest, _options.NumItems).Result;
                listOfItems = tuple.results;
                totalResults = tuple.totalCount;           
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
                CountResult = totalResults,
                Results = listOfItems
            };
        }

        public async Task<SearchResult> SearchAsync(string pattern)
        {
            var searchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = _options.Apikey });
            var listOfRequest = searchService.Cse.List(pattern);
            listOfRequest.Cx = _options.AppId;
            var listOfItems = new List<ItemResult>();

            long? totalResults;
            
            try
            {                
                var tuple = await GetResults(listOfRequest, _options.NumItems);
                listOfItems = tuple.results;
                totalResults = tuple.totalCount;                
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
                CountResult = totalResults,                
                Results = listOfItems
            };
        }

        private async Task<(List<ItemResult> results, long? totalCount)> GetResults(ListRequest listRequest, long count)
        {
            var list = new List<ItemResult>();
            var prime = count / 10;
            var addit = count % 10;
            Google.Apis.Customsearch.v1.Data.Search data = null;

            listRequest.Start = 1;
            listRequest.Num = 10;

            for (int i = 0; i < prime; i++)
            {                               
                data = await listRequest.ExecuteAsync();
                listRequest.Start += 10;

                list.AddRange(data.Items.Select(x => new ItemResult(x.Title, x.Link)));
            }

            if(addit != 0)
            {
                listRequest.Num = addit;                
                data =  await listRequest.ExecuteAsync();
                list.AddRange(data.Items.Select(x => new ItemResult(x.Title, x.Link)));
            }            

            return (list, data?.SearchInformation.TotalResults ?? 0);
        }
    }
}
