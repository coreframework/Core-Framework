using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Web.Areas.Admin.Models;
using Core.Web.Helpers;
using Core.Web.Helpers.PagedList;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.MVC.Extensions;
using Framework.MVC.Helpers;
using Microsoft.Practices.ServiceLocation;
using System.Linq;

namespace Core.Web.Areas.Admin.Controllers
{
    [Permissions((int)BaseEntityOperations.Manage,typeof(UserGroup))]
    public partial class UserGroupController : Controller
    {
        #region Fields

        private readonly IUserGroupService userGroupService;

        #endregion

        #region Constructors

        public UserGroupController()
        {
            userGroupService = ServiceLocator.Current.GetInstance<IUserGroupService>();
        }

        #endregion

        #region Actions

        [HttpGet]
        public virtual ActionResult Index()
        {
            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>();
            columns.Add(new GridColumnViewModel { Name = "Name", Index = "Name"});
            columns.Add(new GridColumnViewModel { Name = "Users list", Width = 150, Align = "center", Sortable = false});
            columns.Add(new GridColumnViewModel { Name = "Remove", Width = 150, Align = "center", Sortable = false});
            columns.Add(new GridColumnViewModel { Name = "Id", Sortable = false, Hidden = true});
            GridViewModel model = new GridViewModel
                                      {
                                          DataUrl = Url.Action(MVC.Admin.UserGroup.DynamicGridData()),
                                          DetailsUrl = String.Format("{0}/", Url.Action(MVC.Admin.UserGroup.Edit())),
                                          DefaultOrderColumn = "Name",
                                          GridTitle = "User groups",
                                          Columns = columns
                                      };
            return View(model);
        }

        [HttpPost]
        public virtual JsonResult DynamicGridData(int page, int rows, string search, string sidx, string sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            IQueryable<UserGroup> searchQuery = userGroupService.GetSearchQuery(search);
            int totalRecords = userGroupService.GetCount(searchQuery);
            int totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var userGroups = searchQuery.OrderBy(sidx + " " + sord).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from userGroup in userGroups
                    select new
                    {
                        id = userGroup.Id,
                        cell = new[] { userGroup.Name,
                            String.Format("<a href=\"{0}\">{1}</a>",
                                Url.Action(MVC.Admin.UserGroup.Users(userGroup.Id)),
                                HttpContext.Translate("Users", ResourceHelper.GetControllerScope(this))),
                            String.Format("<a href=\"{0}\">{1}</a>",
                                Url.Action(MVC.Admin.UserGroup.Remove(userGroup.Id)),
                                HttpContext.Translate("Remove", ResourceHelper.GetControllerScope(this)))}
                    }).ToArray()
            };
            return Json(jsonData);
        }

        [HttpGet]
        public virtual ActionResult New()
        {
            return View(new UserGroupViewModel().MapFrom(new UserGroup()));
        }

        /// <summary>
        /// Creates the specified user group view.
        /// </summary>
        /// <param name="userGroupView">The user group view.</param>
        /// <returns></returns>
        [HttpPut]
        public virtual ActionResult Create(UserGroupViewModel userGroupView)
        {
            if (ModelState.IsValid)
            {
                var userGroup = new UserGroup();
                userGroupView.MapTo(userGroup);
                userGroupService.Save(userGroup);
                //Success(Translate("Messages.RoleCreated"));
                return RedirectToAction(MVC.Admin.UserGroup.Users(userGroup.Id));
            }

            //            Error(Translate("Messages.ValidationError"));
            return View("New", userGroupView);
        }

        /// <summary>
        /// Render edit form for user group specified.
        /// </summary>
        /// <param name="id">The user group id.</param>
        /// <returns>User group edit view.</returns>
        [HttpGet]
        public virtual ActionResult Edit(long id)
        {
            var userGroup = userGroupService.Find(id);
            if (userGroup == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }

            return View(new UserGroupViewModel().MapFrom(userGroup));
        }

        /// <summary>
        /// Updates user group details.
        /// </summary>
        /// <param name="id">The user group id.</param>
        /// <param name="userGroupView">The user group view model.</param>
        /// <returns>Redirect back to user group list.</returns>
        [HttpPost]
        public virtual ActionResult Update(long id, UserGroupViewModel userGroupView)
        {
            var userGroup = userGroupService.Find(id);
            if (userGroup == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }

            if (ModelState.IsValid)
            {
                userGroupView.MapTo(userGroup);
                userGroupService.Save(userGroup);
                //                Success(Translate("Messages.RoleUpdated"));
                return RedirectToAction(MVC.Admin.UserGroup.Index());
            }

            //            Error(Translate("Messages.ValidationError"));
            return View("Edit", userGroupView);
        }

        /// <summary>
        /// Render delete confirmation view.
        /// </summary>
        /// <param name="id">The user group id.</param>
        /// <returns>User group remove confirmation view.</returns>
        [HttpGet]
        public virtual ActionResult Remove(long id)
        {
            var userGroup = userGroupService.Find(id);
            if (userGroup == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }
            return View(new UserGroupViewModel().MapFrom(userGroup));
        }

        /// <summary>
        /// Confirm user group removing.
        /// </summary>
        /// <param name="id">The user group id.</param>
        /// <returns>Redirects back to user groups list.</returns>
        [HttpDelete]
        public virtual ActionResult ConfirmRemove(long id)
        {
            var userGroup = userGroupService.Find(id);
            if (userGroup != null)
            {
                userGroupService.Delete(userGroup);
                //                Success(Translate("Messages.RoleDeleted"));
            }

            return RedirectToAction(MVC.Admin.UserGroup.Index());
        }

        /// <summary>
        /// Renders users assignment view.
        /// </summary>
        /// <param name="id">The user group id.</param>
        /// <returns>Users assignment view.</returns>
        [HttpGet]
        public virtual ActionResult Users(long id)
        {
            var userGroup = userGroupService.Find(id);
            if (userGroup == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }

            return View(UserGroupHelper.BuildAssignmentModel(userGroup));
        }

        /// <summary>
        /// Reassign specified users.
        /// </summary>
        /// <param name="id">The user group id.</param>
        /// <param name="model">The model.</param>
        /// <returns>Redirects back to user groups list.</returns>
        [HttpPut]
        public virtual ActionResult UpdateUsers(long id, UserToUserGroupAssignmentModel model)
        {
            var userGroup = userGroupService.Find(id);
            if (userGroup == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }

            if (UserGroupHelper.UpdateUserGroupToUsersAssignment(userGroup, model))
            {
                //                Success(Translate("Messages.RoleUsersUpdated"));
                return RedirectToAction(MVC.Admin.UserGroup.Index());
            }

            //            Error(Translate("Messages.ValidationError"));

            return View("Users", model);
        }

        #endregion

    }
}
