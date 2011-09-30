using System;
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
        public String Key { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [XmlElement("Title")]
        public String Title { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [XmlElement("Identifier")]
        public String Identifier { get; set; }

        #endregion
    }
}