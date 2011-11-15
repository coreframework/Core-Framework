using Core.WebContent.NHibernate.Models;
using Core.WebContent.NHibernate.Static;
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
            Map(article => article.Author);
            Map(article => article.StartPublishingDate);
            Map(article => article.FinishPublishingDate);
            Map(article => article.CreateDate);
            Map(article => article.LastModifiedDate);
            Map(article => article.UrlType).CustomType<ArticleUrlType>();
            Map(article => article.Status).CustomType<ArticleStatus>();
            Map(article => article.Url);
            References(article => article.Category);
            HasMany(article => article.CurrentLocales).KeyColumn("ArticleId")
            .Table("ArticleLocales").ApplyFilter<CultureFilter>()
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
            .Inverse()
            .LazyLoad()
            .Cascade.All();
        }
    }
}
