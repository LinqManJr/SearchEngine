using SearchEngine.Core.Models;
using System.Collections.Generic;

namespace SearchEngine.Tests
{
    public static class DefaultConfigs
    {
        public static IList<ItemResult> GetDefaultItemsOfResult => new List<ItemResult>
        {
            new ItemResult("title1","link1"),
            new ItemResult("title2","link2"),
            new ItemResult("title3","link3"),
            new ItemResult("title4","link4"),
            new ItemResult("title5","link5"),
            new ItemResult("title6","link6"),
            new ItemResult("title7","link7"),
            new ItemResult("title8","link8"),
            new ItemResult("title9","link9"),
            new ItemResult("title10","link10")
        };

        public static ErrorItem GetDefaultErrorItem => new ErrorItem("BadRequest", "Something went wrong");
    }
}
