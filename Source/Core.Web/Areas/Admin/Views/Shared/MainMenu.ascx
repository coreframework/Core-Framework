<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Core.Web.Areas.Admin.Controllers" %>
<%@ Import Namespace="Core.Web" %>
<%@ Import Namespace="Core.Framework.MEF.Web" %>
<%@ Import Namespace="Microsoft.Practices.ServiceLocation" %>
<%@ Import Namespace="Core.Framework.Permissions.Models" %>
<%@ Import Namespace="Core.Framework.Permissions.Contracts" %>
<%@ Import Namespace="Core.Web.NHibernate.Models" %>
<%@ Import Namespace="Core.Framework.Permissions.Extensions" %>
<% 
   Dictionary<string, IEnumerable<IMenuItem>> menuItems = new Dictionary<string, IEnumerable<IMenuItem>>();
   menuItems.Add("Home", new IMenuItem[] { new ActionLink<AdminHomeController>("Home", "~/Content/images/admin/ico4.png", c => c.Index()), });

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
           usersMenuItem.Add(new[] { new ActionLink<UserController>("Users", "~/Content/images/admin/ico1.png", c => c.Index()) });
       }
       if (isUserGroupsAllowed)
       {
           usersMenuItem.Add(new[] { new ActionLink<UserGroupController>("UserGroups", "~/Content/images/admin/ico2.png", c => c.Index()) });
       }
       if (isUsersAllowed)
       {
           usersMenuItem.Add(new[] { new ActionLink<RoleController>("Roles", "~/Content/images/admin/ico3.png", c => c.Index()) });
       }
       menuItems.Add("Users", usersMenuItem);
   }
   if (permissionService.IsAllowed((int)BaseEntityOperations.Manage, user, typeof(Plugin), null))
   {
       menuItems.Add("Modules", new IMenuItem[] { new ActionLink<ModuleController>("Modules", "~/Content/images/admin/ico5.png", c => c.Index()), new ActionLink<WidgetController>("Widgets", "~/Content/images/admin/ico6.png", c => c.Index()), });
   }
     
   IPluginHelper pluginHelper = ServiceLocator.Current.GetInstance<IPluginHelper>();
   var usersMenuItem1 = new List<IMenuItem>();
   foreach (var verb in MvcApplication.GetVerbsForCategory("AdminModules"))
   {
       if (pluginHelper.IsPluginEnabled(verb.ControllerPluginIdentifier) && verb.IsAllowed(Context.CorePrincipal()))
       {
           usersMenuItem1.Add(new[] { new RouteLink(verb.Name, "~/Content/images/admin/ico4.png", verb.RouteName) });
       }
   }
   menuItems.Add("Plugins", usersMenuItem1);
    
%>
<%=Html.RenderMenu(Url, menuItems) %>

<script type="text/javascript">
    $(function () {
        /** enable left sidebar accordion */
        $("#accordion").accordion({ active: parseInt($('#active').attr('number')) });    
    })
</script>