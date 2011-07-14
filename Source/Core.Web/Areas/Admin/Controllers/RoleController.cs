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
using Framework.MVC.Controllers;
using Framework.MVC.Extensions;
using Framework.MVC.Grids;
using Framework.MVC.Helpers;
using Microsoft.Practices.ServiceLocation;
using System.Linq.Dynamic;

namespace Core.Web.Areas.Admin.Controllers
{
    [Permissions((int)BaseEntityOperations.Manage, typeof(Role))]
    public partial class RoleController : FrameworkController
    {
        #region Fields

        private readonly IRoleService roleService;

        private readonly IUserService userService;

        private readonly IUserGroupService userGroupService;

        #endregion

        #region Constructorss

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleController"/> class.
        /// </summary>
        public RoleController()
        {
            roleService = ServiceLocator.Current.GetInstance<IRoleService>();

            userService = ServiceLocator.Current.GetInstance<IUserService>();

            userGroupService = ServiceLocator.Current.GetInstance<IUserGroupService>();
        }

        #endregion

        #region Actions

        /// <summary>
        /// Renders roles listing.
        /// </summary>
        /// <returns>Accounts roles view.</returns>
        [HttpGet]
        public virtual ActionResult Index()
        {
            IList<GridColumnViewModel> columns = new List<GridColumnViewModel>
                                                     {
                                                         new GridColumnViewModel {Name = "Role", Index = "Name"},
                                                         new GridColumnViewModel
                                                             {Name = "Users list", Width = 150, Sortable = false},
                                                         new GridColumnViewModel
                                                             {Name = "User groups list", Width = 150, Sortable = false},
                                                         new GridColumnViewModel
                                                             {Name = "Role permissions", Width = 150, Sortable = false},
                                                         new GridColumnViewModel
                                                             {Name = "Remove", Width = 150, Sortable = false},
                                                         new GridColumnViewModel
                                                             {Name = "Id", Sortable = false, Hidden = true}
                                                     };
            var model = new GridViewModel
            {
                DataUrl = Url.Action(MVC.Admin.Role.DynamicGridData()),
                DetailsUrl = String.Format("{0}/", Url.Action(MVC.Admin.Role.Edit())),
                DefaultOrderColumn = "Id",
                GridTitle = "Roles",
                Columns = columns
            };
            return View(model);
        }

