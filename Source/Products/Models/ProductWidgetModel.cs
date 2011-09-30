using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Framework.Core.DomainModel;
using Microsoft.Practices.ServiceLocation;
using Products.NHibernate.Contracts;
using Products.NHibernate.Models;

namespace Products.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductWidgetModel : IMappedModel<ProductWidget, ProductWidgetModel>
    {
        #region Fields

        private List<Category> categories;

        #endregion

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
        [Range(1, 100)]
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        [Required]
        public long[] CategoriesId { get; set; }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <value>The categories.</value>
        public List<Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    var categoriesService = ServiceLocator.Current.GetInstance<ICategoryService>();
                    categories = (List<Category>)categoriesService.GetAll();
                }
                return categories;
            }
        }

        #endregion

        public ProductWidgetModel MapFrom(ProductWidget from)
        {
            Id = from.Id;
            PageSize = from.PageSize;
            CategoriesId = from.Categories != null ?  from.Categories.Select(t => t.Category.Id).ToArray() : new long[]{};
            return this;
        }

        public ProductWidget MapTo(ProductWidget to)
        {
            to.Id = Id;
            to.PageSize = PageSize;

            foreach (var category in CategoriesId.Select(item => Categories.Find(ct => ct.Id == item)).Where(category => category != null))
            {
                to.AddCategory(new ProductWidgetToCategory { Category = category, ProductWidget = to});
            }
            return to;
        }
    }
}