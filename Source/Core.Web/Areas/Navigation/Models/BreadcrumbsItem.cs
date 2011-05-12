using System;

namespace Core.Web.Areas.Navigation.Models
{
    public class BreadcrumbsItem
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public String Title { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public String Url { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is home page.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is home page; otherwise, <c>false</c>.
        /// </value>
        public bool IsHomePage { get; set; }
    }
}