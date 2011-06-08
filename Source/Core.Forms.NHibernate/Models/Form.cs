using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Core.Forms.NHibernate.Permissions.Operations;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using FluentNHibernate.Data;

namespace Core.Forms.NHibernate.Models
{
    /// <summary>
    /// Describes form entity.
    /// </summary>
    [Export(typeof(IPermissible))]
    public class Form: Entity, IPermissible
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Form"/> class.
        /// </summary>
        public Form()
        {
            PermissionTitle = "Forms";
            Operations = OperationsHelper.GetOperations<FormOperations>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public virtual long? UserId { get; set; }

        /// <summary>
        /// Gets or sets the form elements.
        /// </summary>
        /// <value>The form elements.</value>
        public virtual IEnumerable<FormElement> FormElements { get; set; }

        #endregion

        #region IPermissible Members

        /// <summary>
        /// Gets or sets the permission title.
        /// </summary>
        /// <value>The permission title.</value>
        public virtual string PermissionTitle { get; set; }

        /// <summary>
        /// Gets or sets the permission groups.
        /// </summary>
        /// <value>The permission groups.</value>
        public virtual IEnumerable<IPermissionOperation> Operations { get; set; }

        #endregion
    }
}
