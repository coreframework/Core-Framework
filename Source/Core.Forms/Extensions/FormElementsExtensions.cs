using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Core.Forms.NHibernate.Models;
using Framework.MVC.Extensions;
using System.Web.Mvc.Html;

namespace Core.Forms.Extensions
{
    public static class FormElementsExtensions
    {
        #region Fields

        public const Int32 CaptchaDefaultHeight = 40; 

        public const Int32 CaptchaDefaultWidth = 120; 

        #endregion

        #region Extensions

        public static MvcHtmlString FormElementRenderer(this HtmlHelper html, FormElement model)
        {
            var builder = new StringBuilder();

            var elementName = String.Format("{0}_{1}", model.Type, model.Id);
            switch (model.Type)
            {
                case FormElementType.TextField:
                    {
                        builder.Append(model.Title);
                        break;
                    }

                case FormElementType.TextBox:
                    {
                        builder.Append(html.Label(elementName, model.Title));
                        builder.Append(html.TextBox(elementName));
                        break;
                    }
                case FormElementType.TextArea:
                    {
                        builder.Append(html.Label(elementName, model.Title));
                        builder.Append(html.TextArea(elementName));
                        break;
                    }
                case FormElementType.CheckBox:
                    {
                        builder.Append(html.CheckBox(elementName));
                        builder.Append(html.Label(elementName));
                        break;
                    }
                case FormElementType.DropDownList:
                    {
                        var values = model.ElementValues.Trim().Split(',');
                        var selectedList = values.Select(item => new SelectListItem() {Text = item, Value = item}).ToList();

                        builder.Append(html.Label(elementName, model.Title));
                        builder.Append(html.DropDownList(elementName, selectedList));
                        break;
                    }
                case FormElementType.RadioButtons:
                    {
                        var values = model.ElementValues.Trim().Split(',');
                        var result = new Dictionary<String, String>();

                        var index = 0;
                        foreach (var item in values)
                        {
                            result.Add(String.Format("{0}_{1}", elementName, index), item);
                            index++;
                        }

                        builder.Append(html.RadioList(model.Id.ToString(), result, null));
                        break;
                    }
                case FormElementType.Captcha:
                    {
                        builder.Append(html.CaptchaImage(CaptchaDefaultHeight, CaptchaDefaultWidth));
                        builder.Append(html.CaptchaTextBox(elementName));
                        break;
                    }

                default:
                    break;
            }
        
            return MvcHtmlString.Create(builder.ToString());
        }

        #endregion
    }
}