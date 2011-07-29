using System.Collections.Generic;

namespace Core.Web.Models
{
    public class NavigationMenuModel
    {
        /// <summary>
        /// Gets or sets the menu items.
        /// </summary>
        /// <value>The menu items.</value>
        public IEnumerable<NavigationMenuItemModel> MenuItems { get; set; }

        /// <summary>
        /// Gets or sets the page mode.
        /// </summary>
        /// <value>The page mode.</value>
        public PageMode PageMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [manage access].
        /// </summary>
        /// <value><c>true</c> if [manage access]; otherwise, <c>false</c>.</value>
        public bool ManageAccess { get; set; }

    }
}