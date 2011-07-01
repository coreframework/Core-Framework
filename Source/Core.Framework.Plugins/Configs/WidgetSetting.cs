using System.Xml.Serialization;
using Core.Framework.Plugins.Web;

namespace Core.Framework.Plugins.Configs
{
    [XmlRoot("WidgetSetting")]
    public class WidgetSetting : IWidgetSetting
    {
        #region Implementation of IWidgetSetting

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value> The key. </value>
        [XmlElement("Key")]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [XmlElement("Title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [XmlElement("Identifier")]
        public string Identifier { get; set; }

        #endregion
    }
}