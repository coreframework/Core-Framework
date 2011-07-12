using System;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.ContentPages.NHibernate.Models
{
    public class ContentPageLocale : Entity, ILocale
    {
        #region Properties

        public virtual ContentPage ContentPage { get; set; }

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

        #endregion
    }
}
