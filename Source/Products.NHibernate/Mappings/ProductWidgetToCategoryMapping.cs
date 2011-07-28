using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Products.NHibernate.Models;

namespace Products.NHibernate.Mappings
{
    public class ProductWidgetToCategoryMapping: ClassMap<ProductWidgetToCategory>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductWidgetToCategoryMapping"/> class.
        /// </summary>
        public ProductWidgetToCategoryMapping()
         {
             Cache.Region("Product_ProductWidgetToCategories").ReadWrite();
             Table("Product_ProductWidgetToCategories");
             Id(prodWidCat => prodWidCat.Id);
             Map(prodWidCat => prodWidCat.ProductWidgetId);
             Map(prodWidCat => prodWidCat.CategoryId);
        }
    }
}
