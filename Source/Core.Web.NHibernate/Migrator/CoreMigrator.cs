using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Linq;
using Core.Framework.Plugins.Web;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Core.Configuration;
using Framework.Migrator;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.NHibernate.Migrator
{
    /// <summary>
    /// 
    /// </summary>
    public class CoreMigrator
    {
        #region Fields

        private static CoreMigrator coreMigrator;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current instance.
        /// </summary>
        /// <value>The current.</value>
        public static CoreMigrator Current
        {
            get
            {
                if (coreMigrator == null)
                {
                    coreMigrator = new CoreMigrator();
                }
                return coreMigrator;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreMigrator"/> class.
        /// </summary>
        protected CoreMigrator() { }

        #endregion

        #region Methods

        /// <summary>
        /// Applies migrations for specified plugin.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        public void MigrateUp(ICorePlugin plugin)
        {
            Debug.Assert(plugin != null);
            Assembly pluginMigrationsAssembly = plugin.GetPluginMigrationsAssembly();
            if (pluginMigrationsAssembly != null)
            {
                var migrationService = ServiceLocator.Current.GetInstance<IMigrationService>();
                IEnumerable<Migration> pluginMigrations = FillPluginSchemaInfo(plugin, migrationService);
                using (ECM7.Migrator.Migrator migrator = GetMigrator(pluginMigrationsAssembly))
                {
                    migrator.Migrate();
                    IList<long> appliedMigrations = migrator.GetAppliedMigrations();
                    if (appliedMigrations != null && appliedMigrations.Count > 0)
                    {
                        IEnumerable<long> pluginMigrationsVersions = (from pluginMigration in pluginMigrations
                                                                      select pluginMigration.Version).AsEnumerable();
                        appliedMigrations.ToList().RemoveAll(
                            migration => pluginMigrationsVersions.Contains(migration));
                        if (appliedMigrations.Count > 0)
                        {
                            var pluginService = ServiceLocator.Current.GetInstance<IPluginService>();
                            Plugin pluginEntity = pluginService.FindPluginByIdentifier(plugin.Identifier);
                            foreach (long appliedMigration in appliedMigrations)
                            {
                                Migration migration = new Migration
                                                          {
                                                              Plugin = pluginEntity,
                                                              Version = appliedMigration
                                                          };
                                migrationService.Save(migration);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Rolls back all migrations.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        public void MigrateDown(ICorePlugin plugin)
        {
            Debug.Assert(plugin != null);
            Assembly pluginMigrationsAssembly = plugin.GetPluginMigrationsAssembly();
            if (pluginMigrationsAssembly != null)
            {
                using (ECM7.Migrator.Migrator migrator = GetMigrator(pluginMigrationsAssembly))
                {
                    if (migrator.AvailableMigrations != null && migrator.AvailableMigrations.Count > 0)
                    {
                        var migrationService = ServiceLocator.Current.GetInstance<IMigrationService>();
                        IEnumerable<Migration> pluginInstalledMigrations = FillPluginSchemaInfo(plugin, migrationService);
                        migrator.Migrate(0);
                        IEnumerable<long> pluginMigrations =
                            (from migrationsType in migrator.AvailableMigrations select migrationsType.Version).
                                AsEnumerable
                                ();
                        foreach (var pluginInstalledMigration in pluginInstalledMigrations)
                        {
                            if (pluginMigrations.Contains(pluginInstalledMigration.Version))
                            {
                                migrationService.Delete(pluginInstalledMigration);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Fills the plugin schema info.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        /// <param name="migrationService">The migration service.</param>
        /// <returns></returns>
        private static IEnumerable<Migration> FillPluginSchemaInfo(ICorePlugin plugin, IMigrationService migrationService)
        {
            ISchemaInfoService schemaInfoService = ServiceLocator.Current.GetInstance<ISchemaInfoService>();
            schemaInfoService.DeleteAll();
            IEnumerable<Migration> pluginMigrations = migrationService.FindPluginMigartions(plugin.Identifier);
            foreach (Migration pluginMigration in pluginMigrations)
            {
                var schemaInfo = new SchemaInfo
                {
                    Version = pluginMigration.Version
                };
                schemaInfoService.Save(schemaInfo);
            }
            return pluginMigrations;
        }

        /// <summary>
        /// Gets the migrator.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        private static ECM7.Migrator.Migrator GetMigrator(Assembly assembly)
        {
            IApplicationConfigurator applicationConfigurator = ServiceLocator.Current.GetInstance<IApplicationConfigurator>();
            ECM7.Migrator.Migrator migrator = null;
            if (applicationConfigurator != null && applicationConfigurator.DatabaseConfiguration != null)
            {
                migrator = new ECM7.Migrator.Migrator(MigratorUtility.GetDialect(applicationConfigurator.DatabaseConfiguration.Platform), applicationConfigurator.DatabaseConfiguration.GetConnectionString(), assembly);
            }
            return migrator;
        }


        #endregion

    }
}
