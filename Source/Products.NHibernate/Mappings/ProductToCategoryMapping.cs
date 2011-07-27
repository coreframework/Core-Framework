using FluentNHibernate.Mapping;
using Products.NHibernate.Models;

namespace Products.NHibernate.Mappings
{
    public class ProductToCategoryMapping : ClassMap<ProductToCategory>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductToCategoryMapping"/> class.
        /// </summary>
        public ProductToCategoryMapping()
         {
             Cache.Region("Product_ProductsToCategories").ReadWrite();
             Table("Product_ProductsToCategories");
             Id(prodCat => prodCat.Id);
             Map(prodCat => prodCat.ProductId);
             Map(prodCat => prodCat.CategoryId);
        }
    }
}
