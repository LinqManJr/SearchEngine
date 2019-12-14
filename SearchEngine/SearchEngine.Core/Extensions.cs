using SearchEngine.Core.Configurations;
using System;

namespace SearchEngine.Core
{
    public static class Extensions
    {       
        public static string ToYandexSearchUrl(this YandexSearchOptions options, string searchPattern)
        {
            string url = string.Concat(options.Uri, $"?user={options.Username}", "&key=", options.Apikey);
            return string.Concat(url, "&query=", Uri.EscapeDataString(searchPattern));
        }
    }
}
