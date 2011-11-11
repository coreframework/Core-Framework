using Core.WebContent.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.WebContent.NHibernate.Mappings
{
    public class ArticleMapping : ClassMap<Article>
    {
        public ArticleMapping()
        {
            Cache.Region("WebContent_Articles").ReadWrite();
            Table("WebContent_Articles");
            Id(article => article.Id);
            Map(article => article.UserId);
            References(article => article.Section);
            HasMany(article => article.CurrentLocales).KeyColumn("ArticleId")
            .Table("ArticleLocales").ApplyFilter<CultureFilter>()
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
            .Inverse()
            .LazyLoad()
            .Cascade.All();
        }
    }
}
