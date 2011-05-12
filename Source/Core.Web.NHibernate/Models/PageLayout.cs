using System.Collections.Generic;
using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models
{
    public class PageLayout : Entity
    {
        private readonly IList<PageLayoutColumnWidthValue> _columnWidths = new List<PageLayoutColumnWidthValue>();

        /// <summary>
        /// Gets or sets the layout template.
        /// </summary>
        /// <value>The layout template.</value>
        public virtual PageLayoutTemplate LayoutTemplate { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>The page.</value>
        public virtual Page Page { get; set; }

        public virtual IEnumerable<PageLayoutColumnWidthValue> ColumnWidths
        {
            get
            {
                return _columnWidths;
            }
        }
    }
}
