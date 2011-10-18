using Core.ContentPages.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

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
            HasMany(page => page.Widgets).KeyColumn("ContentPageId")
            .Table("ContentPageWidgets")
            .Inverse()
            .LazyLoad()
            .Cascade.All();
            HasMany(contentPage => contentPage.CurrentContentPageLocales).KeyColumn("ContentPageId")
            .Table("ContentPageLocales").ApplyFilter<CultureFilter>()
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
            .Inverse()
            .LazyLoad()
            .Cascade.All();
        }
    }
}
