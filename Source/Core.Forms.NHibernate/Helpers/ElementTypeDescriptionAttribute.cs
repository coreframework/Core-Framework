using System;
using Core.Forms.NHibernate.Models;

namespace Core.Forms.NHibernate.Helpers
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ElementTypeDescriptionAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementTypeDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="isValuesEnabled">if set to <c>true</c> [is values enabled].</param>
        /// <param name="isRequiredEnabled">if set to <c>true</c> [is required enabled].</param>
        /// <param name="isValidationEnabled">if set to <c>true</c> [is validation enabled].</param>
        public ElementTypeDescriptionAttribute(bool isValuesEnabled, bool isRequiredEnabled, bool isValidationEnabled)
        {
            IsValuesEnabled = isValuesEnabled;
            IsRequiredEnabled = isRequiredEnabled;
            IsValidationEnabled = isValidationEnabled;
        }

        #endregion

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

        #endregion
    }
}