using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Core.Framework.Permissions.Extensions;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Core.WebContent.NHibernate.Permissions;
using Framework.Core.DomainModel;
using Framework.Core.Localization;
using Microsoft.Practices.ServiceLocation;

namespace Core.WebContent.Models
{
    public class ArticleViewModel : IMappedModel<Article, ArticleViewModel>
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
        /// Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        [Required]
        public virtual String Summary { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [DataType("FckEditorText"), Required]
        public virtual String Content { get; set; }

        /// <summary>
        /// Gets or sets the published date.
        /// </summary>
        /// <value>The published date.</value>
        [DataType(DataType.Date)]
        public virtual DateTime PublishedDate { get; set; }

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
        /// Gets or sets the section.
        /// </summary>
        /// <value>The section.</value>
        public long SectionId { get; set; }

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

        public ArticleViewModel MapFrom(Article from)
        {
            Id = from.Id;
            SectionId = from.Section != null ? from.Section.Id : 0;

            MapLocaleFrom(from.CurrentLocale as ArticleLocale);
            return this;
        }

        public Article MapTo(Article to)
        {
            to.Id = Id;
            to.Section = new Section { Id = SectionId };

            if (String.IsNullOrEmpty(SelectedCulture))
                MapLocaleTo((ArticleLocale)to.CurrentLocale);

            return to;
        }

        public ArticleViewModel MapLocaleFrom(ArticleLocale locale)
        {
            Title = locale.Title;
            Content = locale.Description;
            SelectedCulture = locale.Culture;

            return this;
        }

        public ArticleLocale MapLocaleTo(ArticleLocale locale)
        {
            locale.Title = Title;
            locale.Description = Content;
            if (SelectedCulture!=null)
                locale.Culture = SelectedCulture;
            return locale;
        }
    }
}