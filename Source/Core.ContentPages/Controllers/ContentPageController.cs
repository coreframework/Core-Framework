using System.ComponentModel.Composition;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.ContentPages.Models;
using Core.ContentPages.NHibernate.Models;
using Core.ContentPages.Permissions.Operations;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Helpers;
using Microsoft.Practices.ServiceLocation;
using IContentPageService = Core.ContentPages.NHibernate.Contracts.IContentPageService;


namespace Core.ContentPages.Controllers
{
    /// <summary>
    /// Handles module requests.
    /// </summary>
    [Export(typeof(IController)), ExportMetadata("Name", "ContentPage")]
    [Permissions((int)ContentPagePluginOperations.ManageContentPages, typeof(ContentPagePlugin))]
    public partial class ContentPageController : CoreController
    {
        #region Fields
        
        private readonly IContentPageService contentPageService;

        #endregion

        #region Properties

        public override string ControllerPluginIdentifier
        {
            get { return ContentPagePlugin.Instance.Identifier; }
        }

        #endregion
        
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentPageController"/> class.
        /// </summary>
        public ContentPageController()
        {
            this.contentPageService = ServiceLocator.Current.GetInstance<IContentPageService>();
        }

        #endregion

        #region Admin Actions

        /// <summary>
        /// Renders content pages listing.
        /// </summary>
        /// <returns>List of content pages.</returns>
        
        public virtual ActionResult ShowAll()
        {
            return View("Admin/Index", contentPageService.GetAll());
        }

        /// <summary>
        /// Shows content page details by id.
        /// </summary>
        /// <param name="id">The content page id.</param>
        /// <returns>Content page details</returns>
        public virtual ActionResult ShowById(long? id)
        {
            var contentPage = contentPageService.Find(id ?? 0);
            if (contentPage == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Page not found");
            }

            return View("Admin/Show", contentPage);
        }

        /// <summary>
        /// Shows content page edit form.
        /// </summary>
        /// <param name="id">The content id.</param>
        /// <returns>Content page edit view</returns>
        public virtual ActionResult Edit(long? id)
        {
            var contentPage = contentPageService.Find(id ?? 0);
            if (contentPage == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Page not found");
            }

            return View("Admin/Edit", new ContentPageViewModel().MapFrom(contentPage));
        }

        /// <summary>
        /// Updates content page.
        /// </summary>
        /// <param name="id">The content page id.</param>
        /// <param name="contentPage">The content page model.</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public virtual ActionResult Edit(long? id, ContentPageViewModel contentPage)
        {
            if (ModelState.IsValid && id!=null)
            {
                contentPageService.Save(contentPage.MapTo(new ContentPage{Id = (long) id}));
                return RedirectToAction("ShowAll");
            }

            return View("Admin/Edit", contentPage);
        }

        /// <summary>
        /// Shows content page create form.
        /// </summary>
        /// <returns>Content page create form.</returns>
        public virtual ActionResult New()
        {
            return View("Admin/New", new ContentPageViewModel());
        }

        /// <summary>
        /// Saves new content page.
        /// </summary>
        /// <param name="contentPage">The content page.</param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        public virtual ActionResult New(ContentPageViewModel contentPage)
        {
            if (ModelState.IsValid)
            {
                contentPageService.Save(contentPage.MapTo(new ContentPage()));
                return RedirectToAction("ShowAll");
            }

            return View("Admin/New", contentPage);
        }


        /// <summary>
        /// Removes the specified content page.
        /// </summary>
        /// <param name="id">The content page id.</param>
        /// <returns>List of content pages</returns>
        [HttpPost]
        public virtual ActionResult Remove(long id)
        {
            var contentPage = contentPageService.Find(id);
            if (contentPage != null)
            {
                contentPageService.Delete(contentPage);
            }

            return RedirectToAction("ShowAll");
        }

        #endregion
    }
}
