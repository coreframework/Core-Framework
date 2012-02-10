// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveForeignKey.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using ECM7.Migrator.Framework;

namespace Framework.Migrator.Fluent
{
    /// <summary>
    /// Removes foreign key.
    /// </summary>
    public class RemoveForeignKey : ForeignKey
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveForeignKey"/> class.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <param name="foreignKeyTable">The foreign key table.</param>
        public RemoveForeignKey(String reference, String foreignKeyTable) : base(reference, foreignKeyTable)
        {
        }

        #endregion

        #region IMigrationPart members

        /// <summary>
        /// Removes foreign key and foreign key column if specified.
        /// </summary>
        /// <param name="database">The database.</param>
        public override void Migrate(ITransformationProvider database)
        {
            database.RemoveConstraint(ForeignKeyTable, Name);
            
            if (GenerateColumn)
            {
                database.RemoveColumn(ForeignKeyTable, GetColumn().Name);
            }
        }

        #endregion
    }
}