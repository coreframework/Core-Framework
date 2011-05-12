using System;
using Core.Web.NHibernate.Models.Permissions;
using Framework.Core.Services;

namespace Core.Web.NHibernate.Contracts.Permissions
{
    /// <summary>
    /// Specifies interface for role data service.
    /// </summary>
    public interface IEntityTypeService : IDataService<EntityType>
    {
        EntityType GetByType(Type type);
    }
}