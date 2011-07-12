using System;
using System.Globalization;
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
    }
}
