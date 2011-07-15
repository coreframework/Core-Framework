using System;
using System.Collections.Generic;
using System.Globalization;
using Castle.MicroKernel;
using Framework.Core.Configuration;
using Microsoft.Practices.ServiceLocation;

namespace Framework.Core.Localization
{
    /// <summary>
    /// Culture helper.
    /// </summary>
    public static class CultureHelper
    {
        private static CultureInfo defaultCulture = null;

        /// <summary>
        /// Gets the default culture.
        /// </summary>
        /// <value>The default culture.</value>
        public static CultureInfo DefaultCulture
        {
            get
            {
                if (defaultCulture == null)
                {
                    String cultureCode = ServiceLocator.Current.GetInstance<IConfigurationManager>().AppSettings[Constants.DefaultCulture];
                    defaultCulture = CultureInfo.GetCultureInfo(cultureCode);
                }

                return defaultCulture;
            }
        }

        /// <summary>
        /// Gets the default name of the culture.
        /// </summary>
        /// <value>The default name of the culture.</value>
        public static String DefaultCultureName
        {
            get
            {
                return DefaultCulture.Name;
            }
        }

        /// <summary>
        /// Gets the available cultures.
        /// </summary>
        /// <returns></returns>
        public static IDictionary<String, String> GetAvailableCultures()
        {
            try
            {
                ICultureProvider cultureProvider = ServiceLocator.Current.GetInstance<ICultureProvider>();
                if (cultureProvider != null)
                {
                    return cultureProvider.GetAvailableLanguages();
                }
            }
            catch (Exception)
            {
            }
            IDictionary<String, String> cultures = new Dictionary<string, string> { { DefaultCulture.NativeName, DefaultCultureName } };
            return cultures;
        }
    }
}
