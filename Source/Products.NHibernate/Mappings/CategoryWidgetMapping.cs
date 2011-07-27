using FluentNHibernate.Mapping;
using Products.NHibernate.Models;

namespace Products.NHibernate.Mappings
{
    public class CategoryWidgetMapping : ClassMap<CategoryWidget>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryWidgetMapping"/> class.
        /// </summary>
        public CategoryWidgetMapping()
        {
            Cache.Region("Product_CategoryWidgets").ReadWrite();
            Table("Product_CategoryWidgets");
            Id(categoryWidgets => categoryWidgets.Id);
            Map(categoryWidgets => categoryWidgets.PageSize);
        }
    }
}
