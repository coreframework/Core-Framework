using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.News.NHibernate.Contracts;
using Core.News.Nhibernate.Models;
using Framework.Core.DomainModel;
using Microsoft.Practices.ServiceLocation;

namespace Core.News.Models
{
    public class NewsListingWidgetEditModel : IMappedModel<NewsListingWidget, NewsListingWidgetEditModel>
    {
        #region Fields

        private List<NewsCategory> categories;

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
                if (categories == null)
                {
                    var categoriesService = ServiceLocator.Current.GetInstance<INewsCategoryService>();
                    categories = (List<NewsCategory>)categoriesService.GetAll();
                }
                return categories;
            }
        }

        #endregion

        public NewsListingWidgetEditModel MapFrom(NewsListingWidget from)
        {
            Id = from.Id;
            ItemsOnPage = from.ItemsOnPage;
            ShowPaginator = from.ShowPaginator;
            CategoriesId = from.Categories != null ? from.Categories.Select(t => t.Id).ToArray() : new long[] { };

            return this;
        }

        public NewsListingWidget MapTo(NewsListingWidget to)
        {
            if (Id > 0)
                to.Id = Id;
            to.ItemsOnPage = ItemsOnPage;
            to.ShowPaginator = ShowPaginator;            
            to.Url = String.Empty;
            to.Categories = Categories.Where(t => CategoriesId.Contains(t.Id)).ToList();
            
            return to;
        }
    }
}