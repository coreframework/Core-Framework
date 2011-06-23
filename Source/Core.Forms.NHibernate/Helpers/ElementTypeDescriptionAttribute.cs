using System;
using Core.Forms.NHibernate.Models;

namespace Core.Forms.NHibernate.Helpers
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ElementTypeDescriptionAttribute : Attribute
    {
        #region Properties

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

        /// <summary>
        /// Gets or sets a value indicating whether this instance is max length enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is max length enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsMaxLengthEnabled { get; set; }

        #endregion
    }
}