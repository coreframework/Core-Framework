using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models
{
    public class PageLayoutRow : Entity
    {
        private readonly IList<PageLayoutColumn> _columns = new List<PageLayoutColumn>();

        public virtual IEnumerable<PageLayoutColumn> Columns
        {
            get
            {
                return _columns;
            }
        }
        
    }
}
