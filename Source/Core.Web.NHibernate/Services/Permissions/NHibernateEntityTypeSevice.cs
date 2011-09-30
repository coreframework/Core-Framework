using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Framework.Permissions.Helpers;
using Core.Web.NHibernate.Contracts.Permissions;
using Core.Web.NHibernate.Models.Permissions;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services.Permissions
{
    public class NHibernateEntityTypeService : NHibernateDataService<EntityType>, IEntityTypeService
    {
        public NHibernateEntityTypeService(ISessionManager sessionManager) : base(sessionManager)
        {
        }

        public EntityType GetByType(Type type)
        {
            var query = CreateQuery();
            String typeName = PermissionsHelper.GetEntityType(type);
            return query.Where(entityType => entityType.Name == typeName).FirstOrDefault();
        }
    }
}
