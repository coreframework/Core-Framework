using System;
using Framework.Core.Localization;
using Framework.Facilities.NHibernate.Objects;

namespace Core.WebContent.NHibernate.Models
{
    /// <summary>
    /// Web content section class.
    /// </summary>
    public class Section : LocalizableEntity<SectionLocale>
    {
        #region Properties

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
            return new SectionLocale
                       {
                           Section = this,
                           Culture = null
                       };
        }
    }
}
