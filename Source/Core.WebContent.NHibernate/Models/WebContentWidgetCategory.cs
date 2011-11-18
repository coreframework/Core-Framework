using FluentNHibernate.Data;

namespace Core.WebContent.NHibernate.Models
{
    public class WebContentWidgetCategory: Entity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public virtual WebContentCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the web content widget.
        /// </summary>
        /// <value>The web content widget.</value>
        public virtual WebContentWidget WebContentWidget { get; set; }

        #endregion
    }
}
