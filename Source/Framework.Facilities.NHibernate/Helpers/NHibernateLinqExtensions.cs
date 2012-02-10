// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NHibernateLinqExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace Framework.Facilities.NHibernate.Helpers
{
    /// <summary>
    ///  Additional info http://stackoverflow.com/questions/5549404/nhibernate-3-1-0-4000-nullreferenceexception-using-query-and-nhibernate-faci.
    /// </summary>
    public static class NHibernateLinqExtensions
    {
        /// <summary>
        /// Performs a LINQ query on the specified type.
        /// </summary>
        /// <typeparam name="T">The type to perform the query on.</typeparam>
        /// <param name="session">ISession session.</param>
        /// <returns>A new <see cref="IQueryable{T}"/>.</returns>
        /// <remarks>This method is provided as a workaround for the current bug in the NHibernate LINQ extension methods.</remarks>
        public static IQueryable<T> Linq<T>(this ISession session)
        {
            return new NhQueryable<T>(session.GetSessionImplementation());
        }

        /// <summary>
        /// Performs a LINQ query on the specified type.
        /// </summary>
        /// <typeparam name="T">The type to perform the query on.</typeparam>
        /// <param name="session">IStatelessSession session.</param>
        /// <returns>A new <see cref="IQueryable{T}"/>.</returns>
        /// <remarks>This method is provided as a workaround for the current bug in the NHibernate LINQ extension methods.</remarks>
        public static IQueryable<T> Linq<T>(this IStatelessSession session)
        {
            return new NhQueryable<T>(session.GetSessionImplementation());
        }
    }
}
