using SearchEngine.Core.Configurations;
using System;
using System.Collections.Generic;

namespace SearchEngine.Core.Extensions
{
    public static class Extensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static string ToYandexSearchUrl(this YandexSearchOptions options, string searchPattern)
        {
            string url = string.Concat(options.Uri, $"?user={options.Username}", "&key=", options.Apikey);
            url = string.Concat(url, $"&groupby=attr%3D%22%22.mode%3Dflat.groups-on-page%3D{options.NumItems}.docs-in-group%3D1");
            return string.Concat(url, "&query=", Uri.EscapeDataString(searchPattern));
        }



    }
}
