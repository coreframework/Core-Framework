using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Web.Helpers.HtmlExtensions.MenuTreeView;
using Core.Web.Models;
using Core.Web.NHibernate.Models;

namespace Core.Web.Areas.Navigation.Models
{
    public class PartialNavigationMenuItemModel : IComposite<PartialNavigationMenuItemModel>
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
        public PartialNavigationMenuItemModel Parent { get; set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>The children.</value>
        public IEnumerable<PartialNavigationMenuItemModel> Children { get; set; }
    }
}
