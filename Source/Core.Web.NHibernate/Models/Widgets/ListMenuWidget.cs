using System.Collections.Generic;
using Core.Web.NHibernate.Models.Static;
using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models.Widgets
{
    public class ListMenuWidget:Entity
    {
        public virtual Orientation Orientation { get; set; }

        public virtual IEnumerable<Page> Pages { get; set; }
    }
}
