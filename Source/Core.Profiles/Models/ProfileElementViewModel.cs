using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Framework.Core.DomainModel;
using Framework.Core.Localization;
using Microsoft.Practices.ServiceLocation;

namespace Core.Profiles.Models
{
    public class ProfileElementViewModel : IMappedModel<ProfileElement, ProfileElementViewModel>
    {
        #region Fields

       /* private List<ElementTypeDescriptionModel> types;*/

        private IDictionary<String, String> cultures;

        private List<ProfileHeader> profileHeaders;

        #endregion

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public virtual long Id { get; set; }

        /// <summary>
        /// Gets or sets the profile type id.
        /// </summary>
        /// <value>The profile type id.</value>
        public long ProfileTypeId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show on member profile].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show on member profile]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowOnMemberProfile { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [show on member public profile].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show on member public profile]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowOnMemberPublicProfile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show on member registration].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show on member registration]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowOnMemberRegistration { get; set; } 

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public virtual String Title { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        public virtual String Values { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is required.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is required; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public ProfileElementType Type { get; set; }

        /// <summary>
        /// Gets or sets the length of the max.
        /// </summary>
        /// <value>The length of the max.</value>
        public long? MaxLength { get; set; }

        /// <summary>
        /// Gets or sets the form id.
        /// </summary>
        /// <value>The form id.</value>
        public long ProfileHeaderId { get; set; }

        /// <summary>
        /// Gets or sets the selected culture.
        /// </summary>
        /// <value>The selected culture.</value>
        public String SelectedCulture { get; set; }

        /// <summary>
        /// Gets or sets the cultures.
        /// </summary>
        /// <value>The cultures.</value>
        public IDictionary<String, String> Cultures
        {
            get { return cultures ?? (cultures = CultureHelper.GetAvailableCultures()); }
            set { cultures = value; }
        }

        /// <summary>
        /// Gets the forms.
        /// </summary>
        /// <value>The forms.</value>
        public List<ProfileHeader> ProfileHeaders
        {
            get
            {
                if (profileHeaders == null)
                {
                    var profileHeaderService = ServiceLocator.Current.GetInstance<IProfileHeaderService>();
                    profileHeaders = (List<ProfileHeader>)profileHeaderService.GetProfileHeaders(ProfileTypeId);
                }
                return profileHeaders;
            }
        }

        public ProfileElementViewModel MapFrom(ProfileElement from)
        {
            Id = from.Id;
            Type = from.Type;
            IsRequired = from.IsRequired;
            ProfileHeaderId = from.ProfileHeader != null ? from.ProfileHeader.Id : 0;
            MapLocaleFrom(from.CurrentLocale as ProfileElementLocale);

            return this;
        }

        public ProfileElement MapTo(ProfileElement to)
        {
            to.Id = Id;
            to.IsRequired = IsRequired;
            to.Type = Type;
            to.ProfileHeader = new ProfileHeader { Id = ProfileHeaderId };
            if (String.IsNullOrEmpty(SelectedCulture))
                MapLocaleTo((ProfileElementLocale)to.CurrentLocale);
            return to;
        }

        public ProfileElementViewModel MapLocaleFrom(ProfileElementLocale locale)
        {
            Title = locale.Title;
            Values = locale.ElementValues;
            SelectedCulture = locale.Culture;

            return this;
        }

        public ProfileElementLocale MapLocaleTo(ProfileElementLocale locale)
        {
            locale.Title = Title;
            locale.ElementValues = Values;
            if (SelectedCulture != null)
                locale.Culture = SelectedCulture;
            return locale;
        }
    }
}