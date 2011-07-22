using System;
using System.Collections.Generic;
using Core.Framework.Plugins.Web;
using Core.Web.NHibernate.Models;

namespace Core.Web.Models
{
    public class WidgetHolderViewModel
    {
        /// <summary>
        /// Gets or sets the widget.
        /// </summary>
        /// <value>The widget.</value>
        public PageWidget Widget { get; set; }

        /// <summary>
        /// Gets or sets the widget instance.
        /// </summary>
        /// <value>The widget instance.</value>
        public CoreWidgetInstance WidgetInstance { get; set; }

        /// <summary>
        /// Gets or sets the widget.
        /// </summary>
        /// <value>The widget.</value>
        public ICoreWidget SystemWidget { get; set; }

        /// <summary>
        /// Gets or sets the access.
        /// </summary>
        /// <value>The access.</value>
        public Dictionary<Int32, bool> Access { get; set; }

        /// <summary>
        /// Gets or sets the page access.
        /// </summary>
        /// <value>The page access.</value>
        public Dictionary<Int32, bool> PageAccess { get; set; }
    }
}