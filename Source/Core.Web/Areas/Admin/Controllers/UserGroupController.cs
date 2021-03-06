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
using Framework.Mvc.Controllers;
using Framework.Mvc.Grids;
using Framework.Mvc.Grids.JqGrid;
using Microsoft.Practices.ServiceLocation;
using MvcSiteMapProvider.Filters;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Web.Areas.Admin.Controllers
{
    [Permissions((int)BaseEntityOperations.Manage,typeof(UserGroup))]
    public partial class UserGroupController : FrameworkController
    {
        #region Fields

        private readonly IUserGroupService userGroupService;

        private readonly IUserService userService;

        private readonly IRoleLocaleService roleLocaleService;

        #endregion

        #region Constructors

        public UserGroupController()
        {
            userGroupService = ServiceLocator.Current.GetInstance<IUserGroupService>();
            userService = ServiceLocator.Current.GetInstance<IUserService>();
            roleLocaleService = ServiceLocator.Current.GetInstance<IRoleLocaleService>();
        }

        #endregion

        #region Actions

        [HttpGet]
        public virtual ActionResult Index()
        {
            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>
                                                     {
                                                         new GridColumnViewModel {Name = Translate(".Model.UserGroup.Name"), Index = "Name"},
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate(".Model.UserGroup.RolesList"),
                                                                 Width = 150,
                                                                 Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = Translate(".Model.UserGroup.UsersList"),
                                                                 Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {
                                                                 Width = 10,
                                                                 Sortable = false
                                                             },
                                                         new GridColumnViewModel
                                                             {Name = "Id", Sortable = false, Hidden = true}
                                                     };
            var model = new GridViewModel
            {
                DataUrl = Url.Action(MVC.Admin.UserGroup.DynamicGridData()),
                DetailsUrl = String.Format("{0}/", Url.Action(MVC.Admin.UserGroup.Edit())),
                DefaultOrderColumn = "Name",
                GridTitle = ".UserGroups",
                Columns = columns
            };
            return View(model);
        }

        [HttpPost]
        public virtual JsonResult DynamicGridData(int page, int rows, String search, String sidx, String sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
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
                        cell = new[] { HttpUtility.HtmlEncode(userGroup.Name),
                            String.Format("{0}<a style=\"margin-left:7px\" href=\"{1}\">{2}</a>",
                                                                                              AccountsHelper.GetUserGroupRoles(userGroup),
                                                                                Url.Action(MVC.Admin.UserGroup.Roles(userGroup.Id)),
                                                                                Translate(".Model.UserGroup.Roles")),
                            String.Format(JqGridConstants.UrlTemplate,
                                Url.Action(MVC.Admin.UserGroup.Users(userGroup.Id)),
                                Translate(".Model.UserGroup.Users")), String.Format("<a href=\"{0}\"><em class=\"delete\" style=\"margin-left: 10px;\"/></a>",
                                            Url.Action(MVC.Admin.UserGroup.Remove(userGroup.Id)))
                          }
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
                Success(Translate("Messages.RoleCreated"));
                return RedirectToAction(MVC.Admin.UserGroup.Users(userGroup.Id));
            }

            Error(Translate("Messages.ValidationError"));
            return View("New", userGroupView);
        }

        /// <summary>
        /// Render edit form for user group specified.
        /// </summary>
        /// <param name="id">The user group id.</param>
        /// <returns>User group edit view.</returns>
        [HttpGet]
        [SiteMapTitle("Name")]
        public virtual ActionResult Edit(long id)
        {
            var userGroup = userGroupService.Find(id);
            if (userGroup == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
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
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }

            if (ModelState.IsValid)
            {
                userGroupView.MapTo(userGroup);
                userGroupService.Save(userGroup);
                Success(Translate("Messages.RoleUpdated"));
                return RedirectToAction(MVC.Admin.UserGroup.Index());
            }

            Error(Translate("Messages.ValidationError"));
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
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
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
                Success(Translate("Messages.RoleDeleted"));
            }

            return RedirectToAction(MVC.Admin.UserGroup.Index());
        }

        /// <summary>
        /// Renders users assignment view.
        /// </summary>
        /// <param name="id">The user group id.</param>
        /// <returns>Users assignment view.</returns>
        [HttpGet]
        [SiteMapTitle("Title")]
        public virtual ActionResult Users(long id)
        {
            var userGroup = userGroupService.Find(id);
            if (userGroup == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }
            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>
                                                     {
                                                         new GridColumnViewModel
                                                             {
                                                                 Name = "Name", 
                                                                 Index = "Username",
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
                DataUrl = Url.Action(MVC.Admin.UserGroup.UsersDynamicGridData()),
                DefaultOrderColumn = "Username",
                GridTitle = "Users",
                Columns = columns,
                MultiSelect = true,
                IsRowNotClickable = true,
                SelectedIds = userGroup.Users.Select(t => t.Id),
                Title = String.Format(Translate("Titles.UserGroup_Users"), userGroup.Name)
            };

            return View(model);//View(UserGroupHelper.BuildAssignmentModel(userGroup));
        }

        [HttpPost]
        public virtual JsonResult UsersDynamicGridData(int id, int page, int rows, String search, String sidx, String sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var userGroup = userGroupService.Find(id);
            if (userGroup == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }
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
                    from usr in users
                    select new
                    {
                        id = usr.Id,
                        cell = new[] { HttpUtility.HtmlEncode(usr.Username) }
                    }).ToArray()
            };
            return Json(jsonData);
        }

        public virtual JsonResult UpdateUsers(long id, IEnumerable<String> ids, IEnumerable<string> selids)
        {
            var userGroup = userGroupService.Find(id);
            if (userGroup == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }

            if (UserGroupHelper.UpdateUserGroupToUsersAssignment(userGroup, ids, selids))
            {
                Success(Translate("Messages.UserRolesUpdated"));
                return Json(true);
            }

            return Json(Translate("Messages.ValidationError"));
        }

        [HttpGet]
        [SiteMapTitle("Title")]
        public virtual ActionResult Roles(long id)
        {
            var userGroup = userGroupService.Find(id);
            if (userGroup == null)
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
                DataUrl = Url.Action(MVC.Admin.UserGroup.RolesDynamicGridData()),
                DefaultOrderColumn = "Name",
                GridTitle = "Roles",
                Columns = columns,
                MultiSelect = true,
                IsRowNotClickable = true,
                SelectedIds = userGroup.Roles.Select(t => t.Id),
                Title = String.Format(Translate("Titles.UserGroup_Roles"), userGroup.Name)
            };

            return View(model);
        }

        [HttpPost]
        public virtual JsonResult RolesDynamicGridData(int id, int page, int rows, String search, String sidx, String sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var userGroup = userGroupService.Find(id);
            if (userGroup == null)
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
            var userGroup = userGroupService.Find(id);
            if (userGroup == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.CouldNotFoundEntity"));
            }

            if (UserGroupHelper.UpdateUserGroupToRolesAssignment(userGroup, ids, selids))
            {
                Success(Translate("Messages.UserGroupRolesUpdated"));
                return Json(true);
            }

            return Json(Translate("Messages.ValidationError"));
        }

        #endregion

    }
}
