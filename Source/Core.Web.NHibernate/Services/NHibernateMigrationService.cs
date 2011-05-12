using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services
{
    public class NHibernateMigrationService : NHibernateDataService<Migration>, IMigrationService
    {
        #region Constructors

        public NHibernateMigrationService(ISessionManager sessionManager) : base(sessionManager)
        {}

        #endregion

        #region Methods

        public IEnumerable<Migration> FindPluginMigartions(String pluginIdentifier)
        {
            var query = from migration in CreateQuery()
                        where migration.Plugin.Identifier == pluginIdentifier
                        select migration;
            return query.AsEnumerable();
        }

        #endregion

    }
}
