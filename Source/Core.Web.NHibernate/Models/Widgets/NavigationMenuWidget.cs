using Core.Web.NHibernate.Models.Static;
using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models.Widgets
{
    public class NavigationMenuWidget: Entity
    {
        public virtual Orientation Orientation { get; set; }
    }
}
