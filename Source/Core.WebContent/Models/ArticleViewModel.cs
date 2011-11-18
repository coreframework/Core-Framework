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
        /// Gets or sets the author.
        /// </summary>
        /// <value>The author.</value>
        public virtual String Author { get; set; }

        /// <summary>
        /// Gets or sets the published date.
        /// </summary>
        /// <value>The published date.</value>
        [DataType(DataType.Date)]
        public virtual DateTime? StartPublishingDate { get; set; }

        /// <summary>
        /// Gets or sets the finish publishing date.
        /// </summary>
        /// <value>The finish publishing date.</value>
        [DataType(DataType.Date)]
        public virtual DateTime? FinishPublishingDate { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public ArticleStatus Status { get; set; }

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
        [Required]
        public long SectionId { get; set; }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        /// <value>The category id.</value>
        [Required]
        public long CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [Required]
        public String Url { get; set; }

        /// <summary>
        /// Gets or sets the type of the URL.
        /// </summary>
        /// <value>The type of the URL.</value>
        public ArticleUrlType UrlType { get; set; }

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
            CategoryId = from.Category != null ? from.Category.Id : 0;
            SectionId = from.Category != null && from.Category.Section!=null ? from.Category.Section.Id : 0;
            Author = from.Author;
            Status = from.Status;
            Url = from.Url;
            UrlType = from.UrlType;
            StartPublishingDate = from.StartPublishingDate;
            FinishPublishingDate = from.FinishPublishingDate;
            MapLocaleFrom(from.CurrentLocale as ArticleLocale);
            return this;
        }

        public Article MapTo(Article to)
        {
            to.Id = Id;
            to.Category = new WebContentCategory { Id = CategoryId };
            to.Author = Author;
            to.Status = Status;
            to.Url = Url;
            to.UrlType = UrlType;
            to.StartPublishingDate = StartPublishingDate;
            to.FinishPublishingDate = FinishPublishingDate;
            if (Id>0)
            {
                to.LastModifiedDate = DateTime.Now;
            }

            if (String.IsNullOrEmpty(SelectedCulture))
                MapLocaleTo((ArticleLocale)to.CurrentLocale);

            return to;
        }

        public ArticleViewModel MapLocaleFrom(ArticleLocale locale)
        {
            Title = locale.Title;
            Summary = locale.Summary;
            Content = locale.Description;
            SelectedCulture = locale.Culture;

            return this;
        }

        public ArticleLocale MapLocaleTo(ArticleLocale locale)
        {
            locale.Title = Title;
            locale.Description = Content;
            locale.Summary = Summary;
            if (SelectedCulture!=null)
                locale.Culture = SelectedCulture;
            return locale;
        }
    }
}