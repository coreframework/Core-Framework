using System;
using System.Collections.Generic;
using Framework.Core.Localization;
using Framework.Facilities.NHibernate.Objects;

namespace Core.Profiles.NHibernate.Models
{
    /// <summary>
    /// Describes form element, which form can contain.
    /// </summary>
    public class ProfileElement : LocalizableEntity<ProfileElementLocale>
    {
        #region Fields

        private bool showOnMemberProfile = true;
        private bool showOnMemberPublicProfile = true;
        private bool showOnMemberRegistration = true;
        private readonly IList<UserProfileElement> userProfileElements = new List<UserProfileElement>();

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
        public virtual Int32 Type { get; set; }

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

        public virtual IList<UserProfileElement> UserProfileElements
        {
            get { return userProfileElements; }
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
        /// Gets or sets a value indicating whether [show on member profile].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show on member profile]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool ShowOnMemberProfile
        {
            get { return showOnMemberProfile; }
            set { showOnMemberProfile = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show on member public profile].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show on member public profile]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool ShowOnMemberPublicProfile
        {
            get { return showOnMemberPublicProfile; }
            set { showOnMemberPublicProfile = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show on member registration].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show on member registration]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool ShowOnMemberRegistration
        {
            get { return showOnMemberRegistration; }
            set { showOnMemberRegistration = value; }
        }

        /// <summary>
        /// Initializes the locale entity.
        /// </summary>
        /// <returns></returns>
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
