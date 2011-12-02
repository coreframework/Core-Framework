using System;
using System.Collections.Generic;
using Framework.Core.Localization;
using Framework.Facilities.NHibernate.Objects;

namespace Core.Profiles.NHibernate.Models
{
    public class ProfileHeader : LocalizableEntity<ProfileHeaderLocale>
    {
        #region Fields

        private readonly List<ProfileElement> profileElements = new List<ProfileElement>();

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title
        {
            get
            {
                return ((ProfileHeaderLocale)CurrentLocale).Title;
            }
            set
            {
                ((ProfileHeaderLocale)CurrentLocale).Title = value;
            }
        }

        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        /// <value>The order number.</value>
        public virtual Int32 OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the profile elements.
        /// </summary>
        /// <value>The profile elements.</value>
        public virtual IEnumerable<ProfileElement> ProfileElements
        {
            get { return profileElements; }
        }

        /// <summary>
        /// Gets or sets the type of the profile.
        /// </summary>
        /// <value>The type of the profile.</value>
        public virtual ProfileType ProfileType { get; set; }

        public override ILocale InitializeLocaleEntity()
        {
            return new ProfileHeaderLocale
            {
                ProfileHeader = this,
                Culture = null
            };
        }

        #endregion
    }
}
