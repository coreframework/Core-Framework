using System;
using System.Web.Mvc;

namespace Framework.Mvc.CustomElements
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CustomElement : ICustomElement
    {
        public abstract string Title { get;}
        public abstract bool IsValuesEnabled { get;}
        public abstract bool IsRequiredEnabled { get; }
        public abstract bool IsMaxLengthEnabled { get;}

        /// <summary>
        /// Renders the specified HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public abstract String Render(HtmlHelper html, String name);
    }
}
