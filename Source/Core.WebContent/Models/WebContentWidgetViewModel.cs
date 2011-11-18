using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Core.Framework.Permissions.Extensions;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Core.WebContent.NHibernate.Permissions;
using Core.WebContent.NHibernate.Static;
using Framework.Core.DomainModel;
using Microsoft.Practices.ServiceLocation;

namespace Core.WebContent.Models
{
    public class WebContentWidgetViewModel : IMappedModel<WebContentWidget, WebContentWidgetViewModel>
    {
        #region Fields

        private List<Section> sections;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show pagination].
        /// </summary>
        /// <value><c>true</c> if [show pagination]; otherwise, <c>false</c>.</value>
        public bool ShowPagination { get; set; }

        /// <summary>
        /// Gets or sets the items number.
        /// </summary>
        /// <value>The items number.</value>
        [Range(1, 100)]
        public int ItemsNumber { get; set; }

        /// <summary>
        /// Gets or sets the view mode.
        /// </summary>
        /// <value>The view mode.</value>
        [Required]
        public WebContentWidgetViewMode ViewMode { get; set;}

        /// <summary>
        /// Gets or sets the section id.
        /// </summary>
        /// <value>The section id.</value>
        [Required]
        public long SectionId { get; set; }

        /// <summary>
        /// Gets or sets the article id.
        /// </summary>
        /// <value>The article id.</value>
        public long? ArticleId { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        [Required]
        public long[] CategoriesId { get; set; }

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
                    sections = (List<Section>)sectionService.GetAllowedSectionsByOperation(HttpContext.Current.CorePrincipal(), (int)SectionOperations.AddToWidget);
                }
                return sections;
            }
        }

        #endregion

        public WebContentWidgetViewModel MapFrom(WebContentWidget from)
        {
            Id = from.Id;
            ItemsNumber = from.ItemsNumber;
            ShowPagination = from.ShowPagination;
            ArticleId = from.Article != null ? from.Article.Id : 0;
            CategoriesId = from.Categories != null ? from.Categories.Select(t => t.Category.Id).ToArray() : new long[] { };
            SectionId = from.Section != null ? from.Section.Id : 0;
            return this;
        }

        public WebContentWidget MapTo(WebContentWidget to)
        {
            to.Id = Id;
            to.ItemsNumber = ItemsNumber;
            to.ShowPagination = ShowPagination;
            to.Article = ArticleId != null ? new Article { Id = (long) ArticleId } : null;
            to.ViewMode = ViewMode;
            to.Section = new Section { Id = SectionId };

            foreach (var categoryId in CategoriesId)
            {
                to.AddCategory(new WebContentWidgetCategory { Category = new WebContentCategory { Id = categoryId }, WebContentWidget = to });
            }
 
            return to;
        }
    }
}
