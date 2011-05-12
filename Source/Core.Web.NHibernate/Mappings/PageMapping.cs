using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings
{
    public class PageMapping : ClassMap<Page>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginMapping"/> class.
        /// </summary>
        public PageMapping()
        {
            Cache.Region("Pages").ReadWrite();
            Table("Pages");
            Id(page => page.Id);
            Map(page => page.Title).Length(255);
            Map(page => page.Url).Length(255);

            Map(page => page.ParentPageId);

            References(page => page.User).Column("UserId").Nullable();

            Map(page => page.OrderNumber);
            HasOne(page => page.PageLayout).PropertyRef(pageLayout => pageLayout.Page).Cascade.All().LazyLoad();

            HasMany(page => page.Widgets).KeyColumn("PageId")
                .Table("PageWidgets")
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .Inverse()
                .LazyLoad()
                .Cascade.AllDeleteOrphan();

            HasMany(page => page.Children).KeyColumn("ParentPageId")
            .Table("Pages")
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
            .Inverse()
            .LazyLoad()
            .Cascade.AllDeleteOrphan();

            HasOne(page => page.Settings).PropertyRef(pageSettings => pageSettings.Page).Cascade.All().LazyLoad();

        }
    }
}
