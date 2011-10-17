using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Core.Forms.NHibernate.Models;
using Framework.Mvc.Extensions;

namespace Core.Forms.Extensions
{
    public static class FormElementsExtensions
    {
        #region Fields

        public static readonly Int32 CaptchaDefaultHeight = 40; 

        public static readonly Int32 CaptchaDefaultWidth = 120;

        public static readonly String FormElementNameFormat = "{0}_{1}";

        public static readonly char ElementValuesSeparator = ',';

        #endregion

        #region Extensions

        /// <summary>
        /// Renders html depends on model's type (i.e. textbox, radion buttons)
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="model">The model.</param>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public static MvcHtmlString FormElementRenderer(this HtmlHelper html, FormElement model, FormCollection collection)
        {
            var builder = new StringBuilder();

            var elementName = String.Format(FormElementNameFormat, model.Type, model.Id);
            var elementValue = String.Empty;
            if (collection!=null && collection[elementName]!=null)
            {
                elementValue = collection[elementName];
            }

            builder.Append(RenderElementByType(html, model, elementName, elementValue));
            builder.Append("<br/>");
            builder.Append(html.ValidationMessage(elementName));
        
            return MvcHtmlString.Create(builder.ToString());
        }

        #endregion

        #region Private Methods

        private static String RenderElementByType(HtmlHelper html, FormElement model, String elementName, String elementValue)
        {
            var builder = new StringBuilder();
            switch (model.Type)
            {
                case FormElementType.TextField:
                    {
                        builder.Append(html.Encode(model.Title));
                        break;
                    }
                case FormElementType.TextBox:
                    {
                        builder.Append(html.Label(elementName, model.Title));
                        builder.Append("<br/>");
                        builder.Append(html.TextBox(elementName, elementValue));
                        break;
                    }
                case FormElementType.TextArea:
                    {
                        builder.Append(html.Label(elementName, model.Title));
                        builder.Append("<br/>");
                        builder.Append(html.TextArea(elementName, elementValue));
                        break;
                    }
                case FormElementType.CheckBox:
                    {
                        builder.Append(html.SimpleCheckBox(elementName, FormCollectionExtensions.BooleanValue(elementValue)));
                        builder.Append(html.Label(elementName, model.Title));
                        break;
                    }
                case FormElementType.DropDownList:
                    {
                        builder.Append(html.Label(elementName, model.Title));
                        builder.Append("<br/>");
                        builder.Append(html.DropDownList(elementName, ParseElementValuesForDropDown(model.ElementValues, elementValue)));
                        break;
                    }
                case FormElementType.RadioButtons:
                    {
                        builder.Append(html.Label(elementName, model.Title));
                        builder.Append("<br/>");
                        builder.Append(html.RadioList(elementName, ParseElementValuesForRadioButtons(model.ElementValues, elementName), elementValue));
                        break;
                    }
                case FormElementType.Captcha:
                    {
                        builder.Append(html.CaptchaImage(CaptchaDefaultHeight, CaptchaDefaultWidth));
                        builder.Append(html.Label(elementName, String.Empty));
                        builder.Append("<br/>");
                        builder.Append(html.CaptchaTextBox(elementName));
                        break;
                    }

                default:
                    break;
            }

            return builder.ToString();
        }

        /// <summary>
        /// Parses the element values for radio buttons.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <returns></returns>
        private static Dictionary<String, String> ParseElementValuesForRadioButtons(String values, String elementName)
        {
            var result = new Dictionary<String, String>();
            if (!String.IsNullOrEmpty(values))
            {
                String[] valuesArray = values.Trim().Split(ElementValuesSeparator);
                for (int i = 0; i < valuesArray.Length; i++)
                {
                    if (!String.IsNullOrEmpty(valuesArray[i]))
                    {
                        result.Add(String.Format("{0}_{1}", elementName, i), valuesArray[i]);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Parses the element values for drop down list.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="selectedValue">The selected value.</param>
        /// <returns></returns>
        private static IEnumerable<SelectListItem> ParseElementValuesForDropDown(String values, String selectedValue)
        {
            var valuesArray = values.Trim().Split(ElementValuesSeparator);

            var result = new List<SelectListItem> {new SelectListItem {Text = @"Please select", Value = String.Empty}};
            result.AddRange(from item in valuesArray
                            where !String.IsNullOrEmpty(item)
                            select new SelectListItem {Text = item, Value = item, Selected = item.Equals(selectedValue)});

            return result;
        }

        #endregion
    }
}