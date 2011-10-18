using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;
using Products.NHibernate.Models;

namespace Products.NHibernate.Mappings
{
    public class ProductLocaleMapping : ClassMap<ProductLocale>
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="ProductLocaleMapping"/> class.
        /// </summary>
        public ProductLocaleMapping()
        {
            Cache.Region("Product_ProductLocales").ReadWrite();
            Table("Product_ProductLocales");
            Id(productLocale => productLocale.Id);
            References(productLocale => productLocale.Product).Column("ProductId").LazyLoad().Not.Nullable();
            Map(productLocale => productLocale.Culture).Length(5);
            Map(productLocale => productLocale.Title).Length(255);
            Map(productLocale => productLocale.Description);
            Map(widgetLocale => widgetLocale.Priority).Formula(CultureFilter.CultureFilterPriorityExpression());
        }
    }
}
