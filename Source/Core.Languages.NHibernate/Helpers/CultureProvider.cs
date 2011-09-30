using System;
using System.Collections.Generic;
using System.Globalization;
using Core.Languages.NHibernate.Contracts;
using Core.Languages.NHibernate.Models;
using Framework.Core.Localization;
using Microsoft.Practices.ServiceLocation;

namespace Core.Languages.NHibernate.Helpers
{
    public class CultureProvider : ICultureProvider
    {
        private ILanguageService languageService = ServiceLocator.Current.GetInstance<ILanguageService>();

        public IDictionary<String, String> AvailableLanguages
        {
            get
            {
                IEnumerable<Language> languages = languageService.GetAll();
                IDictionary<String, String> availableLanguages = new Dictionary<String, String>();
                availableLanguages.Add(CultureHelper.NeutralCultureName, null);
                foreach (var language in languages)
                {
                    availableLanguages.Add(language.Title, language.Culture);
                }

                return availableLanguages;
            }
        }

        public CultureInfo DefaultCulture
        {
            get
            {
                Language language = languageService.DefaultLanguage;
                if (language != null)
                {
                    return CultureInfo.GetCultureInfo(language.Culture);
                }
                return null;
            }
        }
    }
}
