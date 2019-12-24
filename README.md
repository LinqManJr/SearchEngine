# SearchEngine
 
Multiple Search Engine with API implementations of Google, Bing, Yandex.

Database -> MSSQL,   ORM -> EF Core,  Test -> NUnit,   Frontend -> Bootstrap

This project using the api of several search engines with the ability to add the first positive result to the database. It is also possible to view all queries from the database and use a filter on the search text. As an additional feature, you can change search engine API configurations (API KEY or AppId, Uri, Number of results in request).

Project structure:
 1. SearchEngine.Core
    This is the main project, which include several folders.
    - Engines - folder with interface ISearchEngine and all realizations of this interface.
    - Configurations - options must be used in contructors of Engines to setup apikeys and etc.
    - Services - interface ISearchService and realization, used for manage multiple engines
    - Models - presented values of requests as SearchResult to adding to db and view at app
    - Extensions - aditional methods, which make it easy some work with Core classes.
 2. SearchEngine.Domain
    This project contains startup migrations for creating database, main context for manage entities and entities.
 3. SearchEngine.WebApp - Obsolete mvc project
 4. SearchEngine.RazorPages - main web-application started in browser
 5. Folder Tests - contains test projects
 
 Manual
 
 If you want to add new functionality in the form of a search engine then you need:
  1. Create new SearchEngineOptions class inherited from SearchEngineOptions if you have besides api and uri fields other fields.If you have only api, uri values we must use default SearchEngineOptions class. 
  2. Add Json Values to appsettings.json in EngineConfigSection in RazorPages project with your name of SearchEngine like that
  <code>"Google": {
      "Name": "google",
      "Uri": "",
      "ApiKey": "your api key",
      "AppId": "your app id",
      "NumItems": 10
    }</code>
  3. Add realization of ISearchEngine with constructor with params (IOptions<YourSearchEngineOptions>) or (ctor(YourSearchEngineOptions)) used for tests. 
  4. To used your SearchEngine in web-application we must configure Startup.cs in RazorPages.
     Add configure with your serachengine like that <code>services.Configure<YourSearchOptions>(configSection.GetSection("SearchEngineName"));</code>. This line get configs from appsetting.json and injecting as IOptions in YourSearchEngine.
     Add your engine with interface in services: <code>services.AddScoped<ISearchEngine, YourSearchEngine>();</code>
     At start web-application SearchService must injecting all engines throuh constructor with params <code>public SearchService(IEnumerable\<ISearchEngine\> engines)</code>
 
 Complete.
 
 
