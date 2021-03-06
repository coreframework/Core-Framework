﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Core.Forms.NHibernate.Permissions.Operations;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Framework.Core.Localization;
using Framework.Facilities.NHibernate.Objects;
using Iesi.Collections.Generic;

namespace Core.Forms.NHibernate.Models
{
    /// <summary>
    /// Describes form entity.
    /// </summary>
    [Export(typeof(IPermissible))]
    public class Form : LocalizableEntity<FormLocale>, IPermissible
    {
        #region Fields

        private readonly Iesi.Collections.Generic.ISet<FormElement> formElements = new HashedSet<FormElement>();

        private String permissionTitle = "Forms";

        private IEnumerable<IPermissionOperation> operations = OperationsHelper.GetOperations<FormOperations>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public virtual long? UserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show submit button].
        /// </summary>
        /// <value><c>true</c> if [show submit button]; otherwise, <c>false</c>.</value>
        public virtual bool ShowSubmitButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show reset button].
        /// </summary>
        /// <value><c>true</c> if [show reset button]; otherwise, <c>false</c>.</value>
        public virtual bool ShowResetButton { get; set; }

        /// <summary>
        /// Gets or sets the form elements.
        /// </summary>
        /// <value>The form elements.</value>
        public virtual IEnumerable<FormElement> FormElements
        {
            get { return formElements; }
        }

        public virtual String Title
        {
            get
            {
                return ((FormLocale)CurrentLocale).Title;
            }
            set { ((FormLocale)CurrentLocale).Title = value; }
        }

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
            return new FormLocale
            {
                Form = this,
                Culture = null
            };
        }

        #endregion
    }
}
