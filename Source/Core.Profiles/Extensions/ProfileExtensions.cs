using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Core.Profiles.NHibernate.Models;
using Framework.Mvc.ElementsTypes;

namespace Core.Profiles.Extensions
{
    public static class ProfileExtensions
    {
        public static readonly String ElementNameFormat = "{0}_{1}";

        public static MvcHtmlString ProfileHeaderRenderer(this HtmlHelper html, ProfileHeader header)
        {
            var builder = new StringBuilder();

            var el = new TagBuilder("p")
                         {
                             InnerHtml = header.Title,
                         };
            el.AddCssClass("profile-header");

            builder.Append(el);
            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString ProfileElementRenderer(this HtmlHelper html, ProfileElement element, FormCollection collection)
        {
            var builder = new StringBuilder();

            var elementName = String.Format(ElementNameFormat, (ElementType)element.Type, element.Id);
            var elementValue = String.Empty;

            if (collection != null && collection[elementName] != null)
            {
                elementValue = collection[elementName];
            }

            builder.Append(html.Label(elementName, element.Title));
            builder.Append("<br/>");
            builder.Append(ElementTypeUtility.RenderElementType(html, (ElementType)element.Type, elementName,
                                                               elementValue, element.ElementValues));
            builder.Append(html.ValidationMessage(elementName));

            return MvcHtmlString.Create(builder.ToString());
        }
    }
}