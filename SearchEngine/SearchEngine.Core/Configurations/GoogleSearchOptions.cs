namespace SearchEngine.Core.Configurations
{
    public class GoogleSearchOptions : SearchEngineOptions
    {
        public string AppId { get; set; }
        public GoogleSearchOptions(string uri, string apikey, string appId) : base(uri, apikey)
        {
            AppId = appId;
        }
    }
}
