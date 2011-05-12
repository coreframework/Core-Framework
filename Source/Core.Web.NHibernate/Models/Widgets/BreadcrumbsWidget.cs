using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models.Widgets
{
    public class BreadcrumbsWidget: Entity
    {
        public virtual bool ShowHomePage { get; set; }
    }
}
