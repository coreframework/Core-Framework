using System;
using System.Collections.Generic;
using System.Globalization;

namespace Framework.Core.Localization
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICultureProvider
    {

        /// <summary>
        /// Gets the available languages.
        /// </summary>
        /// <value>The available languages.</value>
        IDictionary<String, String> AvailableLanguages { get;}

        /// <summary>
        /// Gets the default culture.
        /// </summary>
        /// <value>The default culture.</value>
        CultureInfo DefaultCulture { get; }
    }
}
