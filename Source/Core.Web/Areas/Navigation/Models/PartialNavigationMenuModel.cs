using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Web.NHibernate.Models.Static;

namespace Core.Web.Areas.Navigation.Models
{
    public class PartialNavigationMenuModel
    {

        /// <summary>
        /// Gets or sets the menu items.
        /// </summary>
        /// <value>The menu items.</value>
        public IEnumerable<PartialNavigationMenuItemModel> MenuItems { get; set; }

        /// <summary>
        /// Gets or sets the widget Id.
        /// </summary>
        /// <value>The widget Id.</value>
        public long WidgetId { get; set; }
       
        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        /// <value>The orientation.</value>
        public Orientation Orientation { get; set; }
    }
}