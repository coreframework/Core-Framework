// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveColumn.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using ECM7.Migrator.Framework;

namespace Framework.Migrator.Fluent
{
    /// <summary>
    /// Removes table column.
    /// </summary>
    public class RemoveColumn : Column
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveColumn"/> class.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="tableName">Name of the table.</param>
        public RemoveColumn(String columnName, String tableName) : base(columnName, tableName)
        {
        }

        #endregion

        #region IDatabaseBuilder members

        /// <summary>
        /// Removes table column.
        /// </summary>
        /// <param name="database">The database.</param>
        public override void Migrate(ITransformationProvider database)
        {
            database.RemoveColumn(TableName, Name);
        }

        #endregion
    }
}