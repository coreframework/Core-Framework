using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Core.Framework.Permissions.Extensions;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Core.WebContent.NHibernate.Permissions;
using Core.WebContent.NHibernate.Static;
using Framework.Core.DomainModel;
using Framework.Core.Localization;
using Microsoft.Practices.ServiceLocation;

namespace Core.WebContent.Models
{
    public class CategoryViewModel : IMappedModel<WebContentCategory, CategoryViewModel>
    {
        private IDictionary<String, String> cultures;

        private List<Section> sections;

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
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public CategoryStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the section.
        /// </summary>
        /// <value>The section.</value>
        public long SectionId { get; set; }

        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        /// <value>The name of the section.</value>
        public String SectionName { get; set; }

        /// <summary>
        /// Gets the forms.
        /// </summary>
        /// <value>The forms.</value>
        public List<Section> Sections
        {
            get
            {
                if (sections == null)
                {
                    var sectionService = ServiceLocator.Current.GetInstance<ISectionService>();
                    sections = (List<Section>)sectionService.GetAllowedSectionsByOperation(HttpContext.Current.CorePrincipal(), (int)SectionOperations.View);
                }
                return sections;
            }
        }

        /// <summary>
        /// Gets or sets the cultures.
        /// </summary>
        /// <value>The cultures.</value>
        public IDictionary<String, String> Cultures
        {
            get { return cultures ?? (cultures = CultureHelper.GetAvailableCultures()); }
            set { cultures = value; }
        }

        public CategoryViewModel MapFrom(WebContentCategory from)
        {
            Id = from.Id;
            SectionId = from.Section != null ? from.Section.Id : 0;
            Status = from.Status;
            SectionName = from.Section != null ? ((SectionLocale)from.Section.CurrentLocale).Title : String.Empty;
            MapLocaleFrom(from.CurrentLocale as WebContentCategoryLocale);
            return this;
        }

        public WebContentCategory MapTo(WebContentCategory to)
        {
            to.Id = Id;
            to.Section = new Section { Id = SectionId };
            to.Status = Status;

            if (String.IsNullOrEmpty(SelectedCulture))
                MapLocaleTo((WebContentCategoryLocale)to.CurrentLocale);

            return to;
        }

        public CategoryViewModel MapLocaleFrom(WebContentCategoryLocale locale)
        {
            Title = locale.Title;
            Description = locale.Description;
            SelectedCulture = locale.Culture;

            return this;
        }

        public WebContentCategoryLocale MapLocaleTo(WebContentCategoryLocale locale)
        {
            locale.Title = Title;
            locale.Description = Description;
            if (SelectedCulture!=null)
                locale.Culture = SelectedCulture;
            return locale;
        }
    }
}