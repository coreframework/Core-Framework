// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IColumn.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Migrator.Fluent
{
    /// <summary>
    /// Specifies interface for database objects containing column. F.e., table column, foreign key, primary key.
    /// </summary>
    public interface IColumn
    {
        /// <summary>
        /// Gets the column specification.
        /// </summary>
        /// <returns>Column specification.</returns>
        ECM7.Migrator.Framework.Column GetColumn();
    }
}