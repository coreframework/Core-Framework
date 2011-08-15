using System;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Products.NHibernate.Models
{
    public class ProductLocale: Entity, ILocale
    {
        #region Properties

        public virtual Product Product { get; set; }

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

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public virtual int Priority { get; private set; }

        #endregion
    }
}
