using Castle.Facilities.NHibernateIntegration;
using Core.Languages.NHibernate.Contracts;
using Core.Languages.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Languages.NHibernate.Services
{
    public class NHibernateLanguageService : NHibernateDataService<Language>, ILanguageService
    {
        public NHibernateLanguageService(ISessionManager sessionManager) : base(sessionManager)
        {
        }
    }
}
