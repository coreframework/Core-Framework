﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.NHibernate.Contracts;
using Core.Framework.NHibernate.Models;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Web.Areas.Admin.Helpers;
using Core.Web.Areas.Admin.Models;
using Core.Web.Helpers;
using Framework.Core.Helpers;
using Framework.Mvc.Controllers;
using Framework.Mvc.Grids;
using Microsoft.Practices.ServiceLocation;
using MvcSiteMapProvider.Filters;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Web.Areas.Admin.Controllers
{
    [Permissions((int)BaseEntityOperations.Manage, typeof(User))]
    public partial class UserController : FrameworkController
    {
        #region Fields

        private readonly IUserService userService;

        private readonly IUserGroupService userGroupService;

        private readonly IRoleLocaleService roleLocaleService;

        #endregion

        #region Constructors

        public UserController()
        {
            userService = ServiceLocator.Current.GetInstance<IUserService>();
            userGroupService = ServiceLocator.Current.GetInstance<IUserGroupService>();
            roleLocaleService = ServiceLocator.Current.GetInstance<IRoleLocaleService>();
        }

        #endregion

        #region Actions

        public virtual ActionResult Index()
        {
            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>
                                                     {
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate(".Model.User.Name"), 
                                                                 Index = "Username"
                                                             },
                                                        new GridColumnViewModel
                                                            {
                                                                Name = Translate(".Model.User.Email"), 
                                                                Index = "Email"
                                                            },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate(".Model.User.Status"), 
                                                                 Index = "Status"
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate(".Model.User.UserGroupsList"),
                                                                 Width = 150,
                                                                 Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate(".Model.User.RolesList"),
                                                                 Width = 150,
                                                                 Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Width = 10,
                                                                 Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Id", 
                                                                 Sortable = false, 
                                                                 Hidden = true
                                                             }
                                                     };
            var model = new GridViewModel
            {
                DataUrl = Url.Action(MVC.Admin.User.DynamicGridData()),
                DetailsUrl = String.Format("{0}/", Url.Action(MVC.Admin.User.Edit())),
                DefaultOrderColumn = "Username",
                GridTitle = "Users",
                Columns = columns,
            };
            return View(model);
        }

        [HttpPost]
        public virtual JsonResult DynamicGridData(int page, int rows, String search, String sidx, String sord)
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
                               cell = new[]
                                                                        {
                                                                            HttpUtility.HtmlEncode(user.Username),
                                                                            HttpUtility.HtmlEncode(user.Email),
                                                                            Translate(EnumHelper.Humanize(user.Status)),
                                                                            String.Format("{0}<a style=\"margin-left:7px\" href=\"{1}\">{2}</a>",
                                                                                              AccountsHelper.GetUserUserGroups(user),
                                                                                Url.Action(MVC.Admin.User.UserGroups(user.Id)),
                                                                                Translate(".Model.User.UserGroups")),
                                                                            String.Format("{0}<a style=\"margin-left:7px\" href=\"{1}\">{2}</a>",
                                                                                              AccountsHelper.GetUserRoles(user),
                                                                                Url.Action(MVC.Admin.User.Roles(user.Id)),
                                                                                Translate(".Model.User.Roles")),
                                                                            String.Format(
                                                                                "<a href=\"{0}\"><em class=\"delete\" style=\"margin-left: 10px;\"/></a>",
                                                                                Url.Action(MVC.Admin.User.Remove(user.Id)))
                                                                        }
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
        [SiteMapTitle("Nickname")]
        public virtual ActionResult Edit(long id)
        {
            var user = userService.Find(id);
            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
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
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
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
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
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
        [SiteMapTitle("Title")]
        public virtual ActionResult UserGroups(long id)
        {
            var user = userService.Find(id);
            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }

            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>
                                                     {
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Name", 
                                                                 Index = "Name",
                                                                 Width = 1100
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Id", 
                                                                 Sortable = false, 
                                                                 Hidden = true
                                                             }
                                                     };
            var model = new GridViewModel
            {
                DataUrl = Url.Action(MVC.Admin.User.UserGroupsDynamicGridData()),
                DefaultOrderColumn = "Name",
                GridTitle = "User Groups",
                Columns = columns,
                MultiSelect = true,
                IsRowNotClickable = true,
                SelectedIds = user.UserGroups.Select(t => t.Id),
                Title = String.Format(Translate("Titles.User_UserGroups"), user.Username)
            };

            return View(model);
        }

        [HttpPost]
        public virtual JsonResult UserGroupsDynamicGridData(int id, int page, int rows, String search, String sidx, String sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var user = userService.Find(id);
            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }
            IQueryable<UserGroup> searchQuery = userGroupService.GetSearchQuery(search);
            int totalRecords = userGroupService.GetCount(searchQuery);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
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
                        cell = new[] { userGroup.Name }
                    }).ToArray()
            };

            return Json(jsonData);
        }

        public virtual JsonResult UpdateUserGroups(long id, IEnumerable<String> ids, IEnumerable<string> selids)
        {
            var user = userService.Find(id);
            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }

            if (UserHelper.UpdateUserGroupToUsersAssignment(user, ids, selids))
            {
                Success(Translate("Messages.UserUserGroupsUpdated"));
                return Json(true);
            }

            return Json(Translate("Messages.ValidationError"));
        }

        [HttpGet]
        [SiteMapTitle("Title")]
        public virtual ActionResult Roles(long id)
        {
            var user = userService.Find(id);
            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }

            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>
                                                     {
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Name", 
                                                                 Index = "Name",
                                                                 Width = 1100
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Id", 
                                                                 Sortable = false, 
                                                                 Hidden = true
                                                             }
                                                     };
            var model = new GridViewModel
            {
                DataUrl = Url.Action(MVC.Admin.User.RolesDynamicGridData()),
                DefaultOrderColumn = "Name",
                GridTitle = "Roles",
                Columns = columns,
                MultiSelect = true,
                IsRowNotClickable = true,
                SelectedIds = user.Roles.Select(t => t.Id),
                Title = String.Format(Translate("Titles.User_Roles"), user.Username)
            };

            return View(model);
        }

        [HttpPost]
        public virtual JsonResult RolesDynamicGridData(int id, int page, int rows, String search, String sidx, String sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var user = userService.Find(id);
            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }
            ICriteria searchCriteria = roleLocaleService.GetSearchCriteriaForAssign(search);
            long totalRecords = roleLocaleService.Count(searchCriteria);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var roles = searchCriteria.SetMaxResults(pageSize).SetFirstResult(pageIndex * pageSize).AddOrder(sord == "asc" ? Order.Asc(sidx) : Order.Desc(sidx)).List<RoleLocale>();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from role in roles
                    select new
                    {
                        id = role.Role.Id,
                        cell = new[] { role.Role.Name }
                    }).ToArray()
            };

            return Json(jsonData);
        }

        public virtual JsonResult UpdateRoles(long id, IEnumerable<String> ids, IEnumerable<string> selids)
        {
            var user = userService.Find(id);
            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }

            if (UserHelper.UpdatRoleToUsersAssignment(user, ids, selids))
            {
                Success(Translate("Messages.UserRolesUpdated"));
                return Json(true);
            }

            return Json(Translate("Messages.ValidationError"));
        }

        #endregion
    }
}
