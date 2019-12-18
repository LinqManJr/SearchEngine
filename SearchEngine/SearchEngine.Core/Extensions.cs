using SearchEngine.Core.Configurations;
using System;

namespace SearchEngine.Core
{
    public static class Extensions
    {       
        public static string ToYandexSearchUrl(this YandexSearchOptions options, string searchPattern)
        {
            string url = string.Concat(options.Uri, $"?user={options.Username}", "&key=", options.Apikey);
            url = string.Concat(url, $"&groupby=attr%3D%22%22.mode%3Dflat.groups-on-page%3D{options.NumItems}.docs-in-group%3D1");
            return string.Concat(url, "&query=", Uri.EscapeDataString(searchPattern));
        }
    }
}
