using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Framework.Permissions.Models;
using Core.Web.Areas.Navigation.Helpers;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Models.Static;
using Core.Web.NHibernate.Models.Widgets;
using Core.Web.NHibernate.Permissions.Operations;
using Framework.Core.DomainModel;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Areas.Navigation.Models
{
    public class ListMenuWidgetModel : IMappedModel<ListMenuWidget, ListMenuWidgetModel>
    {
        #region Fields

        private IEnumerable<Page> _pages;

        private IEnumerable<ListMenuPageItemModel> _pagesTree;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public long Id { get; set; }

        public IEnumerable<Page> Pages
        {
            get
            {
                if (_pages == null)
                {
                     var pageService = ServiceLocator.Current.GetInstance<IPageService>();
                    _pages = pageService.GetAllowedPagesByOperation(HttpContext.Current.User as ICorePrincipal, (int) PageOperations.View);
                }
                return _pages;
            }
        }

        /// <summary>
        /// Gets the pages.
        /// </summary>
        /// <value>The pages.</value>
        public IEnumerable<ListMenuPageItemModel> PagesTree
        {
            get
            {
                if (_pagesTree == null)
                {
                    _pagesTree = ListMenuWidgetHelper.BindListMenuPages(SelectedPages,Pages);
                }
                return _pagesTree;
            }
        }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        /// <value>The orientation.</value>
        public Orientation Orientation { get; set; }

        /// <summary>
        /// Gets or sets the selected pages.
        /// </summary>
        /// <value>The selected pages.</value>
        public IEnumerable<Page> SelectedPages { get; set; }

        /// <summary>
        /// Gets or sets the page ids.
        /// </summary>
        /// <value>The page ids.</value>
        public long[] PageIds { get; set; }

        public ListMenuWidgetModel MapFrom(ListMenuWidget from)
        {
            Id = from.Id;
            Orientation = from.Orientation;
            SelectedPages = from.Pages;
            return this;
        }

        public ListMenuWidget MapTo(ListMenuWidget to)
        {
            to.Id = Id;
            to.Orientation = Orientation;
            to.Pages = PageIds!=null?(from page in Pages where PageIds.Contains(page.Id) select page).ToList():null;
            return to;
        }

        #endregion
    }
}