using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models
{
    public class PageLayoutColumnWidthValue : Entity
    {
        /// <summary>
        /// Gets or sets the column.
        /// </summary>
        /// <value>The column.</value>
        public virtual PageLayoutColumn Column { get; set; }

        /// <summary>
        /// Gets or sets the page layout.
        /// </summary>
        /// <value>The page layout.</value>
        public virtual PageLayout PageLayout { get; set; }

        /// <summary>
        /// Gets or sets the width value.
        /// </summary>
        /// <value>The width value.</value>
        public virtual int WidthValue { get; set; }

        /// <summary>
        /// Gets or sets the colspan.
        /// </summary>
        /// <value>The colspan.</value>
        public virtual int? Colspan { get; set; }
    }
}
