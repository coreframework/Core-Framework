using Core.News.Nhibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.News.Nhibernate.Mappings
{
    public class NewsCategoryLocaleMapping : ClassMap<NewsCategoryLocale>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsCategoryLocaleMapping"/> class.
        /// </summary>
        public NewsCategoryLocaleMapping()
        {
            Cache.Region("News_CategoryLocales").ReadWrite();
            Table("News_CategoryLocales");
            Id(categoryLocale => categoryLocale.Id);
            References(categoryLocale => categoryLocale.Category).Column("CategoryId").LazyLoad().Not.Nullable();
            Map(categoryLocale => categoryLocale.Culture).Length(5);
            Map(categoryLocale => categoryLocale.Title).Length(255);
            Map(categoryLocale => categoryLocale.Description);
            Map(categoryLocale => categoryLocale.Priority).Formula(CultureFilter.CultureFilterPriorityExpression());
        }
    }
}
