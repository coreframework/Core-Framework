// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INHibernateMapper.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using FluentNHibernate.Cfg;
using Framework.Core.Configuration;

namespace Framework.Facilities.NHibernate.Castle
{
    /// <summary>
    /// Specifies interface for modules nhibernate mappers. Each mapper can add fluent mappings, hbm mappings or use auto-mapping.
    /// </summary>
    public interface INHibernateMapper
    {
        /// <summary>
        /// Adds mappings to session configuration.
        /// </summary>
        /// <param name="mapping">The mapping configuration.</param>
        /// <param name="databaseConfiguration">The database configuration.</param>
        void Map(MappingConfiguration mapping, DatabaseConfiguration databaseConfiguration);
    }
}