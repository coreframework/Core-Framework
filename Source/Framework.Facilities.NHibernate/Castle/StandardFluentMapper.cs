// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StandardFluentMapper.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions;
using Framework.Core.Configuration;
using Framework.Facilities.NHibernate.Conventions;
using ForeignKeyConvention = Framework.Facilities.NHibernate.Conventions.ForeignKeyConvention;

namespace Framework.Facilities.NHibernate.Castle
{
    /// <summary>
    /// Uses fluent mappings from specified assembly.
    /// </summary>
    public class StandardFluentMapper : INHibernateMapper
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardFluentMapper"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public StandardFluentMapper(Assembly assembly)
        {
            Assembly = assembly;

            Conventions = new IConvention[]
                              {
                                  new TableConvention(),
                                  new PrimaryKeyConvention(),
                                  new ForeignKeyConvention()
                              };
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the assembly.
        /// </summary>
        /// <value>The assembly.</value>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// Gets the conventions.
        /// </summary>
        /// <value>The conventions.</value>
        public IEnumerable<IConvention> Conventions { get; private set; }

        #endregion

        #region INHibernateMapper members

        /// <summary>
        /// Adds mappings to session configuration.
        /// </summary>
        /// <param name="mapping">The mapping configuration.</param>
        /// <param name="databaseConfiguration">The database configuration.</param>
        public virtual void Map(MappingConfiguration mapping, DatabaseConfiguration databaseConfiguration)
        {
            mapping.FluentMappings.AddFromAssembly(Assembly).Conventions.Add(Conventions.ToArray());
        }

        #endregion
    }
}