using System;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.Forms.NHibernate.Models
{
    public class FormLocale: Entity, ILocale
    {
        #region Properties

        /// <summary>
        /// Gets or sets the form.
        /// </summary>
        /// <value>The form.</value>
        public virtual Form Form { get; set; }

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
        /// Gets or sets the submit button text.
        /// </summary>
        /// <value>The submit button text.</value>
        public virtual String SubmitButtonText { get; set; }

        /// <summary>
        /// Gets or sets the reset button text.
        /// </summary>
        /// <value>The reset button text.</value>
        public virtual String ResetButtonText { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public virtual int Priority { get; private set; }

        #endregion
    }
}
