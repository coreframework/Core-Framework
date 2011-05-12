// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabasePlatform.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Core.Configuration
{
    /// <summary>
    /// Specifies supported database platforms.
    /// </summary>
    public enum DatabasePlatform
    {
        /// <summary>
        /// Microsoft SQL Server 2008 database.
        /// </summary>
        SqlServer,

        /// <summary>
        /// Microsoft SQL Server 2000 database.
        /// </summary>
        SqlServer2000,

        /// <summary>
        /// Microsoft SQL Server 2005 database.
        /// </summary>
        SqlServer2005,

        /// <summary>
        /// SQLite database.
        /// </summary>
        SQLite,

        /// <summary>
        /// MySQL database.
        /// </summary>
        MySQL,

        /// <summary>
        /// PostgreSQL database.
        /// </summary>
        PostgreSQL,

        /// <summary>
        /// Oracle9 database.
        /// </summary>
        Oracle9,

        /// <summary>
        /// Oracle10 database.
        /// </summary>
        Oracle10
    }
}