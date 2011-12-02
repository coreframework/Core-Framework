using System;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.Profiles.NHibernate.Models
{
    public class ProfileHeaderLocale : Entity, ILocale
    {
        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title { get; set; }

        /// <summary>
        /// Gets or sets the profile header.
        /// </summary>
        /// <value>The profile header.</value>
        public virtual ProfileHeader ProfileHeader { get; set; }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        /// <value>The culture.</value>
        public virtual String Culture { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public virtual int Priority { get; private set; }

        #endregion
    }
}
