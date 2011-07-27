using FluentNHibernate.Mapping;
using Products.NHibernate.Models;

namespace Products.NHibernate.Mappings
{
    public class CategoryLocaleMapping : ClassMap<CategoryLocale>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryLocaleMapping"/> class.
        /// </summary>
        public CategoryLocaleMapping()
        {
            Cache.Region("Product_CategoryLocales").ReadWrite();
            Table("Product_CategoryLocales");
            Id(categoryLocale => categoryLocale.Id);
            References(categoryLocale => categoryLocale.Category).Column("CategoryId").LazyLoad().Not.Nullable();
            Map(categoryLocale => categoryLocale.Culture).Length(5);
            Map(categoryLocale => categoryLocale.Title).Length(255);
            Map(categoryLocale => categoryLocale.Description);
        }
    }
}
