using System;

namespace Core.Framework.Plugins.Web
{
    public interface ICoreWidget
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        String Title { get;}
        /// <summary>
        /// Gets or sets the plugin.
        /// </summary>
        /// <value>The plugin.</value>
        ICorePlugin Plugin { get; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        String Identifier { get;}

        /// <summary>
        /// Gets the view action.
        /// </summary>
        /// <value>The view action.</value>
        IWidgetActionVerb ViewAction { get;}

        /// <summary>
        /// Gets the edit action.
        /// </summary>
        /// <value>The edit action.</value>
        IWidgetActionVerb EditAction { get;}

        /// <summary>
        /// Gets the save settings action.
        /// </summary>
        /// <value>The save settings action.</value>
        IWidgetActionVerb SaveAction { get; }

        /// <summary>
        /// Gets the widget setting.
        /// </summary>
        IWidgetSetting WidgetSetting { get; }

        /// <summary>
        /// Removes the specified core widget instance.
        /// </summary>
        /// <param name="coreWidgetInstance">The core widget instance.</param>
        void Remove(ICoreWidgetInstance coreWidgetInstance);
    }
}
