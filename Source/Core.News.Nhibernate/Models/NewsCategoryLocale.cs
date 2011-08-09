using System;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.News.Nhibernate.Models
{
    public class NewsCategoryLocale : Entity, ILocale
    {
        #region Properties

        public virtual NewsCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        public virtual String Culture { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual String Description { get; set; }

        #endregion
    }
}
