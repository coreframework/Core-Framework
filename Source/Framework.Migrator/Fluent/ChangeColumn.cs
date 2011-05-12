// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChangeColumn.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using ECM7.Migrator.Framework;

namespace Framework.Migrator.Fluent
{
    /// <summary>
    /// Changes existing column.
    /// </summary>
    public class ChangeColumn : Column
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeColumn"/> class.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="tableName">Name of the table.</param>
        public ChangeColumn(String columnName, String tableName) : base(columnName, tableName)
        {
        }

        #region IDatabaseBuilder members

        /// <summary>
        /// Changes existing column.
        /// </summary>
        /// <param name="database">The database.</param>
        public override void Migrate(ITransformationProvider database)
        {
            database.ChangeColumn(TableName, GetColumn());
        }

        #endregion
    }
}