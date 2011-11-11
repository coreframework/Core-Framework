using Core.WebContent.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.WebContent.NHibernate.Mappings
{
    public class ArticleLocaleMapping: ClassMap<ArticleLocale>
    {
        public ArticleLocaleMapping()
        {
            Cache.Region("WebContent_ArticleLocales").ReadWrite();
            Table("WebContent_ArticleLocales");
            Id(article => article.Id);
            Map(articlelocale => articlelocale.Title).Length(255);
            Map(articlelocale => articlelocale.Culture);
            Map(articlelocale => articlelocale.Description);
            References(articlelocale => articlelocale.Article).Column("ArticleId").LazyLoad().Not.Nullable();
            Map(widgetLocale => widgetLocale.Priority).Formula(CultureFilter.CultureFilterPriorityExpression());
        }
    }
}
