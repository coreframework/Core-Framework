using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models
{
    public class PageLayoutColumn : Entity
    {
        /// <summary>
        /// Gets or sets the default width value.
        /// </summary>
        /// <value>The default width value.</value>
        public virtual int DefaultWidthValue { get; set; }

        /// <summary>
        /// Gets or sets the default colspan.
        /// </summary>
        /// <value>The default colspan.</value>
        public virtual int? DefaultColspan { get; set; }
    }
}
