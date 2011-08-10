﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Framework.Core.Configuration;
using Microsoft.Practices.ServiceLocation;
using System.Linq;

namespace Framework.Core.Localization
{
    /// <summary>
    /// Culture helper.
    /// </summary>
    public static class CultureHelper
    {
        ///<summary>
        /// Neutral culture name.
        ///</summary>
        public const String NeutralCultureName = "Neutral";

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
                    ICultureProvider cultureProvider = ServiceLocator.Current.GetInstance<ICultureProvider>();
                    if (cultureProvider != null)
                    {
                        defaultCulture = cultureProvider.GetDefaultCulture();
                    }
                    if (defaultCulture == null)
                    {
                        String cultureCode =
                            ServiceLocator.Current.GetInstance<IConfigurationManager>().AppSettings[Constants.DefaultCulture];
                        defaultCulture = CultureInfo.GetCultureInfo(cultureCode);
                    }
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
            IDictionary<String, String> cultures = new Dictionary<string, string> { { NeutralCultureName, null } };
            return cultures;
        }

        /// <summary>
        /// Gets the current locale.
        /// </summary>
        /// <param name="locales">The locales.</param>
        /// <returns></returns>
        public static ILocale GetCurrentLocale(IList<ILocale> locales)
        {
            //3 - max locales number: current locale, default locale and neutral locale
            ILocale locale = null;
            if (locales != null && locales.Count > 0 && locales.Count <= 3)
            {
                if (locales.Count == 1)
                {
                    locale = locales[0];
                }
                else if (locales.Count == 2)
                {
                    locale = locales.Where(l => l.Culture != null).First();
                }
                else
                {
                    locale = locales.Where(l => l.Culture != null && !l.Culture.Equals(DefaultCultureName)).First();
                }
            }
            return locale;
        }

        /// <summary>
        /// Sets the default culture.
        /// </summary>
        /// <param name="cultureCode">The culture code.</param>
        public static void SetDefaultCulture(String cultureCode)
        {
            defaultCulture = CultureInfo.GetCultureInfo(cultureCode);
        }
    }
}
