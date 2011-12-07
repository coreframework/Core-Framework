using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Profiles.NHibernate.Models;
using Framework.Core.DomainModel;
using Framework.Core.Localization;

namespace Core.Profiles.Models
{
    public class ProfileHeaderViewModel : IMappedModel<ProfileHeader, ProfileHeaderViewModel>
    {
        #region Fields

        private IDictionary<String, String> cultures;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public virtual long Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public virtual String Title { get; set; }

        /// <summary>
        /// Gets or sets the selected culture.
        /// </summary>
        /// <value>The selected culture.</value>
        public String SelectedCulture { get; set; }

        /// <summary>
        /// Gets or sets the profile type id.
        /// </summary>
        /// <value>The profile type id.</value>
        public long? ProfileTypeId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show on member profile].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show on member profile]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowOnMemberProfile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show on member registration].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show on member registration]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowOnMemberRegistration { get; set; } 

        /// <summary>
        /// Gets or sets the cultures.
        /// </summary>
        /// <value>The cultures.</value>
        public IDictionary<String, String> Cultures
        {
            get { return cultures ?? (cultures = CultureHelper.GetAvailableCultures()); }
            set { cultures = value; }
        }

        public ProfileHeaderViewModel MapFrom(ProfileHeader from)
        {
            Id = from.Id;
            ShowOnMemberProfile = from.ShowOnMemberProfile;
            ShowOnMemberRegistration = from.ShowOnMemberRegistration;
            MapLocaleFrom(from.CurrentLocale as ProfileHeaderLocale);

            return this;
        }

        public ProfileHeader MapTo(ProfileHeader to)
        {
            to.Id = Id;
            to.ShowOnMemberProfile = ShowOnMemberProfile;
            to.ShowOnMemberRegistration = ShowOnMemberRegistration;
            if (String.IsNullOrEmpty(SelectedCulture))
                MapLocaleTo((ProfileHeaderLocale)to.CurrentLocale);
            return to;
        }

        public ProfileHeaderViewModel MapLocaleFrom(ProfileHeaderLocale locale)
        {
            Title = locale.Title;
            SelectedCulture = locale.Culture;

            return this;
        }

        public ProfileHeaderLocale MapLocaleTo(ProfileHeaderLocale locale)
        {
            locale.Title = Title;
            if (SelectedCulture != null)
                locale.Culture = SelectedCulture;
            return locale;
        }

        #endregion
    }
}