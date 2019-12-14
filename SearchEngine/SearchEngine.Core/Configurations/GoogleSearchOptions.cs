namespace SearchEngine.Core.Configurations
{
    public class GoogleSearchOptions : SearchEngineOptions
    {
        public string AppId { get; set; }
        public GoogleSearchOptions(string name, string uri, string apikey, string appId) : base(name, uri, apikey)
        {
            AppId = appId;
        }
    }
}
