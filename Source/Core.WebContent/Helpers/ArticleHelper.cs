using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Core.WebContent.Models;
using Core.WebContent.NHibernate.Static;
using Framework.Core.Helpers.Regex;
using Framework.Mvc.Helpers;

namespace Core.WebContent.Helpers
{
    public class ArticleHelper
    {
        /// <summary>
        /// Validates the article model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="modelState">State of the model.</param>
        public static void ValidateArticle(ArticleViewModel model, ModelStateDictionary modelState)
        {
            if (!String.IsNullOrEmpty(model.Url))
            {
                if (model.UrlType == ArticleUrlType.External)
                {
                    if (!Regex.IsMatch(model.Url, RegexValidationConfig.GetPattern(RegexTemplates.Url)))
                    {
                        modelState.AddModelError(PropertyName.For<ArticleViewModel>(item => item.Url),
                        ResourceHelper.TranslateErrorMessage(new HttpContextWrapper(HttpContext.Current), typeof(ArticleViewModel),
                        PropertyName.For<ArticleViewModel>(item => item.Url), "regularexpression", null));
                    }
                }
                else if (model.UrlType == ArticleUrlType.Internal)
                {
                    if (!Regex.IsMatch(model.Url, RegexValidationConfig.GetPattern(RegexTemplates.UrlPart)))
                    {
                        modelState.AddModelError(PropertyName.For<ArticleViewModel>(item => item.Url),
                        ResourceHelper.TranslateErrorMessage(new HttpContextWrapper(HttpContext.Current), typeof(ArticleViewModel),
                        PropertyName.For<ArticleViewModel>(item => item.Url), "regularexpression", null));
                    }
                }
            }
        }
    }
}