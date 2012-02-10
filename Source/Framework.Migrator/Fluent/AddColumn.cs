// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddColumn.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Data;
using ECM7.Migrator.Framework;

namespace Framework.Migrator.Fluent
{
    /// <summary>
    /// Adds new column to existing table.
    /// </summary>
    public class AddColumn : Column
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddColumn"/> class.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="tableColumnName">Name of the table.</param>
        public AddColumn(String columnName, String tableColumnName) : base(columnName, tableColumnName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddColumn"/> class.
        /// </summary>
        /// <param name="columnType">Type of the column.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="tableName">Name of the table.</param>
        public AddColumn(DbType columnType, String columnName, String tableName) : base(columnType, columnName, tableName)
        {
        }

        #endregion

        #region IMigrationPart members

        /// <summary>
        /// Adds new column to existing table.
        /// </summary>
        /// <param name="database">The database.</param>
        public override void Migrate(ITransformationProvider database)
        {
            database.AddColumn(TableName, GetColumn());
        }

        #endregion
    }
}