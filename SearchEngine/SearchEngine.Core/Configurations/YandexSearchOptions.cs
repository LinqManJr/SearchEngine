namespace SearchEngine.Core.Configurations
{
    public class YandexSearchOptions : SearchEngineOptions
    {
        public YandexSearchOptions(string uri, string apikey, string username) : base(uri, apikey)
        {
            Username = username;
        }

        public string Username { get; set; }
    }
}
