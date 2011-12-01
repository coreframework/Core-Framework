using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Profiles.NHibernate.Models;
using Framework.Core.DomainModel;
using Framework.Core.Localization;

namespace Core.Profiles.Models
{
    public class ProfileTypeViewModel : IMappedModel<ProfileType, ProfileTypeViewModel>
    {
        private IDictionary<String, String> cultures;
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public virtual String Title { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public virtual long Id { get; set; }
       
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

        public ProfileTypeViewModel MapFrom(ProfileType from)
        {
            Id = from.Id;
            MapLocaleFrom(from.CurrentLocale as ProfileTypeLocale);
            return this;
        }

        public ProfileType MapTo(ProfileType to)
        {
            to.Id = Id;
            if (String.IsNullOrEmpty(SelectedCulture))
                MapLocaleTo((ProfileTypeLocale)to.CurrentLocale);

            return to;
        }

        public ProfileTypeViewModel MapLocaleFrom(ProfileTypeLocale locale)
        {
            Title = locale.Title;
            SelectedCulture = locale.Culture;

            return this;
        }

        public ProfileTypeLocale MapLocaleTo(ProfileTypeLocale locale)
        {
            locale.Title = Title;
            if (SelectedCulture != null)
                locale.Culture = SelectedCulture;
            return locale;
        }
    }
}