using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using SearchEngine.Core.Configurations;
using SearchEngine.Core.Models;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SearchEngine.Core.Engines
{
    public class BingSearchEngine : ISearchEngine
    {        
        private readonly SearchEngineOptions _options;        
                       
        public BingSearchEngine(SearchEngineOptions options)
        {
            _options = options;
        }

        public BingSearchEngine(IOptions<SearchEngineOptions> options)
        {
            _options = options.Value;
        }

        public SearchResult Search(string pattern)
        {
            var uriQuery = _options.Uri + "?q=" + Uri.EscapeDataString(pattern);
            uriQuery = string.Concat(uriQuery, $"&count={_options.NumItems}");

            WebRequest request = WebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = _options.Apikey;
            try 
            { 
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string json = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return Parse(json);
            }
            catch(WebException wExc)
            {
                return new SearchResult { Error = new ErrorItem(wExc.Status.ToString(), wExc.Message) };
            }
            catch(Exception exc)
            {
                return new SearchResult { Error = new ErrorItem("Unsupported Exception", exc.Message) };
            }                        
        }

        private SearchResult Parse(string json)
        {
            var jObject = JObject.Parse(json);
            var countResult = (long)jObject["webPages"]["totalEstimatedMatches"];
            var results = from x in jObject["webPages"]["value"]
                          select new ItemResult((string)x["name"], (string)x["url"]);

            return new SearchResult { CountResult = countResult, Results = results.ToList(), SearchTitle = _options.Name};
        }

        public async Task<SearchResult> SearchAsync(string pattern)
        {
            var uriQuery = _options.Uri + "?q=" + Uri.EscapeDataString(pattern);
            uriQuery = string.Concat(uriQuery, $"&count={_options.NumItems}");

            WebRequest request = WebRequest.Create(uriQuery);
            request.Headers["Ocp-Apim-Subscription-Key"] = _options.Apikey;
            try
            {
                HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync();
                string json = await new StreamReader(response.GetResponseStream()).ReadToEndAsync();
                return Parse(json);
            }
            catch (WebException wExc)
            {
                return new SearchResult { Error = new ErrorItem(wExc.Status.ToString(), wExc.Message) };
            }
            catch (Exception exc)
            {
                return new SearchResult { Error = new ErrorItem("Unsupported Exception", exc.Message) };
            }
        }
    }
}
