using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SearchEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.WebApp.Helpers
{
    public static class TagHelpers
    {
        public static HtmlString CreatePartialList(this IHtmlHelper html,IEnumerable<ItemResult> results)
        {
            var tagBuilder = new TagBuilder("div");
            tagBuilder.AddCssClass("list-group");

            var content = new TagBuilder("div");
            content.AddCssClass("d-flex w-100 justify-content-between");           


            return new HtmlString(tagBuilder.ToString());
        }

        public static HtmlString CreateErrorItem(this IHtmlHelper html, ErrorItem item)
        {
            throw new NotImplementedException();
        }
    }
}
