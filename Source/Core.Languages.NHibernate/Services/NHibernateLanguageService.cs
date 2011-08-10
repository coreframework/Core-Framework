using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Languages.NHibernate.Contracts;
using Core.Languages.NHibernate.Models;
using Framework.Facilities.NHibernate;
using Framework.Facilities.NHibernate.Filters;
using NHibernate;

namespace Core.Languages.NHibernate.Services
{
    public class NHibernateLanguageService : NHibernateDataService<Language>, ILanguageService
    {
        protected override ISession Session
        {
            get
            {
                ISession session;

                if (String.IsNullOrEmpty(Alias))
                {
                    session = SessionManager.OpenSession();
                }
                else
                {
                    session = SessionManager.OpenSession(Alias);
                }
                session.EnableFilter("CultureFilter").SetParameter(CultureFilter.FilterParamName, String.Empty)
                    .SetParameter(CultureFilter.DefaultCultureFilterParamName, String.Empty);

                return session;
            }
        }

        public NHibernateLanguageService(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public int GetCount(IQueryable<Language> baseQuery)
        {
            return baseQuery.Count();
        }

        public Language GetDefaultLanguage()
        {
            var query = CreateQuery().Where(language => language.IsDefault);

            return query.FirstOrDefault();
        }

        public IQueryable<Language> GetSearchQuery(string searchString)
        {
            var baseQuery = CreateQuery();
            if (String.IsNullOrEmpty(searchString))
            {
                return baseQuery;
            }
            return baseQuery.Where(language => language.Title.Contains(searchString));
        }
    }
}
