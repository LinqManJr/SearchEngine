using System.Collections.Generic;

namespace SearchEngine.Core.Models
{
    public class SearchResult
    {
        public string SearchTitle { get; set; }
        public long? CountResult { get; set; }

        public ErrorItem Error { get; set; }
        public IList<ItemResult> Results { get; set; }
    }

    public class ItemResult
    {
        public string ActionLink { get; set; }
        public string SiteTitle { get; set; }

        public ItemResult(string title, string link)
        {
            SiteTitle = title;
            ActionLink = link;
        }
    }

    public class ErrorItem
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public ErrorItem(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
