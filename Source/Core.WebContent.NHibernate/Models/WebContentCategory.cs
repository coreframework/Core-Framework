using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.WebContent.NHibernate.Permissions;
using Framework.Core.Localization;
using Framework.Facilities.NHibernate.Objects;

namespace Core.WebContent.NHibernate.Models
{
    /// <summary>
    /// Web content category class.
    /// </summary>
    [Export(typeof(IPermissible))]
    public class WebContentCategory : LocalizableEntity<WebContentCategoryLocale>, IPermissible
    {
        #region Fields

        private String permissionTitle = "Web Content: Categories";

        private IEnumerable<IPermissionOperation> operations = OperationsHelper.GetOperations<CategoryOperations>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public virtual long? UserId { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>The create date.</value>
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public virtual CategoryStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the section.
        /// </summary>
        /// <value>The section.</value>
        public virtual Section Section { get; set; }

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

        public override ILocale InitializeLocaleEntity()
        {
            return new WebContentCategoryLocale
                       {
                           Category = this,
                           Culture = null
                       };
        }
    }
}
