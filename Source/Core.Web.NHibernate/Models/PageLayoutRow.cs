using System.Collections.Generic;
using FluentNHibernate.Data;
using Iesi.Collections.Generic;

namespace Core.Web.NHibernate.Models
{
    public class PageLayoutRow : Entity
    {
        private readonly Iesi.Collections.Generic.ISet<PageLayoutColumn> columns = new HashedSet<PageLayoutColumn>();

        public virtual IEnumerable<PageLayoutColumn> Columns
        {
            get
            {
                return columns;
            }
        }
    }
}
