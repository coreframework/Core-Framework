using System.Collections.Generic;
using Core.Web.Helpers.HtmlExtensions.MenuTreeView;
using Core.Web.NHibernate.Models;

namespace Core.Web.Models
{
    public class NavigationMenuModel : IComposite<NavigationMenuModel>
    {
        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>The page.</value>
        public Page Page { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is current.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is current; otherwise, <c>false</c>.
        /// </value>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public NavigationMenuModel Parent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remove access].
        /// </summary>
        /// <value><c>true</c> if [remove access]; otherwise, <c>false</c>.</value>
        public bool RemoveAccess { get; set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>The children.</value>
        public IEnumerable<NavigationMenuModel> Children { get; set; }
    }
}