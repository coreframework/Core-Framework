// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrimaryKeyConvention.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Framework.Facilities.NHibernate.Conventions
{
    /// <summary>
    /// Specifies primary key accept convention.
    /// </summary>
    public class PrimaryKeyConvention : IIdConvention
    {
        #region IIdConvention members

        /// <summary>
        /// Applies the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply(IIdentityInstance instance)
        {
            instance.GeneratedBy.Identity();
        }

        #endregion
    }
}