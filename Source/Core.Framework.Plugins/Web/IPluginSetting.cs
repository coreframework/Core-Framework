using System;
using Core.Framework.Plugins.Configs;
using Core.Framework.Plugins.Handlers;
using Core.Framework.Plugins.Plugins;

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
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        String Version { get; set; }

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
        /// Gets or sets the js path.
        /// </summary>
        /// <value>The js path.</value>
        String JsPath { get; set; }

        /// <summary>
        /// Gets or sets the js pack.
        /// </summary>
        /// <value>The js pack.</value>
        String JsPack { get; set; }

        /// <summary>
        /// Gets or sets the widget settings.
        /// </summary>
        /// <value>
        /// The widget settings.
        /// </value>
        WidgetSetting[] WidgetSettings { get; set; }

        /// <summary>
        /// Gets or sets the HTTP handlers.
        /// </summary>
        /// <value>The HTTP handlers.</value>
        PluginHttpHandler[] HttpHandlers { get; set; }

        PluginDependency[] PluginDependencies { get; set; }
    }
}