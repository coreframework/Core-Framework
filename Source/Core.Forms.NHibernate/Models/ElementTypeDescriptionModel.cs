using System;

namespace Core.Forms.NHibernate.Models
{
    public class ElementTypeDescriptionModel
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public String Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether values field enable.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is values enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsValuesEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether validation filed enable.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is validation enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsValidationEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether requirement field enable.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is required enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsRequiredEnabled { get; set; }

    }
}