// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMigrationPart.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ECM7.Migrator.Framework;

namespace Framework.Migrator.Fluent
{
    /// <summary>
    /// Logical separated migration part.
    /// </summary>
    public interface IMigrationPart
    {
        /// <summary>
        /// Executes migration part.
        /// </summary>
        /// <param name="database">The database.</param>
        void Migrate(ITransformationProvider database);
    }
}