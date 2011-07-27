using System;
using System.ComponentModel.DataAnnotations;
using Core.News.Nhibernate.Models;
using Framework.Core.DomainModel;

namespace Core.News.Models
{
    public class NewsArticleViewModel : IMappedModel<NewsArticle, NewsArticleViewModel>
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public virtual String Title { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [DataType("FckEditorText"), Required]
        public virtual String Content { get; set; }

        public NewsArticleViewModel MapFrom(NewsArticle from)
        {
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