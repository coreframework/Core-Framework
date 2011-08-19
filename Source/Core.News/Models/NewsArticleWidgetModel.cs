using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.News.NHibernate.Contracts;
using Framework.Core.DomainModel;
using Microsoft.Practices.ServiceLocation;
using Core.News.Nhibernate.Models;

namespace Core.News.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class NewsArticleWidgetModel : IMappedModel<NewsArticleWidget, NewsArticleWidgetModel>
    {
        #region Fields

        private List<NewsCategory> _categories;

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
        public int ItemsOnPage { get; set; }

        [Required]
        public bool ShowPaginator { get; set; }

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
        public List<NewsCategory> Categories
        {
            get
            {
                if (_categories == null)
                {
                    var categoriesService = ServiceLocator.Current.GetInstance<INewsCategoryService>();
                    _categories = (List<NewsCategory>)categoriesService.GetAll();
                }
                return _categories;
            }
        }

        #endregion

        public NewsArticleWidgetModel MapFrom(NewsArticleWidget from)
        {
            Id = from.Id;
            ItemsOnPage = from.ItemsOnPage;
            ShowPaginator = from.ShowPaginator;
            CategoriesId = from.Categories != null ?  from.Categories.Select(t => t.Category.Id).ToArray() : new long[]{};
            return this;
        }

        public NewsArticleWidget MapTo(NewsArticleWidget to)
        {
            if (Id > 0)
                to.Id = Id;
            to.ItemsOnPage = ItemsOnPage;
            to.ShowPaginator = ShowPaginator;
            //to.Categories = Categories.Where(t => CategoriesId.Contains(t.Id)).ToList();
            if (to.Id > 0)
            foreach (var category in CategoriesId.Select(item => Categories.Find(ct => ct.Id == item)).Where(category => category != null))
            {
                to.AddCategory(new NewsArticleWidgetToCategories { Category = category, NewsArticleWidget = to });
            }
            return to;
        }
    }
}