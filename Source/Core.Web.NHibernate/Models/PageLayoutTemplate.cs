using System;
using System.Collections.Generic;
using FluentNHibernate.Data;
using Iesi.Collections.Generic;

namespace Core.Web.NHibernate.Models
{
    public class PageLayoutTemplate : Entity
    {
        private readonly Iesi.Collections.Generic.ISet<PageLayoutRow> rows = new HashedSet<PageLayoutRow>();

        public virtual String LayoutCssClass { get; set; }

        public virtual int Priority { get; set; }

        public virtual int ColumnsNumber { get; set; }

        public virtual IEnumerable<PageLayoutRow> Rows
        {
            get
            {
                return rows;
            }
        }
    }
}
