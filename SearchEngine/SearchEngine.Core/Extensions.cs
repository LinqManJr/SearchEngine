using SearchEngine.Core.Configurations;
using System;

namespace SearchEngine.Core
{
    public static class Extensions
    {  
        public static string ToYandexSearchUrl(this SearchConfig config, string searchPattern)
        {
            string url = string.Concat(config.Url, $"?user={config.UserName}", "&key=", config.ApiKey);
            return string.Concat(url, "&query=", Uri.EscapeDataString(searchPattern));
        }
    }
}
