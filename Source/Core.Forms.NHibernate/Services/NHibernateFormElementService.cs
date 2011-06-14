using Castle.Facilities.NHibernateIntegration;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Forms.NHibernate.Services
{
    public class NHibernateFormElementService: NHibernateDataService<FormElement>, IFormElementService
    {
        public NHibernateFormElementService(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
