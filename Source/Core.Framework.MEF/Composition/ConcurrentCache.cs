using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Framework.MEF.Composition
{
    /// <summary>
    /// Defines a thread-safe cache of objects.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The value for the specified key.</typeparam>
    internal class ConcurrentCache<TKey, TValue>
    {
        #region Fields
        private readonly Dictionary<TKey, TValue> cache = new Dictionary<TKey, TValue>();
        private readonly object sync = new object();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the set of cached values.
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                lock (sync)
                    return cache.Values.ToArray();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Fetches the value with the specified key.  If the value does not exist, create it.
        /// </summary>
        /// <param name="key">The key of the value.</param>
        /// <param name="creator">The delegate used to create the instance.</param>
        /// <returns>The value with the specified key.  If the value does not exist, create it.</returns>
        public TValue Fetch(TKey key, Func<TValue> creator)
        {
            lock (sync)
            {
                TValue value;
                if (!cache.TryGetValue(key, out value))
                    cache[key] = value = creator();

                return value;
            }
        }
        #endregion
    }
}