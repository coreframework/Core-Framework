using System.Collections.Generic;
using Core.Web.Helpers.HtmlExtensions.MenuTreeView;
using Core.Web.NHibernate.Models;

namespace Core.Web.Areas.Navigation.Models
{
    public class ListMenuPageItemModel : IComposite<ListMenuPageItemModel>
    {
        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>The page.</value>
        public Page Page { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this page is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public ListMenuPageItemModel Parent { get; set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>The children.</value>
        public IEnumerable<ListMenuPageItemModel> Children { get; set; }
    }
}