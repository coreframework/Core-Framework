using System;
using System.Collections.Generic;
using Framework.Core.Localization;
using Framework.Facilities.NHibernate.Objects;

namespace Core.Profiles.NHibernate.Models
{
    public class ProfileType : LocalizableEntity<ProfileTypeLocale>
    {
        #region Fields

        private readonly IList<ProfileHeader> profileHeaders = new List<ProfileHeader>();

        #endregion

        #region Properties

        public virtual IList<ProfileHeader> ProfileHeaders
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
