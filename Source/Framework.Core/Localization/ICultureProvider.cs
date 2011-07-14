using System;
using System.Collections.Generic;

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
    }
}
