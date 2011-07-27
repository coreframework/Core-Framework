using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.News.Nhibernate.Models;
using Framework.Core.DomainModel;
using Framework.Core.Localization;

namespace Core.News.Models
{
    public class NewsArticleLocaleViewModel : IMappedModel<NewsArticle, NewsArticleLocaleViewModel>
    {
        public IDictionary<String, String> Cultures { get; set; }
        public long NewsArticleId { get; set; }
        public String SelectedCulture { get; set; }
        [Required]
        public String Title { get; set; }
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [DataType("FckEditorText"), Required]
        public String Content { get; set; }

        public NewsArticleLocaleViewModel MapFrom(NewsArticle from)
        {
            NewsArticleId = from.Id;
            Cultures = CultureHelper.GetAvailableCultures();
            SelectedCulture = from.CurrentLocale.Culture;
            Title = from.Title;
            Content = from.Content;

            return this;
        }

        public NewsArticle MapTo(NewsArticle to)
        {
            to.Title = Title;
            to.Content = Content;

            return to;
        }
    }
}