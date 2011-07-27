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
            //References(productWidget => productWidget.Category).Column("CategoryId").LazyLoad().Not.Nullable();
           
            HasManyToMany(productWidget => productWidget.Categories)
                .Table("Product_ProductWidgetToCategories").ParentKeyColumn("ProductWidgetId")
                .ChildKeyColumn("CategoryId").Cascade.SaveUpdate().LazyLoad();

        }
    }
}