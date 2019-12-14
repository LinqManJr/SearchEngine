using SearchEngine.Core.Configurations;
using SearchEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SearchEngine.Core.Engines
{
    public class YandexSearchEngine : ISearchEngine
    {       
        private readonly SearchConfig config;
        private readonly YandexSearchOptions _options;

        public YandexSearchEngine(SearchConfig config)
        {
            this.config = config;
        }
        public YandexSearchEngine(YandexSearchOptions options)
        {
            _options = options;
        }

        public SearchResult Search(string pattern)
        {
            string urlQuery = _options.ToYandexSearchUrl(pattern);

            var webRequest = WebRequest.Create(urlQuery);
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponseAsync().Result;

            using (Stream dataStream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    using (XmlReader xmlReader = XmlReader.Create(dataStream))
                    {
                        XDocument xDoc = XDocument.Load(xmlReader);

                        return ParseResult(xDoc);
                    }
                }
            }        
            
        }

        public async Task<SearchResult> SearchAsync(string pattern)
        {
            string urlQuery = _options.ToYandexSearchUrl(pattern);

            var webRequest = WebRequest.Create(urlQuery);
            var response = (HttpWebResponse) await webRequest.GetResponseAsync();

            using (Stream dataStream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    using (XmlReader xmlReader = XmlReader.Create(dataStream, new XmlReaderSettings { Async = true}))
                    {                        
                        XDocument xDoc = await XDocument.LoadAsync(xmlReader, LoadOptions.None, CancellationToken.None);

                        return ParseResult(xDoc);
                    }
                }
            }
            
        }

        private SearchResult ParseResult(XDocument xDoc)
        {
            if (xDoc.Root == null)
                return null;            
            
            if (!TryParse(xDoc, out ErrorItem error))
                return new SearchResult() { Error = error };

            var itemsResult = new List<ItemResult>();

            var countResult = xDoc.Elements("yandexsearch").Elements("response").Elements("found")
                                .FirstOrDefault(x => x.Attribute("priority").Value == "all").Value;

            var elements = from element in xDoc.Elements("yandexsearch").Elements("response").Elements("results")
                                                .Elements("grouping").Elements("group")
                           select element;
            
            foreach(var element in elements)
            {
                var docEl = element.Element("doc");
                var title = docEl.Element("title").HasElements ? docEl.Element("title").Element("hlword").Value 
                                                               : docEl.Element("title").Value;
                var link = docEl.Element("url").Value;

                itemsResult.Add(new ItemResult(link, title));
            }                

            return new SearchResult { CountResult = long.Parse(countResult), Results = itemsResult, Error = error, SearchTitle = _options.Name};
            
        }
        private bool TryParse(XDocument xDoc, out ErrorItem errorResult)
        {            
            var response = xDoc.Element("yandexsearch").Element("response");
            if (response.Element("error") == null)
            {
                errorResult = null;
                return true;
            }

            var code = response.Element("error").Attribute("code").Value;
            var value = response.Element("error").Value;
            errorResult = new ErrorItem($"Error Code is {code}", value);
            return false;
        }

    }
}
