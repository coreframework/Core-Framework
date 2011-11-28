using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Core.Web.Helpers;
using Core.Web.NHibernate.Models;
using Framework.Core.DomainModel;

namespace Core.Web.Areas.Admin.Models
{
    public class PageTemplateViewModel : IMappedModel<Page, PageTemplateViewModel>
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The template identifier.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public String Title { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [Required]
        public String Url { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has childrent.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has childrent; otherwise, <c>false</c>.
        /// </value>
        public bool HasChildrent { get; set; }

        /// <summary>
        /// Gets or sets the inherited pages count.
        /// </summary>
        /// <value>The inherited pages count.</value>
        public int InheritedPagesCount { get; set; }

        public PageTemplateViewModel MapFrom(Page from)
        {
            Id = from.Id;
            Title = from.Title;
            Url = from.Url;
            HasChildrent = from.InheritedPagesCount > 0;
            InheritedPagesCount = from.InheritedPagesCount;

            return this;
        }

        public Page MapTo(Page to)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            to.Title = Title;
            to.Url = Url = urlHelper.EncodeForSEO(Url);

            return to;
        }

        #region Object members

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override String ToString()
        {
            if (!String.IsNullOrEmpty(Title))
            {
                return Title;
            }

            return base.ToString();
        }

        #endregion
    }
}