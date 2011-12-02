using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Framework.Mvc.CustomElements.Generic
{
    /// <summary>
    /// 
    /// </summary>
    public class TextBox: CustomElement
    {
        public override string Title
        {
            get { return "Text box"; }
        }

        public override bool IsValuesEnabled
        {
            get { return false; }
        }

        public override bool IsRequiredEnabled
        {
            get { return true; }
        }

        public override bool IsMaxLengthEnabled
        {
            get { return true; }
        }

        public override string Render(HtmlHelper html, string name)
        {
            return html.TextBox(name).ToString();
        }
    }
}
