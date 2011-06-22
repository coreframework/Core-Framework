using System;
using FluentNHibernate.Data;

namespace Core.Forms.NHibernate.Models
{
    public class FormBuilderWidget: Entity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [save data].
        /// </summary>
        /// <value><c>true</c> if [save data]; otherwise, <c>false</c>.</value>
        public virtual bool SaveData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [send email].
        /// </summary>
        /// <value><c>true</c> if [send email]; otherwise, <c>false</c>.</value>
        public virtual bool SendEmail { get; set; }

        /// <summary>
        /// Gets or sets the sender email.
        /// </summary>
        /// <value>The sender email.</value>
        public virtual String SenderEmail { get; set; }

        /// <summary>
        /// Gets or sets the form.
        /// </summary>
        /// <value>The form.</value>
        public virtual Form Form { get; set; }

        #endregion
    }
}
