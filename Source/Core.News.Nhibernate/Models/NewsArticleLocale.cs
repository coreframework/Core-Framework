using System;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.News.Nhibernate.Models
{
    public class NewsArticleLocale : Entity, ILocale
    {
        #region Properties

        public virtual NewsArticle NewsArticle { get; set; }

        public virtual String Culture { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        public virtual String Summary { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public virtual String Content { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public virtual int Priority { get; private set; }

        #endregion
    }
}
