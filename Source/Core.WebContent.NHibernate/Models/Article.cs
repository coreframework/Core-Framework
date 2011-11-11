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
    public class Article : LocalizableEntity<ArticleLocale>, IPermissible
    {
        #region Fields

        private String permissionTitle = "Web Content: Articles";

        private IEnumerable<IPermissionOperation> operations = OperationsHelper.GetOperations<ArticleOperations>();

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
            return new ArticleLocale
                       {
                           Article = this,
                           Culture = null
                       };
        }
    }
}
