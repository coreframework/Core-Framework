using System;
using Framework.Core.Localization;
using Framework.Facilities.NHibernate.Objects;
using Iesi.Collections.Generic;

namespace Core.Profiles.NHibernate.Models
{
    public class ProfileHeader : LocalizableEntity<ProfileHeaderLocale>
    {
        #region Fields

        private readonly ISet<ProfileElement> profileElements = new HashedSet<ProfileElement>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether [show on member profile].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show on member profile]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool ShowOnMemberProfile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show on member registration].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show on member registration]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool ShowOnMemberRegistration { get; set; }

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
        public virtual ISet<ProfileElement> ProfileElements
        {
            get
            {
                return profileElements;
            }
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
