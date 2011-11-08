using Castle.Facilities.NHibernateIntegration;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.WebContent.NHibernate.Services
{
    public class NHibernateSectionSettingsService: NHibernateDataService<SectionSettings>, ISectionSettingsService
    {
        public NHibernateSectionSettingsService(ISessionManager sessionManager) : base(sessionManager)
        {
          
        }
    }
}
