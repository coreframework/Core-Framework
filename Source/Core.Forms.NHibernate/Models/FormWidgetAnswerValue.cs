using System;
using FluentNHibernate.Data;

namespace Core.Forms.NHibernate.Models
{
    public class FormWidgetAnswerValue: Entity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the field.
        /// </summary>
        /// <value>The field.</value>
        public virtual String Field { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public virtual String Value { get; set; }

        /// <summary>
        /// Gets or sets the answer.
        /// </summary>
        /// <value>The answer.</value>
        public virtual FormWidgetAnswer Answer { get; set; }

        #endregion
    }
}
