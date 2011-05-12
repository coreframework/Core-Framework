// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuExtension.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Linq;
using Framework.MVC.Extensions;

namespace Core.Web.Areas.Admin.Helpers
{
    /// <summary>
    /// Extends <see cref="HtmlHelper"/> functionality to render admin main menu.
    /// </summary>
    public static class MenuExtension
    {
        /// <summary>
        /// Renders admin area main menu.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="url">The URL helper.</param>
        /// <param name="items">Menu items.</param>
        /// <returns>Html markup for admin main menu.</returns>
        public static MvcHtmlString RenderMenu(this HtmlHelper html, UrlHelper url, Dictionary<String, IEnumerable<IMenuItem>> items)
        {
            var activeSection = String.Empty;
            foreach (var section in items)
            {
                foreach (var item in section.Value)
                {
                    if (item.IsCurrent(html.ViewContext.RequestContext))
                    {
                        activeSection = section.Key;
                        break;
                    }
                }
                if (!String.IsNullOrEmpty(activeSection))
                {
                    break;
                }
            }

            var menu = new TagBuilder("ul");
            menu.Attributes["id"] = "navigation";
            menu.InnerHtml = RenderSectionsList(html, url, items, activeSection);
            return MvcHtmlString.Create(menu.ToString());
        }

        private static String RenderSectionsList(HtmlHelper html, UrlHelper url, Dictionary<String, IEnumerable<IMenuItem>> items, String activeSection)
        {
            var buffer = new StringBuilder();
            foreach (var section in items)
            {
                var isCurrent = activeSection.Equals(section.Key);
                var sectionTag = new TagBuilder("li");
                if (isCurrent)
                {
                    sectionTag.Attributes["class"] = "current";
                }
                sectionTag.InnerHtml = RenderSection(html, url, section.Key, section.Value, isCurrent);
                buffer.Append(sectionTag.ToString());
            }
            return buffer.ToString(); 
        }

        private static String RenderSection(HtmlHelper html, UrlHelper url, String title, IEnumerable<IMenuItem> items, bool isCurrent)
        {
            var sectionContent = new StringBuilder();
            var firstItem = items.FirstOrDefault();
            if (firstItem != null)
            {
                var link = new TagBuilder("a");
                link.Attributes["href"] = firstItem.GetUrl(url);
                link.InnerHtml = html.Translate(String.Format(".{0}", title));
                sectionContent.Append(link.ToString());
            }
            else
            {
                sectionContent.Append(title);
            }

            if (isCurrent)
            {
                var sectionItems = new TagBuilder("ul");
                sectionItems.Attributes["id"] = "nav_sub";
                sectionItems.InnerHtml = RenderMenuItems(html, url, items);
                sectionContent.Append(sectionItems.ToString());
            }

            return sectionContent.ToString();
        }

        private static String RenderMenuItems(HtmlHelper html, UrlHelper url, IEnumerable<IMenuItem> items)
        {
            var itemsContent = new StringBuilder();

            foreach (var item in items)
            {
                var itemTag = new TagBuilder("li");
                var link = new TagBuilder("a");
                link.Attributes["href"] = item.GetUrl(url);
                link.InnerHtml = (new TagBuilder("spam") { InnerHtml = html.Translate(String.Format(".{0}", item.Title)) }).ToString();
                if (item.IsCurrent(html.ViewContext.RequestContext))
                {
                    link.Attributes["class"] = "current";
                }

                itemTag.InnerHtml = link.ToString();

                itemsContent.Append(itemTag.ToString());
            }

            return itemsContent.ToString();
        }
    }
}