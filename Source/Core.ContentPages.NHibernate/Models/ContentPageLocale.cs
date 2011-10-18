using System;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.ContentPages.NHibernate.Models
{
    public class ContentPageLocale : Entity, ILocale
    {
        #region Properties

        /// <summary>
        /// Gets or sets the content page.
        /// </summary>
        /// <value>The content page.</value>
        public virtual ContentPage ContentPage { get; set; }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        /// <value>The culture.</value>
        public virtual String Culture { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title { get; set; }

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
