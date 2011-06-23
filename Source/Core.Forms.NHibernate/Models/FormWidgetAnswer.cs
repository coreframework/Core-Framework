using System;
using System.Collections.Generic;
using FluentNHibernate.Data;

namespace Core.Forms.NHibernate.Models
{
    public class FormWidgetAnswer: Entity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>The create date.</value>
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public virtual long? UserId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title { get; set; }

        /// <summary>
        /// Gets or sets the form builder widget.
        /// </summary>
        /// <value>The form builder widget.</value>
        public virtual FormBuilderWidget FormBuilderWidget { get; set; }

        /// <summary>
        /// Gets or sets the answer values.
        /// </summary>
        /// <value>The answer values.</value>
        public virtual IEnumerable<FormWidgetAnswerValue> AnswerValues { get; set; }

        #endregion
    }
}
