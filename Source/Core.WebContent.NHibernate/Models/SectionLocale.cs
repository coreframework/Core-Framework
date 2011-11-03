using System;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.WebContent.NHibernate.Models
{
    /// <summary>
    /// Contains section's localized fields.
    /// </summary>
    public class SectionLocale: Entity, ILocale
    {
        #region Properties

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

        /// <summary>
        /// Gets or sets the section.
        /// </summary>
        /// <value>The section.</value>
        public virtual Section Section { get; set; }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        /// <value>The culture.</value>
        public virtual String Culture { get; set; }

        #endregion
    }
}
