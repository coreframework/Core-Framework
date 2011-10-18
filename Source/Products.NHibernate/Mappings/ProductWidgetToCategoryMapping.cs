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
             References(prodWidCat => prodWidCat.ProductWidget);
             References(prodWidCat => prodWidCat.Category);
        }
    }
}
