using Microsoft.Extensions.Hosting;
using SearchEngine.Core.Configurations;
using SearchEngine.Core.Models;
using System.Collections.Generic;

namespace SearchEngine.Tests
{
    public static class DefaultConfigs
    {
        public static YandexSearchOptions YaOptions => new YandexSearchOptions 
        { 
            Name = "yandex", 
            Uri = "https://yandex.com/search/xml", 
            Apikey = "", 
            Username = "" 
        };

        public static GoogleSearchOptions GoogleOptions => new GoogleSearchOptions 
        { 
            Name = "google", 
            Uri = string.Empty,
            Apikey = "", 
            AppId = "" 
        };

        public static SearchEngineOptions BingOptions => new SearchEngineOptions 
        { 
            Name = "bing", 
            Uri = "https://api.cognitive.microsoft.com/bing/v7.0/search", 
            Apikey = ""
        };

        public static IList<ItemResult> GetDefaultItemsOfResult => new List<ItemResult>
        {
            new ItemResult("title1","link1"),
            new ItemResult("title2","link2"),
            new ItemResult("title3","link3"),
            new ItemResult("title4","link4"),
            new ItemResult("title5","link5"),
            new ItemResult("title6","link6"),
            new ItemResult("title7","link7"),
            new ItemResult("title8","link8"),
            new ItemResult("title9","link9"),
            new ItemResult("title10","link10")
        };

        public static ErrorItem GetDefaultErrorItem => new ErrorItem("BadRequest", "Something went wrong");
          
    }
}
