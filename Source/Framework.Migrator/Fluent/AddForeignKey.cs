// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddForeignKey.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using ECM7.Migrator.Framework;

namespace Framework.Migrator.Fluent
{
    /// <summary>
    /// Adds foreign key to new or existing table.
    /// </summary>
    public class AddForeignKey : ForeignKey
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddForeignKey"/> class.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <param name="foreignKeyTable">The foreign key table.</param>
        public AddForeignKey(String reference, String foreignKeyTable) : base(reference, foreignKeyTable)
        {
        }

        #endregion

        #region IMigrationPart members

        /// <summary>
        /// Adds foreign key and foreign key column if specified.
        /// </summary>
        /// <param name="database">The database.</param>
        public override void Migrate(ITransformationProvider database)
        {
            if (GenerateColumn)
            {
                database.AddColumn(ForeignKeyTable, GetColumn());
            }

            database.AddForeignKey(Name, ForeignKeyTable,
                                   new[] {ForeignKeyColumn}, PrimaryKeyTable, new[] {PrimaryKeyColumn}, DeleteConstraint, UpdateConstraint);
        }

        #endregion
    }
}