using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Models.Widgets;
using Core.Web.NHibernate.Permissions.Operations;
using Framework.Core.DomainModel;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Areas.Navigation.Models
{
    public class SiteMapWidgetModel : IMappedModel<SiteMapWidget, SiteMapWidgetModel>
    {
        #region Fields

        private List<Page> _rootPages;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the content page.
        /// </summary>
        /// <value>The content page.</value>
        public long? RootPageId { get; set; }

        /// <summary>
        /// Gets or sets the depth.
        /// </summary>
        /// <value>The depth.</value>
        public Int32? Depth { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include root in tree].
        /// </summary>
        /// <value><c>true</c> if [include root in tree]; otherwise, <c>false</c>.</value>
        [DisplayName("Include root in tree")]
        public bool IncludeRootInTree { get; set; }

        /// <summary>
        /// Gets the content pages.
        /// </summary>
        /// <value>The content pages.</value>
        public List<Page> RootPages
        {
            get
            {
                if (_rootPages == null)
                {
                    var contentPageService = ServiceLocator.Current.GetInstance<IPageService>();
                    _rootPages = (List<Page>)contentPageService.GetAllowedPagesByOperation(HttpContext.Current.User as ICorePrincipal,(Int32)PageOperations.View);
                }
                return _rootPages;
            }
        }

        #endregion

        public SiteMapWidgetModel MapFrom(SiteMapWidget from)
        {
            Id = from.Id;
            RootPageId = from.RootPage != null ? from.RootPage.Id : 0;
            IncludeRootInTree = from.IncludeRootInTree;
            Depth = from.Depth;
            return this;
        }

        public SiteMapWidget MapTo(SiteMapWidget to)
        {
            to.Id = Id;
            if (RootPageId!=null)
            {
                 to.RootPage = new Page()
                {
                    Id = (long) RootPageId
                };
            }
           
            to.IncludeRootInTree = IncludeRootInTree;
            to.Depth = Depth;
            return to;
        }
    }
}