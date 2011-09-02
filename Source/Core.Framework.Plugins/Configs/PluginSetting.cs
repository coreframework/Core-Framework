using System.Collections.Generic;
using System.Xml.Serialization;
using Core.Framework.Plugins.Handlers;
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
        public string Identifier { get; set; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title.</value>
        [XmlElement("Title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets the resources directory.
        /// </summary>
        /// <value>The resources directory.</value>
        [XmlElement("ResourcesDirectory")]
        public string ResourcesDirectory { get; set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        [XmlElement("Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets the Plugin Css and JS config path.
        /// </summary>
        [XmlElement("CssJsConfigPath")]
        public string CssJsConfigPath { get; set; }

        /// <summary>
        /// Gets the Plugin images path.
        /// </summary>
        [XmlElement("ImagesPath")]
        public string ImagesPath { get; set; }

        /// <summary>
        /// Gets the Plugin CSS path.
        /// </summary>
        [XmlElement("CssPath")]
        public string CssPath { get; set; }

        /// <summary>
        /// Gets the Plugin CSS pack.
        /// </summary>
        [XmlElement("CssPack")]
        public string CssPack { get; set; }

        /// <summary>
        /// Gets or sets the js path.
        /// </summary>
        /// <value>The js path.</value>
        [XmlElement("JsPath")]
        public string JsPath { get; set; }

        /// <summary>
        /// Gets or sets the js pack.
        /// </summary>
        /// <value>The js pack.</value>
        [XmlElement("JsPack")]
        public string JsPack { get; set; }

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

        #endregion
    }
}