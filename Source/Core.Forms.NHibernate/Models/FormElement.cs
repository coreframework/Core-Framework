using System;
using FluentNHibernate.Data;

namespace Core.Forms.NHibernate.Models
{
    /// <summary>
    /// Describes form element, which form can contain.
    /// </summary>
    public class FormElement: Entity
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Name { get; set; }

        /// <summary>
        /// Gets or sets the type of element.
        /// </summary>
        /// <value>The type.</value>
        public virtual Int32 Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this element is required.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this element is required; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the form.
        /// </summary>
        /// <value>The form.</value>
        public virtual Form Form { get; set; }
    }
}
