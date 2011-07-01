using System;

namespace Core.Framework.Plugins.Web
{
    /// <summary>
    /// Defines widget setting
    /// </summary>
    public interface IWidgetSetting
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value> The key. </value>
        String Key { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        String Title { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        String Identifier { get; set; }
    }
}