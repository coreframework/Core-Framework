using Core.Web.NHibernate.Models.Static;
using Core.Web.NHibernate.Models.Widgets;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings.Widgets
{
    public class ListMenuWidgetMapping: ClassMap<ListMenuWidget>
    {
        public ListMenuWidgetMapping()
        {
            Cache.Region("ListMenuWidgets").ReadWrite();
            Table("ListMenuWidgets");
            Id(listMenuWidget => listMenuWidget.Id);
            Map(listMenuWidget => listMenuWidget.Orientation).CustomType(typeof(Orientation));
            HasManyToMany(post => post.Pages)
             .Table("ListMenuWidgets_Pages")
             .ParentKeyColumn("ListMenuWidgetId")
             .ChildKeyColumn("PageId")
             .Cascade.SaveUpdate()
             .AsSet().LazyLoad();
        }
    }
}
