using System;
using System.Collections.Generic;

namespace Framework.Core.Extensions
{
    /// <summary>
    /// Extends <see cref="IEnumerable"/> class functionality.
    /// </summary>
    public static class ListExtentions
    {
        /// <summary>
        /// Updates the specified IEnumerable list.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="outer">The outer.</param>
        /// <param name="updator">The updator.</param>
        public static void Update<TSource>(this IEnumerable<TSource> outer, Action<TSource> updator)
        {
            foreach (var item in outer)
            {
                updator(item);
            }
        }
    }
}
