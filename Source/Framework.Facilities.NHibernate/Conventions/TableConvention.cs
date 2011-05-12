// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TableConvention.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using Framework.Core.Helpers;

namespace Framework.Facilities.NHibernate.Conventions
{
    /// <summary>
    /// Specifies table convention.
    /// </summary>
    public class TableConvention : IClassConvention
    {
        #region IClassConvention members

        /// <summary>
        /// Applies the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply(IClassInstance instance)
        {
            instance.Table(Inflector.Pluralize(instance.Name));
        }

        #endregion
    }
}