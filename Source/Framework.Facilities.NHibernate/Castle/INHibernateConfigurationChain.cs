// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INHibernateConfigurationChain.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using NHibernate.Cfg;

namespace Framework.Facilities.NHibernate.Castle
{
    /// <summary>
    /// Allows altering of the raw NHibernate Configuration object before creation.
    /// </summary>
    public interface INHibernateConfigurationChain
    {
        /// <summary>
        /// Processes the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        void Process(Configuration configuration);
    }
}