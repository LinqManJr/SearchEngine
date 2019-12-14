namespace SearchEngine.Core.Configurations
{
    public class SearchEngineOptions
    {
        public string Name { get; }
        public string Uri { get; set; }
        public string Apikey { get; set; }

        public SearchEngineOptions(string name, string uri, string apikey)
        {
            Name = name;
            Uri = uri;
            Apikey = apikey;
        }
    }
}
