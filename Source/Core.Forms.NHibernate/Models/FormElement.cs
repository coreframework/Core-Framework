using System;
using Framework.Core.Localization;
using Framework.Facilities.NHibernate.Objects;

namespace Core.Forms.NHibernate.Models
{
    /// <summary>
    /// Describes form element, which form can contain.
    /// </summary>
    public class FormElement : LocalizableEntity<FormElementLocale>
    {
        #region Properties
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title
        {
            get
            {
                return ((FormElementLocale)CurrentLocale).Title;
            }
            set
            {
                ((FormElementLocale)CurrentLocale).Title = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of element.
        /// </summary>
        /// <value>The type.</value>
        public virtual FormElementType Type { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        public virtual String ElementValues
        {
            get
            {
                return ((FormElementLocale)CurrentLocale).ElementValues;
            }
            set
            {
                ((FormElementLocale)CurrentLocale).ElementValues = value;
            }
        }

        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        /// <value>The order number.</value>
        public virtual Int32 OrderNumber { get; set; }

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

        /// <summary>
        /// Gets or sets the length of the max.
        /// </summary>
        /// <value>The length of the max.</value>
        public virtual long? MaxLength { get; set; }

        /// <summary>
        /// Gets or sets the regex template.
        /// </summary>
        /// <value>The regex template.</value>
        public virtual RegexTemplate RegexTemplate { get; set; }

        public virtual Type LocaleType
        {
            get
            {
                return typeof(FormElementLocale);
            }
        }

        public override ILocale InitializeLocaleEntity()
        {
            return new FormElementLocale
                        {
                            FormElement = this,
                            Culture = null
                        };
        }

        #endregion
    }
}
