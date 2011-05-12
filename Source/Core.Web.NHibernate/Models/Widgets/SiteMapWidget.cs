using System;
using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models.Widgets
{
    public class SiteMapWidget:Entity
    {
        public virtual Page RootPage { get; set; }

        public virtual Int32? Depth { get; set; }

        public virtual bool IncludeRootInTree { get; set; }
    }
}
