using System;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.Forms.NHibernate.Models
{
    public class FormElementLocale: Entity, ILocale
    {
        #region Properties

        /// <summary>
        /// Gets or sets the form.
        /// </summary>
        /// <value>The form.</value>
        public virtual FormElement FormElement { get; set; }

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
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        public virtual String ElementValues { get; set; }

        #endregion
    }
}
