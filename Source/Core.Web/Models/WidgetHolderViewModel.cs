using System;
using System.Collections.Generic;
using Core.Framework.Plugins.Web;
using Core.Web.NHibernate.Models;

namespace Core.Web.Models
{
    public class WidgetHolderViewModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the column.
        /// </summary>
        /// <value>The column.</value>
        public int Column { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the widget instance.
        /// </summary>
        /// <value>The widget instance.</value>
        public CoreWidgetInstance WidgetInstance { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public PageWidgetSettings Settings { get; set; }

        /// <summary>
        /// Gets or sets the widget.
        /// </summary>
        /// <value>The widget.</value>
        public ICoreWidget Widget { get; set; }

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