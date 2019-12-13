namespace SearchEngine.Core.Configurations
{
    public class SearchConfig
    {
        public string SearchEngine { get; set; }
        public string ApiKey { get; set; }
        public string AppId { get; set; } // for google
        public string Url { get; set; }
        public string UserName { get; set; } = string.Empty; // only yandex
    }
}
