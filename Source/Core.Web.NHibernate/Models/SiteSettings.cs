using System;
using System.Collections.Generic;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models
{
    public class SiteSettings: Entity, IPermissible
    {
        #region Fields

        private String permissionTitle = "Site Settings";

        private IEnumerable<IPermissionOperation> operations = OperationsHelper.GetOperations<BaseEntityOperations>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the show panel.
        /// </summary>
        /// <value>The show panel.</value>
        public virtual bool ShowPanel { get; set; }
        
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
