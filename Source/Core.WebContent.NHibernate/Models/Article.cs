using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.WebContent.NHibernate.Permissions;
using Core.WebContent.NHibernate.Static;
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
        /// Gets or sets the last modified date.
        /// </summary>
        /// <value>The last modified date.</value>
        public virtual DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the section.
        /// </summary>
        /// <value>The section.</value>
        public virtual WebContentCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public virtual ArticleStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>The author.</value>
        public virtual String Author { get; set; }

        /// <summary>
        /// Gets or sets the type of the URL.
        /// </summary>
        /// <value>The type of the URL.</value>
        public virtual ArticleUrlType UrlType { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public virtual String Url { get; set; }

        /// <summary>
        /// Gets or sets the start publishing date.
        /// </summary>
        /// <value>The start publishing date.</value>
        public virtual DateTime? StartPublishingDate { get; set; }

        /// <summary>
        /// Gets or sets the finish publishing date.
        /// </summary>
        /// <value>The finish publishing date.</value>
        public virtual DateTime? FinishPublishingDate { get; set; }

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
