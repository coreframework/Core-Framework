using System;
using System.Collections.Generic;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Configs;
using Core.Framework.Plugins.Web;
using System.Linq;

namespace Core.Framework.Plugins.Widgets
{
    public abstract class BaseWidget : ICoreWidget, IPermissible
    {
        #region Fields

        private IWidgetSetting widgetSetting;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual string Title 
        {
            get { return WidgetSetting.Title; }
        }

        /// <summary>
        /// Gets or sets the plugin.
        /// </summary>
        /// <value>The plugin.</value>
        public abstract ICorePlugin Plugin { get;}

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public virtual string Identifier
        {
            get { return WidgetSetting.Identifier; }
        }

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

        /// <summary>
        /// Gets the widget setting.
        /// </summary>
        public IWidgetSetting WidgetSetting
        {
            get
            {
                return widgetSetting ?? (widgetSetting = Plugin.PluginSetting.WidgetSettings.Where(t => t.Key == GetType().FullName).SingleOrDefault() ?? new WidgetSetting());
            }
        }

        #endregion

        #region Constructor

        protected BaseWidget()
        {
            PermissionTitle = Title;
            Operations = OperationsHelper.GetOperations<BaseWidgetOperations>();
        }

        #endregion

        #region Methods

        public virtual void Remove(ICoreWidgetInstance coreWidgetInstance)
        {
            
        }

        public virtual long? Clone(ICoreWidgetInstance coreWidgetInstance)
        {
            return null;
        }

        #endregion

        #region IPermissible Members

        public string PermissionTitle { get; set; }

        public IEnumerable<IPermissionOperation> Operations { get; set; }

        #endregion
    }
}
