using System.Collections.Generic;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models
{
    public class SiteSettings: Entity, IPermissible
    {
        #region

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteSettings"/> class.
        /// </summary>
        public SiteSettings()
        {
            PermissionTitle = "Site Settings";
            Operations = OperationsHelper.GetOperations<BaseEntityOperations>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the show main menu.
        /// </summary>
        /// <value>The show main menu.</value>
        public virtual bool ShowMainMenu { get; set; }

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
        public virtual string PermissionTitle { get; set; }

        /// <summary>
        /// Gets or sets the object permission operations.
        /// </summary>
        /// <value>The object permission operations.</value>
        public virtual IEnumerable<IPermissionOperation> Operations { get; set; }

        #endregion
    }
}
