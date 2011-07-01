using System;
using Core.Framework.Plugins.Configs;

namespace Core.Framework.Plugins.Web
{
    /// <summary>
    /// Defines plugin setting
    /// </summary>
    public interface IPluginSetting
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        String Identifier { get; set; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title.</value>
        String Title { get; set; }

        /// <summary>
        /// Gets the resources directory.
        /// </summary>
        /// <value>The resources directory.</value>
        String ResourcesDirectory { get; set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        String Description { get; set; }

        /// <summary>
        /// Gets the Plugin Css and JS config path.
        /// </summary>
        String CssJsConfigPath { get; set; }

        /// <summary>
        /// Gets the Plugin images path.
        /// </summary>
        String ImagesPath { get; set; }

        /// <summary>
        /// Gets the Plugin CSS path.
        /// </summary>
        String CssPath { get; set; }

        /// <summary>
        /// Gets the Plugin CSS pack.
        /// </summary>
        String CssPack { get; set; }

        /// <summary>
        /// Gets or sets the widget settings.
        /// </summary>
        /// <value>
        /// The widget settings.
        /// </value>
        WidgetSetting[] WidgetSettings { get; set; }
    }
}