using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;
using Products.NHibernate.Models;

namespace Products.NHibernate.Mappings
{
    public class ProductMapping : ClassMap<Product>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductMapping"/> class.
        /// </summary>
        public ProductMapping()
        {
            Cache.Region("Product_Products").ReadWrite();
            Table("Product_Products");
            Id(product => product.Id);
            Map(product => product.FileName).Length(255);
            Map(product => product.Price);

            HasManyToMany(product => product.Categories)
               .Table("Product_ProductsToCategories").ParentKeyColumn("ProductId")
               .ChildKeyColumn("CategoryId").Cascade.SaveUpdate().LazyLoad();

            HasMany(product => product.CurrentProductLocales).KeyColumn("ProductId")
            .Table("Product_ProductLocales").ApplyFilter<CultureFilter>()
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
            .Inverse()
            .LazyLoad()
            .Cascade.All();
        }
    }
}
