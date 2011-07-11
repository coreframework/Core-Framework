using Core.ContentPages.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.ContentPages.NHibernate.Mappings
{
    public class ContentPageMapping : ClassMap<ContentPage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentPageMapping"/> class.
        /// </summary>
        public ContentPageMapping()
        {
            Cache.Region("ContentPages").ReadWrite();
            Table("ContentPages");
            Id(contentPage => contentPage.Id);
            Map(contentPage => contentPage.Title).Length(255);
            Map(contentPage => contentPage.Content);

            HasMany(page => page.Widgets).KeyColumn("ContentPageId")
            .Table("ContentPageWidgets")
            .Inverse()
            .LazyLoad()
            .Cascade.All();
        }
    }
}
