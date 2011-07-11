using System;
using FluentNHibernate.Data;

namespace Core.Languages.NHibernate.Models
{
    public class Language : Entity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        public virtual String Code { get; set; }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        /// <value>The culture.</value>
        public virtual String Culture { get; set; }

        #endregion

    }
}
