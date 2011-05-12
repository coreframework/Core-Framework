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
        private readonly Func<T, string> _locationRenderer;

        private readonly IEnumerable<T> _rootLocations;

        private readonly String _cssClass;

        private HtmlTextWriter _writer;

        public TreeRenderer(
            IEnumerable<T> rootLocations,String cssClass,
            Func<T, string> locationRenderer)
        {
            _rootLocations = rootLocations;
            _locationRenderer = locationRenderer;
            _cssClass = cssClass;
        }

        public string Render()
        {
            _writer = new HtmlTextWriter(new StringWriter());
            RenderLocations(true,_rootLocations);
            return _writer.InnerWriter.ToString();
        }

        /// <summary>
        /// Recursively walks the location tree outputting it as hierarchical UL/LI elements
        /// </summary>
        /// <param name="renderCssClass">if set to <c>true</c> [render CSS class].</param>
        /// <param name="locations">The locations.</param>
        private void RenderLocations(bool renderCssClass,IEnumerable<T> locations)
        {
            if (locations == null) return;
            if (locations.Count() == 0) return;
            InUl(renderCssClass, () => locations.ForEach(location => InLi(() =>
                                                              {
                                                                  _writer.Write(_locationRenderer(location));
                                                                  RenderLocations(false,location.Children);
                                                              })));
        }

        private void InUl(bool renderCssClass,Action action)
        {
            _writer.WriteLine(); 
            if (renderCssClass)
                _writer.AddAttribute("class", _cssClass);
            
            _writer.RenderBeginTag(HtmlTextWriterTag.Ul);
          
            action();
            _writer.RenderEndTag();
            _writer.WriteLine();
        }

        private void InLi(Action action)
        {
            _writer.RenderBeginTag(HtmlTextWriterTag.Li);
            action();
            _writer.RenderEndTag();
            _writer.WriteLine();
        }
    }
}