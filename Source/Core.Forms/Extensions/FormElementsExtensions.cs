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

        public const String FormElementNameFormat = "{0}_{1}";

        public const char ElementValuesSeparator = ',';

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
                        builder.Append(html.TextBox(elementName, elementValue));
                        break;
                    }
                case FormElementType.TextArea:
                    {
                        builder.Append(html.Label(elementName, model.Title));
                        builder.Append(html.TextArea(elementName, elementValue));
                        break;
                    }
                case FormElementType.CheckBox:
                    {
                        builder.Append(html.CheckBox(elementName, FormCollectionExtensions.BooleanValue(elementValue)));
                        builder.Append(model.Title);
                        break;
                    }
                case FormElementType.DropDownList:
                    {
                        builder.Append(html.Label(elementName, model.Title));
                        builder.Append(html.DropDownList(elementName, ParseElementValuesForDropDown(model.ElementValues, elementValue)));
                        break;
                    }
                case FormElementType.RadioButtons:
                    {
                        builder.Append((model.Title));
                        builder.Append(html.RadioList(elementName, ParseElementValuesForRadioButtons(model.ElementValues, elementName), elementValue));
                        break;
                    }
                case FormElementType.Captcha:
                    {
                        builder.Append(html.CaptchaImage(CaptchaDefaultHeight, CaptchaDefaultWidth));
                        builder.Append(html.Label(elementName,""));
                        builder.Append(html.CaptchaTextBox(elementName));
                        break;
                    }

                default:
                    break;
            }

            builder.Append(html.ValidationMessage(elementName));
        
            return MvcHtmlString.Create(builder.ToString());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Parses the element values for radio buttons.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <returns></returns>
        private static Dictionary<String, String> ParseElementValuesForRadioButtons(String values, String elementName)
        {
            var valuesArray = values.Trim().Split(ElementValuesSeparator);
            var result = new Dictionary<String, String>();

            for (var i=0; i<valuesArray.Length; i++)
            {
                if (!String.IsNullOrEmpty(valuesArray[i]))
                {
                    result.Add(String.Format("{0}_{1}", elementName, i), valuesArray[i]);
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

            var result = new List<SelectListItem> {new SelectListItem {Text = @"Please select", Value = ""}};
            result.AddRange(from item in valuesArray
                            where !String.IsNullOrEmpty(item)
                            select new SelectListItem {Text = item, Value = item, Selected = item.Equals(selectedValue)});

            return result;
        }

        #endregion
    }
}