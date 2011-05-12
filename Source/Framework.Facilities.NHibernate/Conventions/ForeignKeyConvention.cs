// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ForeignKeyConvention.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Framework.Facilities.NHibernate.Conventions
{
    /// <summary>
    /// Specifies foreign key naming convention.
    /// </summary>
    public class ForeignKeyConvention : IReferenceConvention
    {
        #region IReferenceConvention members

        /// <summary>
        /// Applies the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply(IManyToOneInstance instance)
        {
            instance.Column(instance.Name + "Id");
        }

        #endregion
    }
}