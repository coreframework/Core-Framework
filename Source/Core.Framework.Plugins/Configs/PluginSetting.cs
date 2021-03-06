using System;
using System.Xml.Serialization;
using Core.Framework.Plugins.Handlers;
using Core.Framework.Plugins.Plugins;
using Core.Framework.Plugins.Web;

namespace Core.Framework.Plugins.Configs
{
    /// <summary>
    /// Contains plugin settings.
    /// </summary>
    [XmlRoot("PluginSetting")]
    public class PluginSetting : IPluginSetting
    {
        #region Implementation of IPluginSetting

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [XmlElement("Identifier")]
        public String Identifier { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        [XmlElement("Version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title.</value>
        [XmlElement("Title")]
        public String Title { get; set; }

        /// <summary>
        /// Gets the resources directory.
        /// </summary>
        /// <value>The resources directory.</value>
        [XmlElement("ResourcesDirectory")]
        public String ResourcesDirectory { get; set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        [XmlElement("Description")]
        public String Description { get; set; }

        /// <summary>
        /// Gets the Plugin Css and JS config path.
        /// </summary>
        [XmlElement("CssJsConfigPath")]
        public String CssJsConfigPath { get; set; }

        /// <summary>
        /// Gets the Plugin images path.
        /// </summary>
        [XmlElement("ImagesPath")]
        public String ImagesPath { get; set; }

        /// <summary>
        /// Gets the Plugin CSS path.
        /// </summary>
        [XmlElement("CssPath")]
        public String CssPath { get; set; }

        /// <summary>
        /// Gets the Plugin CSS pack.
        /// </summary>
        [XmlElement("CssPack")]
        public String CssPack { get; set; }

        /// <summary>
        /// Gets or sets the js path.
        /// </summary>
        /// <value>The js path.</value>
        [XmlElement("JsPath")]
        public String JsPath { get; set; }

        /// <summary>
        /// Gets or sets the js pack.
        /// </summary>
        /// <value>The js pack.</value>
        [XmlElement("JsPack")]
        public String JsPack { get; set; }

        /// <summary>
        /// Gets or sets the widget settings.
        /// </summary>
        /// <value>
        /// The widget settings.
        /// </value>
        [XmlArray("WidgetSettings"), XmlArrayItem(typeof(WidgetSetting))]
        public WidgetSetting[] WidgetSettings { get; set; }

        /// <summary>
        /// Gets or sets the HTTP handlers.
        /// </summary>
        /// <value>The HTTP handlers.</value>
        [XmlArray("httphandlers", IsNullable = true), XmlArrayItem("handler", typeof(PluginHttpHandler))]
        public PluginHttpHandler[] HttpHandlers { get; set; }

        /// <summary>
        /// Gets or sets the plugin dependencies.
        /// </summary>
        /// <value>The plugin dependencies.</value>
        [XmlArray("PluginDependencies", IsNullable = true), XmlArrayItem("PluginDependency", typeof(PluginDependency))]
        public PluginDependency[] PluginDependencies { get; set; }

        #endregion
    }
}