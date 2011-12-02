using System;
using Framework.Core.Localization;
using Framework.Facilities.NHibernate.Objects;

namespace Core.Profiles.NHibernate.Models
{
    /// <summary>
    /// Describes form element, which form can contain.
    /// </summary>
    public class ProfileElement : LocalizableEntity<ProfileElementLocale>
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
                return ((ProfileElementLocale)CurrentLocale).Title;
            }
            set
            {
                ((ProfileElementLocale)CurrentLocale).Title = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of element.
        /// </summary>
        /// <value>The type.</value>
        public virtual ProfileElementType Type { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        public virtual String ElementValues
        {
            get
            {
                return ((ProfileElementLocale)CurrentLocale).ElementValues;
            }
            set
            {
                ((ProfileElementLocale)CurrentLocale).ElementValues = value;
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
        public virtual ProfileHeader ProfileHeader { get; set; }

        /// <summary>
        /// Gets or sets the length of the max.
        /// </summary>
        /// <value>The length of the max.</value>
        public virtual long? MaxLength { get; set; }

        public virtual Type LocaleType
        {
            get
            {
                return typeof(ProfileElementLocale);
            }
        }

        public override ILocale InitializeLocaleEntity()
        {
            return new ProfileElementLocale
            {
                ProfileElement = this,
                Culture = null
            };
        }

        #endregion
    }
}
