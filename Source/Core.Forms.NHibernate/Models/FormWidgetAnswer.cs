using System;
using System.Collections.Generic;
using Core.Framework.Permissions.Models;
using FluentNHibernate.Data;

namespace Core.Forms.NHibernate.Models
{
    public class FormWidgetAnswer: Entity
    {
        #region Fields

        private IEnumerable<FormWidgetAnswerValue> _answerValues;

        #endregion

        #region Properties

        public FormWidgetAnswer()
        {
            _answerValues = new List<FormWidgetAnswerValue>();
            User = new BaseUser();
        }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>The create date.</value>
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public virtual BaseUser User { get; set; }

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
        public virtual IEnumerable<FormWidgetAnswerValue> AnswerValues
        {
            get { return _answerValues; }
        }

        #endregion
    }
}
