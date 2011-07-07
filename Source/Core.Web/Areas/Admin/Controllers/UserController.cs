using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Web.Areas.Admin.Models;
using Core.Web.Helpers;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Core.Helpers;
using Framework.MVC.Controllers;
using Framework.MVC.Extensions;
using Framework.MVC.Grids;
using Framework.MVC.Helpers;
using Microsoft.Practices.ServiceLocation;
using System.Linq.Dynamic;

namespace Core.Web.Areas.Admin.Controllers
{
    [Permissions((int)BaseEntityOperations.Manage, typeof(User))]
    public partial class UserController : FrameworkController
    {
        #region Fields

        private readonly IUserService userService;

        #endregion

        #region Constructors

        public UserController()
        {
            userService = ServiceLocator.Current.GetInstance<IUserService>();
        }
        

        #endregion

        #region Actions

        public virtual ActionResult Index()
        {
            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>
                                                     {
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "User", Index = "Username"
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Status", Index = "Status"
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "User groups",
                                                                 Width = 150,
                                                                 Align = "center",
                                                                 Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Width = 10,
                                                                 Align = "center",
                                                                 Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Id", Sortable = false, Hidden = true
                                                             }
                                                     };
            var model = new GridViewModel
            {
                DataUrl = Url.Action(MVC.Admin.User.DynamicGridData()),
                DetailsUrl = String.Format("{0}/", Url.Action(MVC.Admin.User.Edit())),
                DefaultOrderColumn = "Username",
                GridTitle = "Users",
                Columns = columns
            };
            return View(model);
        }

        [HttpPost]
        public virtual JsonResult DynamicGridData(int page, int rows, string search, string sidx, string sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            IQueryable<User> searchQuery = userService.GetSearchQuery(search);
            int totalRecords = userService.GetCount(searchQuery);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var users = searchQuery.OrderBy(sidx + " " + sord).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from user in users
                    select new
                    {
                        id = user.Id,
                        cell = new[] { user.Username, HttpContext.Translate(EnumHelper.Humanize(user.Status), ResourceHelper.GetControllerScope(this)),
                            String.Format("<a href=\"{0}\">{1}</a>",
                                Url.Action(MVC.Admin.User.UserGroups(user.Id)),
                                HttpContext.Translate("UserGroups", ResourceHelper.GetControllerScope(this))),
                            String.Format("<a href=\"{0}\" style=\"margin-left: 5px;\"><em class=\"delete\"/></a>",
                                Url.Action(MVC.Admin.User.Remove(user.Id))/*,
                                HttpContext.Translate("Remove", ResourceHelper.GetControllerScope(this))*/)}
                    }).ToArray()
            };
            return Json(jsonData);
        }

        /// <summary>
        /// Render edit for user specified.
        /// </summary>
        /// <returns>User edit view.</returns>
        [HttpGet]
        public virtual ActionResult New()
        {
            return View(new UserViewModel().MapFrom(new User()));
        }

        /// <summary>
        /// Updates role details.
        /// </summary>
        /// <param name="model">The role view model.</param>
        /// <returns>Redirect back to roles list.</returns>
        [HttpPut]
        public virtual ActionResult Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserHelper.Save(model);
                Success(Translate("Messages.UserCreated"));
                return RedirectToAction(MVC.Admin.User.Index());
            }

            Error(Translate("Messages.ValidationError"));
            return View("New", model);
        }

        /// <summary>
        /// Render edit form for user specified.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>User edit view.</returns>
        [HttpGet]
        public virtual ActionResult Edit(long id)
        {
            var user = userService.Find(id);
            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }

            return View(new UserViewModel().MapFrom(user));
        }

        /// <summary>
        /// Updates user details.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <param name="model">The user view model.</param>
        /// <returns>Redirect back to users list.</returns>
        [HttpPost]
        public virtual ActionResult Update(long id, UserViewModel model)
        {
            var user = userService.Find(id);
            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }

            if (ModelState.IsValid)
            {
                UserHelper.Update(user, model);
                Success(Translate("Messages.UserUpdated"));
                return RedirectToAction(MVC.Admin.User.Index());
            }

            Error(Translate("Messages.ValidationError"));
            return View("Edit", model);
        }

        /// <summary>
        /// Render delete confirmation view.
        /// </summary>
        /// <param name="id">The role id.</param>
        /// <returns>Role remove confirmation view.</returns>
        [HttpGet]
        public virtual ActionResult Remove(long id)
        {
            var user = userService.Find(id);
            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }
            return View(new UserViewModel().MapFrom(user));
        }

        /// <summary>
        /// Confirm role removing.
        /// </summary>
        /// <param name="id">The role id.</param>
        /// <returns>Redirects back to roles list.</returns>
        [HttpDelete]
        public virtual ActionResult ConfirmRemove(long id)
        {
            var user = userService.Find(id);
            if (user != null)
            {
                userService.Delete(user);
                Success(Translate("Messages.UserDeleted"));
            }

            return RedirectToAction(MVC.Admin.User.Index());
        }

        /// <summary>
        /// Users groups.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult UserGroups(long id)
        {
            var user = userService.Find(id);
            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }

            return View(UserHelper.BuildAssignmentModel(user));
        }

        /// <summary>
        /// Updates the user groups.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPut]
        public virtual ActionResult UpdateUserGroups(long id, UserGroupToUserAssignmentModel model)
        {
            var user = userService.Find(id);
            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }

            if (UserHelper.UpdateUserGroupToUsersAssignment(user, model))
            {
                Success(Translate("Messages.UserRolesUpdated"));
                return RedirectToAction(MVC.Admin.User.Index());
            }

            Error(Translate("Messages.ValidationError"));

            return View("UserGroups", model);
        }

        #endregion
    }
}