        [HttpPost]
        public virtual JsonResult DynamicGridData(int page, int rows, string search, string sidx, string sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            IQueryable<Role> searchQuery = roleService.GetSearchQuery(search);
            int totalRecords = roleService.GetCount(searchQuery);
            var totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);
            var roles = searchQuery.OrderBy(sidx + " " + sord).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                    from role in roles
                    select new
                    {
                        id = !role.IsSystemRole ? role.Id.ToString() : null,
                        cell = new[] { role.Name,
                            !role.NotAssignableRole ? String.Format("<a href=\"{0}\">{1}</a>",
                                Url.Action(MVC.Admin.Role.Users(role.Id)),
                                HttpContext.Translate("Users", ResourceHelper.GetControllerScope(this))) : String.Empty,
                            !role.NotAssignableRole ? String.Format("<a href=\"{0}\">{1}</a>",
                                Url.Action(MVC.Admin.Role.UserGroups(role.Id)),
                                HttpContext.Translate("UserGroups", ResourceHelper.GetControllerScope(this))) : String.Empty,
                            !role.NotPermissible ? String.Format("<a href=\"{0}\">{1}</a>",
                                Url.Action(MVC.Admin.Role.Permissions(role.Id, null)),
                                HttpContext.Translate("Permissions", ResourceHelper.GetControllerScope(this))) : String.Empty,
                            !role.IsSystemRole ? String.Format("<a href=\"{0}\">{1}</a>",
                                Url.Action(MVC.Admin.Role.Remove(role.Id)),
                                HttpContext.Translate("Remove", ResourceHelper.GetControllerScope(this))) : String.Empty
                            }
                    }).ToArray()
            };
            return Json(jsonData);
        }

        /// <summary>
        /// Render edit for role specified.
        /// </summary>
        /// <returns>Role edit view.</returns>
        [HttpGet]
        public virtual ActionResult New()
        {
            return View(new RoleViewModel().MapFrom(new Role()));
        }

        /// <summary>
        /// Updates role details.
        /// </summary>
        /// <param name="roleView">The role view model.</param>
        /// <returns>Redirect back to roles list.</returns>
        [HttpPut]
        public virtual ActionResult Create(RoleViewModel roleView)
        {
            if (ModelState.IsValid)
            {
                var role = new Role { IsSystemRole = false };
                roleView.MapTo(role);
                roleService.Save(role);
                Success(Translate("Messages.RoleCreated"));
                return RedirectToAction(MVC.Admin.Role.Users(role.Id));
            }

            Error(Translate("Messages.ValidationError"));
            return View("New", roleView);
        }

        /// <summary>
        /// Render edit form for role specified.
        /// </summary>
        /// <param name="id">The role id.</param>
        /// <returns>Role edit view.</returns>
        [HttpGet]
        public virtual ActionResult Edit(long id)
        {
            var role = roleService.Find(id);
            if (role == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }

            return View(new RoleLocaleViewModel().MapFrom(role));
        }

        [HttpPost]
        public virtual ActionResult ChangeLanguage(long roleId, String culture)
        {
            var role = roleService.Find(roleId);
            if (role == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Role not found");
            }
            RoleLocaleViewModel model = new RoleLocaleViewModel().MapFrom(role);
            model.SelectedCulture = culture;
            IRoleLocaleService localeService = ServiceLocator.Current.GetInstance<IRoleLocaleService>();
            RoleLocale locale = localeService.GetLocale(roleId, culture);
            if (locale != null)
            {
                model.Name = locale.Name;
            }

            return PartialView("EditForm", model);
        }

        /// <summary>
        /// Updates role details.
        /// </summary>
        /// <param name="id">The role id.</param>
        /// <param name="roleView">The role view model.</param>
        /// <returns>Redirect back to roles list.</returns>
        [HttpPost]
        public virtual ActionResult Update(long id, RoleLocaleViewModel roleView)
        {
            var role = roleService.Find(id);
            if (role == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }

            if (ModelState.IsValid)
            {
                IRoleLocaleService localeService = ServiceLocator.Current.GetInstance<IRoleLocaleService>();
                RoleLocale roleLocale = localeService.GetLocale(id, roleView.SelectedCulture);
                if (roleLocale == null)
                {
                    roleLocale = new RoleLocale { Role = role, Culture = roleView.SelectedCulture };
                }
                roleLocale.Name = roleView.Name;
                localeService.Save(roleLocale);
                Success(Translate("Messages.RoleUpdated"));
                return RedirectToAction(MVC.Admin.Role.Index());
            }

            Error(Translate("Messages.ValidationError"));
            return View("Edit", roleView);
        }

        /// <summary>
        /// Render delete confirmation view.
        /// </summary>
        /// <param name="id">The role id.</param>
        /// <returns>Role remove confirmation view.</returns>
        [HttpGet]
        public virtual ActionResult Remove(long id)
        {
            var role = roleService.Find(id);
            if (role == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }
            return View(new RoleViewModel().MapFrom(role));
        }

        /// <summary>
        /// Confirm role removing.
        /// </summary>
        /// <param name="id">The role id.</param>
        /// <returns>Redirects back to roles list.</returns>
        [HttpDelete]
        public virtual ActionResult ConfirmRemove(long id)
        {
            var role = roleService.Find(id);
            if (role != null)
            {
                roleService.Delete(role);
                Success(Translate("Messages.RoleDeleted"));
            }

            Success(Translate("Messages.UnknownError"));
            return RedirectToAction(MVC.Admin.Role.Index());
        }

        /// <summary>
        /// Renders users assignment view.
        /// </summary>
        /// <param name="id">The role id.</param>
        /// <returns>Users assignment view.</returns>
        [HttpGet]
        public virtual ActionResult Users(long id)
        {
            var role = roleService.Find(id);
            if (role == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
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
                DataUrl = Url.Action(MVC.Admin.Role.UsersDynamicGridData()),
                DefaultOrderColumn = "Username",
                GridTitle = "Users",
                Columns = columns,
                MultiSelect = true,
                IsRowNotClickable = true,
                SelectedIds = role.Users.Select(t => t.Id),
                Title = role.Name
            };


            return View(model);
            //return View(RoleHelper.BuildRoleToUsersAssignmentModel(role));
        }

        [HttpPost]
        public virtual JsonResult UsersDynamicGridData(int id, int page, int rows, string search, string sidx, string sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var role = roleService.Find(id);
            if (role == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
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
                        cell = new[] { usr.Username }
                    }).ToArray()
            };
            return Json(jsonData);
        }

        public virtual JsonResult UpdateUsers(long id, IEnumerable<string> ids, IEnumerable<string> selids)
        {
            var userGroup = roleService.Find(id);
            if (userGroup == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }

            if (RoleHelper.UpdateRoleToUsersAssignment(userGroup, ids, selids))
            {
                Success(Translate("Messages.UserRolesUpdated"));
                return Json(true);
            }

            Error(Translate("Messages.ValidationError"));
            return Json(Translate("Messages.ValidationError"));
        }

        /// <summary>
        /// Renders user groups assignment view.
        /// </summary>
        /// <param name="id">The role id.</param>
        /// <returns>User groups assignment view.</returns>
        [HttpGet]
        public virtual ActionResult UserGroups(long id)
        {
            var role = roleService.Find(id);
            if (role == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
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
                SelectedIds = role.UserGroups.Select(t => t.Id),
                Title = role.Name
            };

            return View(model);

            //return View(RoleHelper.BuildRoleToUserGroupsAssignmentModel(role));
        }

        [HttpPost]
        public virtual JsonResult UserGroupsDynamicGridData(int id, int page, int rows, string search, string sidx, string sord)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var role = roleService.Find(id);
            if (role == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
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

        public virtual JsonResult UpdateUserGroups(long id, IEnumerable<string> ids, IEnumerable<string> selids)
        {
            var role = roleService.Find(id);
            if (role == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }

            if (RoleHelper.UpdateRoleToUserGroupsAssignment(role, ids, selids))
            {
                Success(Translate("Messages.RoleUserGroupsUpdated"));
                return Json(true);
            }

            return Json(Translate("Messages.ValidationError"));
        }

        #region Permissions

        /// <summary>
        /// Permissionses the specified role id.
        /// </summary>
        /// <param name="roleId">The role id.</param>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Permissions(long roleId, String resource)
        {
            var role = roleService.Find(roleId);
            if (role == null || role.NotPermissible)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }

            return View(RoleHelper.BindRolePermissionModel(roleId, resource));
        }

        /// <summary>
        /// Applies the permissions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult ApplyPermissions(PermissionOperationsModel model)
        {
            var role = roleService.Find(model.RoleId);
            if (role == null || role.NotPermissible)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, HttpContext.Translate("Messages.CouldNotFoundEntity", ResourceHelper.GetControllerScope(this)));
            }

            RoleHelper.ApplyRolePermissions(model);
            Success(Translate("Messages.SuccessfullyApplyPermissions"));
            return RedirectToAction(MVC.Admin.Role.Permissions(model.RoleId, String.Format("{0}_{1}", model.ResourceId, (int)model.Area)));
        }

        #endregion

        #endregion
    }
}

