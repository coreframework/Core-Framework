// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddTable.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using ECM7.Migrator.Framework;

namespace Framework.Migrator.Fluent
{
    /// <summary>
    /// Provides fluent interface for new table definition.
    /// </summary>
    /// <example>
    ///     Database.AddTable("Users", t => {
    ///         t.PrimaryKey();                 // Adds long field named Id and makes it identity primary key.
    ///         t.String("Login");              // Adds String field named Login.
    ///         t.String("Password");           // Adds String field named Password.
    ///         t.String("FullName").Null();    // Adds nullable String field named FullName.
    ///     });
    /// </example>
    public class AddTable : Table
    {
        #region Fields

        private readonly List<IColumn> newColumns = new List<IColumn>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddTable"/> class.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        public AddTable(String tableName) : base(tableName)
        {
        }

        #endregion

        #region Table members

        /// <summary>
        /// Adds the column with name specified. Adds column definition to columns list to generate add table statement.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>table instance.</returns>
        public override Column AddColumn(String columnName)
        {
            var column = base.AddColumn(columnName);
            newColumns.Add(column);
            return column;
        }

        /// <summary>
        /// Adds foreigns key. Adds foreign key column definition to columns list to generate add table statement and avoid column generation by foreign key.
        /// </summary>
        /// <param name="reference">The relationship name.</param>
        /// <returns>table instance.</returns>
        public override ForeignKey ForeignKey(String reference)
        {
            var foreignKey = base.ForeignKey(reference).WithoutColumn();
            newColumns.Add(foreignKey);
            return foreignKey;
        }

        #endregion

        #region IMigrationPart members

        /// <summary>
        /// Adds new table with columns specified and executes all inner migration parts (column, keys, constraint manipulation).
        /// </summary>
        /// <param name="database">The database.</param>
        public override void Migrate(ITransformationProvider database)
        {
            database.AddTable(Name, newColumns.Select(x => x.GetColumn()).ToArray());

            base.Migrate(database);
        }

        #endregion
    }
}