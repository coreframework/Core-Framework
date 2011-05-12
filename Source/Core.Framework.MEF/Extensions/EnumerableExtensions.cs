using System;
using System.Collections.Generic;

namespace Core.Framework.MEF.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="IEnumerable{T}" /> type.
    /// </summary>
    public static class EnumerableExtensions
    {
        #region Methods
        /// <summary>
        /// Performs the given <see cref="Action{T}" /> on each item in the enumerable.
        /// </summary>
        /// <typeparam name="T">The type of item in the enumerable.</typeparam>
        /// <param name="items">The enumerable of items.</param>
        /// <param name="action">The action to perform on each item.</param>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            Throw.Throw.IfArgumentNull(items, "items");
            Throw.Throw.IfArgumentNull(action, "action");

            foreach (T item in items)
                action(item);
        }
        #endregion
    }
}