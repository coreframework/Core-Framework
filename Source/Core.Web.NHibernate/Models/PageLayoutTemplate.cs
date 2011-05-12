using System;
using System.Collections.Generic;
using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models
{
    public class PageLayoutTemplate : Entity
    {
        private readonly IList<PageLayoutRow> _rows = new List<PageLayoutRow>();

        public virtual String LayoutCssClass { get; set; }

        public virtual int Priority { get; set; }

        public virtual int ColumnsNumber { get; set; }

        public virtual IEnumerable<PageLayoutRow> Rows
        {
            get
            {
                return _rows;
            }
        }
    }
}
