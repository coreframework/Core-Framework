using System;
using System.Collections.Generic;
using Core.Framework.Permissions.Models;
using FluentNHibernate.Data;

namespace Core.Forms.NHibernate.Models
{
    public class FormBuilderWidget: Entity
    {
        #region Fields

        private readonly IList<FormWidgetAnswer> _answers;

        #endregion

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

        /// <summary>
        /// Gets or sets the answers.
        /// </summary>
        /// <value>The answers.</value>
        public virtual IEnumerable<FormWidgetAnswer> Answers
        {
            get { return _answers; }
        }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public virtual BaseUser User { get; set; }

        #endregion
    }
}
