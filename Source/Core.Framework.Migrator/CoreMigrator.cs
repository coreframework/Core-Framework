using System.Reflection;
using Core.Framework.Plugins.Web;
using Framework.Core.Configuration;
using Framework.Migrator;
using Microsoft.Practices.ServiceLocation;

namespace Core.Framework.Migrator
{
    public class CoreMigrator
    {
        #region Fields

        private static CoreMigrator coreMigrator;

        #endregion

        #region Properties

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

        protected CoreMigrator() { }

        #endregion

        #region Methods

        public void MigrateUp(ICorePlugin plugin)
        {
            ServiceLocator.Current.GetInstance<ISIS>();
        }

        public void MigrateDown()
        {

        }

        private ECM7.Migrator.Migrator GetMigrator(Assembly assembly)
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
