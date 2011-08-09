using System.ComponentModel.DataAnnotations;
using Framework.Core.DomainModel;
using Core.News.Nhibernate.Models;

namespace Core.News.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CategoryWidgetModel : IMappedModel<NewsCategoryWidget, CategoryWidgetModel>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        /// <value>The page size.</value>
        [Required]
        [Range(0, 100)]
        public int PageSize { get; set; }
        #endregion

        public CategoryWidgetModel MapFrom(NewsCategoryWidget from)
        {
            Id = from.Id;
            PageSize = from.PageSize;
            return this;
        }

        public NewsCategoryWidget MapTo(NewsCategoryWidget to)
        {
            to.Id = Id;
            to.PageSize = PageSize;
            return to;
        }
    }
}