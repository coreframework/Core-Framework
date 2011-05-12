// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChangeTable.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Framework.Migrator.Fluent
{
    /// <summary>
    /// Provides fluent interface for change table expressions.
    /// </summary>
    /// <example>
    ///     Database.ChangeTable("Roles", t => {
    ///         t.ChangeColumn("Description").Text;     // Changes description field data type.
    ///         t.RemoveColumn("Name");                 // Removes name field.
    ///         t.Integer("Priority");                  // Adds priority integer field.
    ///     });
    /// </example>
    public class ChangeTable : Table
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeTable"/> class.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        public ChangeTable(String tableName) : base(tableName)
        {
        }

        #endregion

        #region Table members

        /// <summary>
        /// Adds the column with name specified.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>table instance.</returns>
        public override Column AddColumn(String columnName)
        {
            var column = base.AddColumn(columnName);
            Parts.Add(column);
            return column;
        }

        #endregion

        #region Change table operations

        /// <summary>
        /// Changes the column with name specified.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>table instance.</returns>
        public virtual Column ChangeColumn(String columnName)
        {
            var column = new ChangeColumn(columnName, Name);
            Parts.Add(column);
            return column;
        }

        /// <summary>
        /// Removes the column with name specified.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>table instance.</returns>
        public virtual Column RemoveColumn(String columnName)
        {
            var column = new RemoveColumn(columnName, Name);
            Parts.Add(column);
            return column;
        }

        /// <summary>
        /// Removes the foreign key with name specified.
        /// </summary>
        /// <param name="reference">Name of the relationship.</param>
        /// <returns>table instance.</returns>
        public virtual ForeignKey RemoveForeignKey(String reference)
        {
            var foreignKey = new RemoveForeignKey(reference, Name);
            Parts.Add(foreignKey);
            return foreignKey;
        }

        #endregion
    }
}