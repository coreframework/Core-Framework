<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Core.Web.Areas.Admin.Controllers" %>
<%@ Import Namespace="Core.Web.Areas.Admin.Helpers" %>
<%@ Import Namespace="Core.Web" %>
<%@ Import Namespace="Core.Framework.MEF.Web" %>
<%@ Import Namespace="Microsoft.Practices.ServiceLocation" %>
<%@ Import Namespace="Core.Framework.Permissions.Models" %>
<%@ Import Namespace="Core.Web.Helpers" %>
<%@ Import Namespace="Core.Framework.Permissions.Contracts" %>
<%@ Import Namespace="Core.Web.NHibernate.Models" %>
<%@ Import Namespace="Core.Web.Controllers" %>
<%@ Import Namespace="Core.Framework.Permissions.Extensions" %>
<% 
   Dictionary<string, IEnumerable<IMenuItem>> menuItems = new Dictionary<string, IEnumerable<IMenuItem>>();
   menuItems.Add("Home", new IMenuItem[] {new ActionLink<AdminHomeController>("Home", c => c.Index()),});

   var permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
   var user = Context.CorePrincipal();
   bool isUsersAllowed = permissionService.IsAllowed((int) BaseEntityOperations.Manage, user, typeof (User), null);
   bool isUserGroupsAllowed = permissionService.IsAllowed((int) BaseEntityOperations.Manage, user, typeof (UserGroup), null);
   bool isRolesAllowed = permissionService.IsAllowed((int) BaseEntityOperations.Manage, user, typeof (Role), null);
   if (isUsersAllowed || isUserGroupsAllowed || isRolesAllowed)
   {
       var usersMenuItem = new List<IMenuItem>();
       if (isUsersAllowed)
       {
           usersMenuItem.Add(new [] { new ActionLink<UserController>("Users", c => c.Index())});
       }
       if (isUserGroupsAllowed)
       {
           usersMenuItem.Add(new[] { new ActionLink<UserGroupController>("UserGroups", c => c.Index()) });
       }
       if (isUsersAllowed)
       {
           usersMenuItem.Add(new[] { new ActionLink<RoleController>("Roles", c => c.Index()) });
       }
       menuItems.Add("Users", usersMenuItem);
   }
   if (permissionService.IsAllowed((int)BaseEntityOperations.Manage, user, typeof(Plugin), null))
   {
       menuItems.Add("Modules", new IMenuItem[] { new ActionLink<ModuleController>("Modules", c => c.Index()), new ActionLink<WidgetController>("Widgets", c => c.Index()), });
   }
     
   IPluginHelper pluginHelper = ServiceLocator.Current.GetInstance<IPluginHelper>();
   foreach (var verb in MvcApplication.GetVerbsForCategory("AdminModules"))
   {
       if (pluginHelper.IsPluginEnabled(verb.ControllerPluginIdentifier) && verb.IsAllowed(Context.CorePrincipal()))
       {
           menuItems.Add(verb.Name, new IMenuItem[] { new RouteLink(verb.Name, verb.RouteName) });
       }
   }
    
%>
<%=Html.RenderMenu(Url, menuItems) %>