using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.Web.NHibernate.Models
{
    [Export(typeof (IPermissible))]
    public class Role : Entity, IRole, IPermissible, ILocalizable
    {
        #region Fields

        private IList<User> users = new List<User>();
        private IList<UserGroup> userGroups = new List<UserGroup>();

        private readonly IList<RoleLocale> currentRoleLocales = new List<RoleLocale>();
        private IList<ILocale> currentLocales = new List<ILocale>();
        private RoleLocale currentLocale;

        private String permissionTitle = "Roles";

        private IEnumerable<IPermissionOperation> operations = OperationsHelper.GetOperations<BaseEntityOperations>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual String Name
        {
            get { return ((RoleLocale) CurrentLocale).Name; }
            set { ((RoleLocale) CurrentLocale).Name = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is system role.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is system role; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsSystemRole { get; set; }

        public virtual bool NotAssignableRole { get; set; }

        public virtual bool NotPermissible { get; set; }

        public virtual IList<User> Users
        {
            get { return users; }
            set { users = value; }
        }

        public virtual IList<UserGroup> UserGroups
        {
            get { return userGroups; }
            set { userGroups = value; }
        }

        public virtual IList<ILocale> CurrentLocales
        {
            get
            {
                if (currentLocales.Count == 0 && currentRoleLocales.Count > 0)
                {
                    currentLocales = currentRoleLocales.ToList().ConvertAll(mc => (ILocale) mc);
                }
                return currentLocales;
            }
            set { currentLocales = value; }
        }

        public virtual IList<RoleLocale> CurrentRoleLocales
        {
            get { return CurrentLocales.ToList().ConvertAll(mc => (RoleLocale) mc); }
            set { CurrentLocales = value.ToList().ConvertAll(mc => (ILocale) mc); }
        }

        public virtual ILocale CurrentLocale
        {
            get
            {
                if (currentLocale == null)
                {
                    currentLocale = CultureHelper.GetCurrentLocale(CurrentLocales) as RoleLocale;
                    if (currentLocale == null)
                    {
                        currentLocale = new RoleLocale
                                             {
                                                 Role = this,
                                                 Culture = null
                                             };
                    }
                }
                return currentLocale;
            }
        }

        #endregion

        #region IPermissible Members

        /// <summary>
        /// Gets or sets the permission title.
        /// </summary>
        /// <value>The permission title.</value>
        public virtual String PermissionTitle
        {
            get { return permissionTitle; }
            set { permissionTitle = value; }
        }

        /// <summary>
        /// Gets or sets the permission operations.
        /// </summary>
        /// <value>The permission operations.</value>
        public virtual IEnumerable<IPermissionOperation> Operations
        {
            get { return operations; }
            set { operations = value; }
        }

        #endregion
    }
}