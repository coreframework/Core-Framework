using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.WebContent.NHibernate.Models;
using Framework.Core.DomainModel;
using Framework.Core.Localization;

namespace Core.WebContent.Models
{
    public class SectionViewModel : IMappedModel<Section, SectionViewModel>
    {
        private IDictionary<String, String> cultures;
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public virtual String Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual String Description { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public virtual long Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow manage].
        /// </summary>
        /// <value><c>true</c> if [allow manage]; otherwise, <c>false</c>.</value>
        public bool AllowManage { get; set; }

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

        public SectionViewModel MapFrom(Section from)
        {
            Id = from.Id;
            MapLocaleFrom(from.CurrentLocale as SectionLocale);
            return this;
        }

        public Section MapTo(Section to)
        {
            to.Id = Id;
            if (String.IsNullOrEmpty(SelectedCulture))
                MapLocaleTo((SectionLocale)to.CurrentLocale);

            return to;
        }

        public SectionViewModel MapLocaleFrom(SectionLocale locale)
        {
            Title = locale.Title;
            Description = locale.Description;
            SelectedCulture = locale.Culture;

            return this;
        }

        public SectionLocale MapLocaleTo(SectionLocale locale)
        {
            locale.Title = Title;
            locale.Description = Description;
            if (SelectedCulture!=null)
                locale.Culture = SelectedCulture;
            return locale;
        }
    }
}