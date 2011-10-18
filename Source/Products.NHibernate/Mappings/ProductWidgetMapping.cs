using FluentNHibernate.Mapping;
using Products.NHibernate.Models;

namespace Products.NHibernate.Mappings
{
    public class ProductWidgetMapping : ClassMap<ProductWidget>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductWidgetMapping"/> class.
        /// </summary>
        public ProductWidgetMapping()
        {
            Cache.Region("Product_ProductWidgets").ReadWrite();
            Table("Product_ProductWidgets");
            Id(productWidget => productWidget.Id);
            Map(productWidget => productWidget.PageSize);

            HasMany(page => page.Categories).KeyColumn("ProductWidgetId")
              .Table("Product_ProductWidgetToCategories")
              .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
              .Inverse()
              .LazyLoad()
              .Cascade.AllDeleteOrphan();
        }
    }
}