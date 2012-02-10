using System;
using Framework.Core.Localization;
using Framework.Facilities.NHibernate.Objects;
using Iesi.Collections.Generic;

namespace Core.Profiles.NHibernate.Models
{
    public class ProfileType : LocalizableEntity<ProfileTypeLocale>
    {
        #region Fields

        private readonly ISet<ProfileHeader> profileHeaders = new HashedSet<ProfileHeader>();

        #endregion

        #region Properties

        public virtual ISet<ProfileHeader> ProfileHeaders
        {
            get { return profileHeaders; }
        }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public virtual long? UserId { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>The create date.</value>
        public virtual DateTime CreateDate { get; set; }

        #endregion

        public override ILocale InitializeLocaleEntity()
        {
            return new ProfileTypeLocale
            {
                ProfileType = this,
                Culture = null
            };
        }
    }
}
