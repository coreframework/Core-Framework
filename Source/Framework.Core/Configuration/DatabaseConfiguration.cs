// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseConfiguration.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Framework.Core.Configuration
{
    /// <summary>
    /// Provides universal configuration for supported database platforms. Use properties dictionary for platform specific options.
    /// </summary>
    public class DatabaseConfiguration
    {
        #region Connection Strings

        /// <summary>
        /// Use connection String custom property to override default connection string.
        /// </summary>
        public static readonly String ConnectionStringKey = "connection-string";

        /// <summary>
        /// Database name for sqlite in memory database.
        /// </summary>
        public static readonly String InMemoryDatabase = ":memory:";

        // Default connection String templates: {0} - Host, {1} - Database, {2} - Username, {3} - Password.
        private static readonly Dictionary<DatabasePlatform, String> templates = new Dictionary<DatabasePlatform, String> 
        {
            { DatabasePlatform.SqlServer, "Server={0};Database={1};User ID={2};Password={3};" },
            { DatabasePlatform.SqlServer2000, "Server={0};Database={1};User ID={2};Password={3};" },
            { DatabasePlatform.SqlServer2005, "Server={0};Database={1};User ID={2};Password={3};" },
            { DatabasePlatform.SQLite, "Data Source={1};Version=3;" },
            { DatabasePlatform.MySQL, "Server={0};Database={1};Uid={2};Pwd={3};" },
            { DatabasePlatform.PostgreSQL, "Server={0};initial catalog={1};User ID={2};Password={3};" },
            { DatabasePlatform.Oracle9, "Server={0};initial catalog={1};User ID={2};Password={3};" },
            { DatabasePlatform.Oracle10, "Server={0};initial catalog={1};User ID={2};Password={3};" },
        };

        #endregion

        #region Fields

        private readonly Dictionary<String, String> properties = new Dictionary<String, String>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the database platform.
        /// </summary>
        /// <value>The platform.</value>
        public DatabasePlatform Platform { get; set; }

        /// <summary>
        /// Gets or sets the database host.
        /// </summary>
        /// <value>The database server host name.</value>
        public String Host { get; set; }

        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        /// <value>The database name.</value>
        public String Database { get; set; }

        /// <summary>
        /// Gets or sets the database username.
        /// </summary>
        /// <value>The database username.</value>
        public String Username { get; set; }

        /// <summary>
        /// Gets or sets the database password.
        /// </summary>
        /// <value>The database password.</value>
        public String Password { get; set; }

        /// <summary>
        /// Gets custom properties.
        /// </summary>
        /// <value>The custom properties.</value>
        public Dictionary<String, String> Properties
        {
            get
            {
                return properties;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the database specific connection string.
        /// </summary>
        /// <returns>connection string.</returns>
        public String GetConnectionString()
        {
            String connectionString;

            if (Properties.ContainsKey(ConnectionStringKey))
            {
                connectionString = Properties[ConnectionStringKey];
            }
            else
            {
                if (templates.ContainsKey(Platform))
                {
                    connectionString = String.Format(templates[Platform], Host, Database, Username, Password);
                }   
                else
                {
                    throw new NotSupportedException(String.Format("{0} is not supported.", Platform));
                }
            }

            return connectionString;
        }

        #endregion
    }
}