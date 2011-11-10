using Core.WebContent.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.WebContent.NHibernate.Mappings
{
    public class CategoryLocaleMapping: ClassMap<WebContentCategoryLocale>
    {
        public CategoryLocaleMapping()
        {
            Cache.Region("WebContent_CategoryLocales").ReadWrite();
            Table("WebContent_CategoryLocales");
            Id(category => category.Id);
            Map(formlocale => formlocale.Title).Length(255);
            Map(formlocale => formlocale.Culture);
            Map(formlocale => formlocale.Description);
            References(formlocale => formlocale.Category).Column("CategoryId").LazyLoad().Not.Nullable();
            Map(widgetLocale => widgetLocale.Priority).Formula(CultureFilter.CultureFilterPriorityExpression());
        }
    }
}
