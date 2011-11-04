using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.News.Nhibernate.Models
{
    public class NewsArticle : Entity, ILocalizable
    {
        private readonly IList<NewsArticleLocale> currentNewsArticleLocales = new List<NewsArticleLocale>();
        private IList<ILocale> currentLocales = new List<ILocale>();
        private NewsArticleLocale currentLocale;

        private DateTime publishDate = DateTime.Now;

        #region Properties

        public virtual long WidgetId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title
        {
            get
            {
                return ((NewsArticleLocale)CurrentLocale).Title;
            }
            set { ((NewsArticleLocale)CurrentLocale).Title = value; }
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public virtual String Url { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        public virtual String Summary
        {
            get
            {
                return ((NewsArticleLocale)CurrentLocale).Summary;
            }
            set { ((NewsArticleLocale)CurrentLocale).Summary = value; }
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public virtual String Content
        {
            get
            {
                return ((NewsArticleLocale)CurrentLocale).Content;
            }
            set { ((NewsArticleLocale)CurrentLocale).Content = value; }
        }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        public virtual IList<NewsCategory> Categories { get; set; }

        /// <summary>
        /// Gets or sets the status Id.
        /// </summary>
        /// <value>The status Id.</value>
        public virtual int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>The create date.</value>
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the publish date.
        /// </summary>
        /// <value>The publish date.</value>
        public virtual DateTime PublishDate
        {
            get { return publishDate; }
            set { publishDate = value; }
        }

        /// <summary>
        /// Gets or sets the last modified date.
        /// </summary>
        /// <value>The last modified date.</value>
        public virtual DateTime LastModifiedDate { get; set; }

        public virtual IList<ILocale> CurrentLocales
        {
            get
            {
                if (currentLocales.Count == 0 && currentNewsArticleLocales.Count > 0)
                {
                    currentLocales = currentNewsArticleLocales.ToList().ConvertAll(mc => (ILocale)mc);
                }
                return currentLocales;
            }
            set
            {
                currentLocales = value;
            }
        }

        public virtual IList<NewsArticleLocale> CurrentNewsArticleLocales
        {
            get
            {
                return CurrentLocales.ToList().ConvertAll(mc => (NewsArticleLocale)mc);
            }
            set
            {
                CurrentLocales = value.ToList().ConvertAll(mc => (ILocale)mc);
            }
        }

        public virtual Type LocaleType
        {
            get
            {
                return typeof(NewsArticleLocale);
            }
        }

        public virtual ILocale CurrentLocale
        {
            get
            {
                if (currentLocale == null)
                {
                    currentLocale = CultureHelper.GetCurrentLocale(CurrentLocales) as NewsArticleLocale;
                    if (currentLocale == null)
                    {
                        currentLocale = new NewsArticleLocale
                        {
                            NewsArticle = this,
                            Culture = null
                        };
                    }
                }
                return currentLocale;
            }
        }

        #endregion
    }
}
