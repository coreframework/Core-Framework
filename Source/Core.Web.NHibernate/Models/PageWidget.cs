using System;
using System.Collections.Generic;
using Core.Web.NHibernate.Models.Static;
using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models
{
    public class PageWidget: Entity
    {
        #region Fields

        private Widget widget = new Widget();
        private readonly IList<PageWidget> holderInstances = new List<PageWidget>();
        private PageSection pageSection = PageSection.Body;

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
        public virtual Widget Widget
        {
            get { return widget; }
            set { widget = value; }
        }

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
        public virtual PageSection PageSection
        {
            get { return pageSection; }
            set { pageSection = value; }
        }

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

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the parent widget id.
        /// </summary>
        /// <value>The parent widget id.</value>
        public virtual long? ParentWidgetId { get; set; }

        public virtual long? TemplateWidgetId { get; set; }

        public virtual long EntityId
        {
            get { return Id; }
        }

        public virtual IEnumerable<PageWidget> HolderInstances
        {
            get
            {
                return holderInstances;
            }
        }
    }
}
