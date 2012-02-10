using System;
using System.Linq;
using Core.Framework.NHibernate.Models;

namespace Core.Web.Areas.Admin.Helpers
{
    public static class AccountsHelper
    {
        public static String GetUserUserGroups(User user)
        {
            return String.Join("; ", user.UserGroups.Select(userGroup => userGroup.Name));
        }

        public static String GetUserRoles(User user)
        {
            return String.Join("; ", user.Roles.Select(role => role.Name));
        }

        public static String GetUserGroupRoles(UserGroup userGroup)
        {
            return String.Join("; ", userGroup.Roles.Select(role => role.Name));
        }
    }
}