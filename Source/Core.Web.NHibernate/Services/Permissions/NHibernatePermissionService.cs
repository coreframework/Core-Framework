using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Framework.Permissions.Helpers;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Contracts.Permissions;
using Core.Web.NHibernate.Models.Permissions;
using Framework.Facilities.NHibernate;
using NHibernate;
using NHibernate.Linq;

namespace Core.Web.NHibernate.Services.Permissions
{
    /// <summary>
    /// NHibernate implementation for <see cref="IPermissionService"/>.
    /// </summary>
    public class NHibernatePermissionService : NHibernateDataService<Permission>, IPermissionService
    {
        #region Fields

        private readonly IRoleService roleService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NHibernatePermissionService"/> class.
        /// </summary>
        /// <param name="sessionManager">The session manager.</param>
        /// <param name="roleService">The role service.</param>
        public NHibernatePermissionService(ISessionManager sessionManager, IRoleService roleService) : base(sessionManager)
        {
            this.roleService = roleService;
        }

        #endregion

        #region NHibernateDataService members

        /// <summary>
        /// Set cache options for linq queries.
        /// </summary>
        /// <returns>Query object used to evaluate an expression tree.</returns>
        public override IQueryable<Permission> CreateQuery()
        {
            var query = Session.Linq<Permission>();
            query.QueryOptions.SetCachable(true).SetCacheMode(CacheMode.Normal);
            return query.AsQueryable();
        }

        #endregion
     
        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <param name="roleId">The role id.</param>
        /// <param name="resourceId">The resource id.</param>
        /// <param name="entityId">The entity id.</param>
        /// <returns></returns>
        public Permission GetPermission(long roleId, long resourceId, long? entityId)
        {
            var query = from permission in CreateQuery()
                        where permission.Role.Id == roleId && permission.EntityType.Id == resourceId && permission.EntityId==entityId 
                        select permission;

           return query.FirstOrDefault();
        }
     
        public IEnumerable<Permission> GetResourcePermissions(Type entityType, long entityId, bool includeEntityNull)
        {
            if (!includeEntityNull)
            {
                var query = from permission in CreateQuery()
                            where permission.EntityType.Name == PermissionsHelper.GetEntityType(entityType) &&
                                  permission.EntityId == entityId
                            select permission;
                
                return query.ToList();
            }
            else
            {
                var query = from permission in CreateQuery()
                            where permission.EntityType.Name == PermissionsHelper.GetEntityType(entityType) &&
                                  (permission.EntityId == entityId || permission.EntityId == null)
                            select permission;
                
                return query.ToList();
            }
        }
    }
}