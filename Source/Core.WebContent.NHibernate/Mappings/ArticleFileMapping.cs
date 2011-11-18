using Core.WebContent.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.WebContent.NHibernate.Mappings
{
    public class ArticleFileMapping: ClassMap<ArticleFile>
    {
        public ArticleFileMapping()
        {
            Cache.Region("WebContent_ArticleFiles").ReadWrite();
            Table("WebContent_ArticleFiles");
            Id(file => file.Id);
            Map(file => file.Title).Length(255);
            Map(file => file.FileName);
            References(file => file.Article).Column("ArticleId").LazyLoad().Not.Nullable();
        }
    }
}
