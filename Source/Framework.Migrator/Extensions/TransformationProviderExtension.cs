// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransformationProviderExtension.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using ECM7.Migrator.Framework;
using Framework.Migrator.Fluent;

namespace Framework.Migrator.Extensions
{
    /// <summary>
    /// Extends <see cref="ITransformationProvider"/> functionality to provide fluent migration interface.
    /// </summary>
    public static class TransformationProviderExtension
    {
        /// <summary>
        /// New table definition.
        /// </summary>
        /// <example>
        ///     Database.AddTable("Users", t => {
        ///         t.PrimaryKey();                 // Adds long field named Id and makes it identity primary key.
        ///         t.String("Login");              // Adds String field named Login.
        ///         t.String("Password");           // Adds String field named Password.
        ///         t.String("FullName").Null();    // Adds nullable String field named FullName.
        ///     });
        /// </example>
        /// <param name="transformationProvider">The transformation provider.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="tableDefinition">The table definition expressions.</param>
        public static void AddTable(this ITransformationProvider transformationProvider, String tableName, Action<Table> tableDefinition)
        {
            var table = new AddTable(tableName);
            tableDefinition(table);
            table.Migrate(transformationProvider);
        }

        /// <summary>
        /// New table expression.
        /// </summary>
        /// <example>
        ///     Database.ChangeTable("Roles", t => {
        ///         t.ChangeColumn("Description").Text;     // Changes description field data type.
        ///         t.RemoveColumn("Name");                 // Removes name field.
        ///         t.Integer("Priority");                  // Adds priority integer field.
        ///     });
        /// </example>
        /// <param name="transformationProvider">The transformation provider.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="tableDefinition">The table change expressions.</param>
        public static void ChangeTable(this ITransformationProvider transformationProvider, String tableName, Action<ChangeTable> tableDefinition)
        {
            var table = new ChangeTable(tableName);
            tableDefinition(table);
            table.Migrate(transformationProvider);
        }
    }
}