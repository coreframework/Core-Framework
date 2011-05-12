using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services
{
    public class NHibernateSchemaInfoService : NHibernateDataService<SchemaInfo>, ISchemaInfoService
    {
        public NHibernateSchemaInfoService(ISessionManager sessionManager) : base(sessionManager)
        {}
    }
}
