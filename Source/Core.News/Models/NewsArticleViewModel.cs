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
        [Required, StringLength(255)]
        public virtual String Title { get; set; }

        /// <summary>
        /// Gets or sets the Summary.
        /// </summary>
        /// <value>The Summary.</value>
        [Required, StringLength(1024)]
        public virtual String Summary { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [DataType("FckEditorText"), Required]
        public virtual String Content { get; set; }

        /// <summary>
        /// Gets or sets the status Id.
        /// </summary>
        /// <value>The status Id.</value>
        [Required]
        public virtual int StatusId { get; set; }

        public virtual NewsStatus Status
        {
            get
            {
                return (NewsStatus)StatusId; 
            }
            set
            {
                StatusId = (int)value;
            }
        }

        public virtual DateTime CreateDate { get; set; }

        public virtual DateTime LastModifiedDate { get; set; }

        public NewsArticleViewModel MapFrom(NewsArticle from)
        {
            Title = from.Title;
            Content = from.Content;
            Summary = from.Summary;
            StatusId = from.StatusId;
            CreateDate = from.CreateDate;
            LastModifiedDate = from.LastModifiedDate;
            return this;

        }

        public NewsArticle MapTo(NewsArticle to)
        {
            to.Title = Title;
            to.Content = Content;
            to.Summary = Summary;
            to.StatusId = StatusId;
            to.CreateDate = CreateDate;
            to.LastModifiedDate = LastModifiedDate;
            return to;
        }
    }
}