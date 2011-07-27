using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Products.NHibernate.Contracts;
using Products.NHibernate.Models;

namespace Products.Helpers
{
    public class ProductHelper
    {
        /// <summary>
        /// Updates the categories to product assignment.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="ids">The ids.</param>
        /// <param name="selids">The selected ids.</param>
        /// <returns></returns>
        public static bool UpdateCategoriesToProductAssignment(Product product, IEnumerable<String> ids, IEnumerable<String> selids)
        {
            var productService = ServiceLocator.Current.GetInstance<IProductService>();
            var categoryService = ServiceLocator.Current.GetInstance<ICategoryService>();

            var notselids = ids.Where(t => !selids.Contains(t)).ToList();

            var noselected = product.Categories.Where(t => notselids.Contains(t.Id.ToString())).ToList();
            foreach (var category in noselected)
            {
                product.Categories.Remove(category);
            }

            foreach (var selid in selids)
            {
                string selid1 = selid;
                if (!product.Categories.Any(t => t.Id.ToString() == selid1))
                {
                    long selectedID;
                    if (long.TryParse(selid1, out selectedID))
                    {
                        product.Categories.Add(categoryService.Find(selectedID));
                    }
                }
            }

            return productService.Save(product);
        }
    }
}