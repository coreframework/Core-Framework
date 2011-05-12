// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MigratorUtility.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Framework.Core.Configuration;

namespace Framework.Migrator
{
    /// <summary>
    /// Provides helper methods for migrator.
    /// </summary>
    public class MigratorUtility
    {
        #region Fields

        private static readonly Dictionary<DatabasePlatform, String> migratorDialects = new Dictionary<DatabasePlatform, String> 
        {
            { DatabasePlatform.SqlServer, "SqlServer" },
            { DatabasePlatform.SqlServer2000, "SqlServer" },
            { DatabasePlatform.SqlServer2005, "SqlServer" },
            { DatabasePlatform.SQLite, "SQLite" },
            { DatabasePlatform.MySQL, "MySql" },
            { DatabasePlatform.PostgreSQL, "PostgreSQL" },
            { DatabasePlatform.Oracle9, "Oracle" },
            { DatabasePlatform.Oracle10, "Oracle" },
        };

        #endregion

        #region Helper methods

        /// <summary>
        /// Gets migrator dialect for specified <paramref name="platform"/>.
        /// </summary>
        /// <param name="platform">Database platform.</param>
        /// <returns>Migrator dialect for specified <paramref name="platform"/>.</returns>
        public static String GetDialect(DatabasePlatform platform)
        {
            var dialect = String.Empty;
            if (migratorDialects.ContainsKey(platform))
            {
                dialect = migratorDialects[platform];
            }
            return dialect;
        }

        #endregion
    }
}