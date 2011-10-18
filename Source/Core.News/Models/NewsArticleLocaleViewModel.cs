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
        [Required, StringLength(255)]
        public String Title { get; set; }

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
        public String Content { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM.dd.yyyy}")]
        public virtual DateTime PublishDate { get; set; }

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

        public virtual bool PublishingAccess { get; set; }

        public NewsArticleLocaleViewModel MapFrom(NewsArticle from)
        {
            NewsArticleId = from.Id;
            Cultures = CultureHelper.GetAvailableCultures();
            SelectedCulture = from.CurrentLocale.Culture;
            Title = from.Title;
            Content = from.Content;
            Summary = from.Summary;
            StatusId = from.StatusId;
            CreateDate = from.CreateDate;
            LastModifiedDate = from.LastModifiedDate;
            PublishDate = from.PublishDate;

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
            to.PublishDate = PublishDate;

            return to;
        }
    }
}