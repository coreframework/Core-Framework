﻿using System;
using Core.Web.NHibernate.Models.Static;
using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models
{
    public class PageWidget: Entity
    {
        #region Constructor

        public PageWidget()
        {
            Widget = new Widget();
            PageSection = PageSection.Body;
        }

        #endregion

        /// <summary>
        /// Gets or sets the instance id.
        /// </summary>
        /// <value>The instance id.</value>
        public virtual long? InstanceId { get; set; }

        /// <summary>
        /// Gets or sets the widget.
        /// </summary>
        /// <value>The widget.</value>
        public virtual Widget Widget { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>The page.</value>
        public virtual Page Page { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public virtual PageWidgetSettings Settings { get; set; }

        /// <summary>
        /// Gets or sets the page section.
        /// </summary>
        /// <value>The page section.</value>
        public virtual PageSection PageSection { get; set; }

        /// <summary>
        /// Gets or sets the column.
        /// </summary>
        /// <value>The column.</value>
        public virtual Int32 ColumnNumber { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        public virtual Int32 OrderNumber { get; set; }

        public virtual User User { get; set; }

        #region IPermissible Members

        public virtual long EntityId
        {
            get { return Id; }
        }

        #endregion
    }
}
