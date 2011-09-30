// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Defaults.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Data;

namespace Framework.Migrator.Fluent
{
    /// <summary>
    /// Containts default conventions.
    /// </summary>
    public static class Defaults
    {
        /// <summary>
        /// Default primary key name.
        /// </summary>
        public static readonly String PrimaryKeyName = "Id";

        /// <summary>
        /// Default primary key data type.
        /// </summary>
        public static readonly DbType PrimaryKeyType = DbType.Int64;

        /// <summary>
        /// Default column size.
        /// </summary>
        public static readonly int ColumnSize = 255;

        /// <summary>
        /// Maximum String field length.
        /// </summary>
        public static readonly int MaxStringLength = 4000;

        /// <summary>
        /// Template for foreign key constraint name.
        /// </summary>
        public static readonly String ForeignKeyNameTemplate = "FK_{0}_{1}";

        /// <summary>
        /// Template for foreign key column name.
        /// </summary>
        public static readonly String ForeignKeyColumnTemplate = "{0}Id";
    }
}