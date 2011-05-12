﻿using System;
using System.Collections.Generic;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;

namespace Core.Framework.Plugins.Widgets
{
    public abstract class BaseWidget : ICoreWidget, IPermissible
    {
        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public abstract string Title { get;}

        /// <summary>
        /// Gets or sets the plugin.
        /// </summary>
        /// <value>The plugin.</value>
        public abstract ICorePlugin Plugin { get;}

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public abstract string Identifier { get; }

        /// <summary>
        /// Gets the view action.
        /// </summary>
        /// <value>The view action.</value>
        public abstract IWidgetActionVerb ViewAction { get;}

        /// <summary>
        /// Gets the edit action.
        /// </summary>
        /// <value>The edit action.</value>
        public abstract IWidgetActionVerb EditAction { get; }

        /// <summary>
        /// Gets the save settings action.
        /// </summary>
        /// <value>The save settings action.</value>
        public abstract IWidgetActionVerb SaveAction { get; }

        /// <summary>
        /// Gets the view operation code.
        /// </summary>
        /// <value>The view operation code.</value>
        public virtual Int32 ViewOperationCode { get { return (Int32)BaseWidgetOperations.View; } }

        /// <summary>
        /// Gets the manage operation code.
        /// </summary>
        /// <value>The manage operation code.</value>
        public virtual Int32 ManageOperationCode { get { return (Int32)BaseWidgetOperations.Manage; } }

        /// <summary>
        /// Gets the add to page operation code.
        /// </summary>
        /// <value>The add to page operation code.</value>
        public virtual Int32 AddToPageOperationCode { get { return (Int32)BaseWidgetOperations.AddToPage; } }

        /// <summary>
        /// Gets the add to page operation code.
        /// </summary>
        /// <value>The add to page operation code.</value>
        public virtual Int32 PermissionOperationCode { get { return (Int32)BaseWidgetOperations.Permissions; } }

        #endregion

        #region Constructor

        protected BaseWidget()
        {
            PermissionTitle = Title;
            Operations = OperationsHelper.GetOperations<BaseWidgetOperations>();

        }

        #endregion

        #region IPermissible Members

        public string PermissionTitle { get; set; }

        public IEnumerable<IPermissionOperation> Operations { get; set; }

        #endregion
    }
}
