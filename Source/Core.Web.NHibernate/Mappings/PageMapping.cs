using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

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
            Map(page => page.Url).Length(255);

            Map(page => page.ParentPageId);

            References(page => page.User).Column("UserId").Nullable();

            Map(page => page.OrderNumber);
            HasOne(page => page.PageLayout).PropertyRef(pageLayout => pageLayout.Page).Cascade.All().LazyLoad();
            Map(page => page.HideInMainMenu);
            Map(page => page.IsServicePage);
            Map(page => page.IsTemplate);
            Map(page => page.PlaceHoldersCount).Formula(
                @"(SELECT count(*)
                FROM PageWidgets
                WHERE PageWidgets.PageId = Id AND PageWidgets.WidgetId in (SELECT Widgets.Id FROM Widgets WHERE Widgets.IsPlaceHolder = 1))").LazyLoad();
            Map(page => page.InheritedPagesCount).Formula(
                @"(SELECT count(*)
                FROM Pages
                WHERE Pages.TemplateId = Id)").LazyLoad();

            HasMany(page => page.Widgets).KeyColumn("PageId")
                .Table("PageWidgets").AsSet()
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
                .Inverse()
                .LazyLoad()
                .Cascade.AllDeleteOrphan();

            HasMany(page => page.Children).KeyColumn("ParentPageId")
            .Table("Pages").AsSet()
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
            .Inverse()
            .LazyLoad()
            .Cascade.AllDeleteOrphan();

            HasOne(page => page.Settings).PropertyRef(pageSettings => pageSettings.Page).Cascade.All().LazyLoad();
            HasMany(page => page.CurrentLocales).KeyColumn("PageId")
            .Table("PageLocales").AsSet().ApplyFilter<CultureFilter>()
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
            .Inverse()
            .LazyLoad()
            .Cascade.All();

            References(page => page.Template).Column("TemplateId").Nullable();
        }
    }
}
