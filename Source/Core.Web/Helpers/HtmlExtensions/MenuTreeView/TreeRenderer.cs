using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using Core.Framework.MEF.Extensions;

namespace Core.Web.Helpers.HtmlExtensions.MenuTreeView
{
    public class TreeRenderer<T> where T : IComposite<T>
    {
        private readonly Func<T, String> locationRenderer;

        private readonly IEnumerable<T> rootLocations;

        private readonly String cssClass;

        private HtmlTextWriter writer;

        public TreeRenderer(
            IEnumerable<T> rootLocations,String cssClass,
            Func<T, String> locationRenderer)
        {
            this.rootLocations = rootLocations;
            this.locationRenderer = locationRenderer;
            this.cssClass = cssClass;
        }

        public String Render()
        {
            writer = new HtmlTextWriter(new StringWriter());
            RenderLocations(true,rootLocations);
            return writer.InnerWriter.ToString();
        }

        /// <summary>
        /// Recursively walks the location tree outputting it as hierarchical UL/LI elements
        /// </summary>
        /// <param name="renderCssClass">if set to <c>true</c> [render CSS class].</param>
        /// <param name="locations">The locations.</param>
        private void RenderLocations(bool renderCssClass,IEnumerable<T> locations)
        {
            if (locations == null || !locations.Any()) return;

            InUl(renderCssClass, () => locations.ForEach(location => InLi(() =>
                                                              {
                                                                  writer.Write(locationRenderer(location));
                                                                  RenderLocations(false,location.Children);
                                                              })));
        }

        private void InUl(bool renderCssClass,Action action)
        {
            writer.WriteLine(); 
            if (renderCssClass)
                writer.AddAttribute("class", cssClass);
            
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
          
            action();
            writer.RenderEndTag();
            writer.WriteLine();
        }

        private void InLi(Action action)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Li);
            action();
            writer.RenderEndTag();
            writer.WriteLine();
        }
    }
}