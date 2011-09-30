using System.Collections.Generic;
using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models
{
    public class PageLayoutRow : Entity
    {
        private readonly IList<PageLayoutColumn> columns = new List<PageLayoutColumn>();

        public virtual IEnumerable<PageLayoutColumn> Columns
        {
            get
            {
                return columns;
            }
        }
    }
}
