﻿using System.Collections.Generic;
using FluentNHibernate.Data;
using Iesi.Collections.Generic;

namespace Core.Web.NHibernate.Models
{
    public class PageLayout : Entity
    {
        private readonly Iesi.Collections.Generic.ISet<PageLayoutColumnWidthValue> columnWidths = new HashedSet<PageLayoutColumnWidthValue>();

        /// <summary>
        /// Gets or sets the layout template.
        /// </summary>
        /// <value>The layout template.</value>
        public virtual PageLayoutTemplate LayoutTemplate { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>The page.</value>
        public virtual Page Page { get; set; }

        public virtual IEnumerable<PageLayoutColumnWidthValue> ColumnWidths
        {
            get
            {
                return columnWidths;
            }
        }

        #region Helper Methods

        public virtual void AddColumnWidth(PageLayoutColumnWidthValue item)
        {
            columnWidths.Add(item);
        }

        #endregion
    }
}
