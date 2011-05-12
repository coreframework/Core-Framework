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
        public const String PrimaryKeyName = "Id";

        /// <summary>
        /// Default primary key data type.
        /// </summary>
        public const DbType PrimaryKeyType = DbType.Int64;

        /// <summary>
        /// Default column size.
        /// </summary>
        public const int ColumnSize = 255;

        /// <summary>
        /// Maximum string field length.
        /// </summary>
        public const int MaxStringLength = 4000;

        /// <summary>
        /// Template for foreign key constraint name.
        /// </summary>
        public const String ForeignKeyNameTemplate = "FK_{0}_{1}";

        /// <summary>
        /// Template for foreign key column name.
        /// </summary>
        public const String ForeignKeyColumnTemplate = "{0}Id";
    }
}