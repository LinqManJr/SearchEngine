namespace SearchEngine.Core.Configurations
{
    public class SearchEngineOptions
    {
        public string Uri { get; set; }
        public string Apikey { get; set; }

        public SearchEngineOptions(string uri, string apikey)
        {
            Uri = uri;
            Apikey = apikey;
        }
    }
}
