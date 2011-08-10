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
        /// <returns></returns>
        IDictionary<String, String> GetAvailableLanguages();

        /// <summary>
        /// Gets the default culture.
        /// </summary>
        /// <returns></returns>
        CultureInfo GetDefaultCulture();
    }
}
