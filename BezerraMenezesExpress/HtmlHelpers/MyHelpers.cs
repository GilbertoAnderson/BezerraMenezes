using System;
using System.Web.Mvc;

namespace BezerraMenezesExpress.WebUI.HtmlHelpers
{
    public static class MyHelpers
    {            
        //public static MvcHtmlString ImageLink(this HtmlHelper html, string imagePath, string alt, string cssClass,
        //   string action, string controllerName)
        //{
        //    return ActionImage(html, imagePath, alt, cssClass, action, controllerName, null);
        //}

        public static MvcHtmlString ImageLink(this HtmlHelper html, string imagePath, string alt, string cssClass,
           string action, string controllerName, object routeValues)
        {
            var currentUrl = new UrlHelper(html.ViewContext.RequestContext);
            var imgTagBuilder = new TagBuilder("img"); // build the <img> tag
            imgTagBuilder.MergeAttribute("src", currentUrl.Content(imagePath));
            imgTagBuilder.MergeAttribute("title", alt);
            imgTagBuilder.MergeAttribute("class", cssClass);
            string imgHtml = imgTagBuilder.ToString(TagRenderMode.SelfClosing);
            var anchorTagBuilder = new TagBuilder("a"); // build the <a> tag
            anchorTagBuilder.MergeAttribute("href", currentUrl.Action(action, controllerName, routeValues));
            anchorTagBuilder.InnerHtml = imgHtml; // include the <img> tag inside
            string anchorHtml = anchorTagBuilder.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(anchorHtml);
        }
    }
}