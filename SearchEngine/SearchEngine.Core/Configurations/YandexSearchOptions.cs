namespace SearchEngine.Core.Configurations
{
    public class YandexSearchOptions : SearchEngineOptions
    {
        public YandexSearchOptions(string name, string uri, string apikey, string username) : base(name, uri, apikey)
        {
            Username = username;
        }

        public string Username { get; set; }
    }
}
