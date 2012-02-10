// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YamlDocument.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using Yaml.Grammar;

namespace Framework.Core.Helpers.Yaml
{
    /// <summary>
    /// Wraps yaml document for using dynamic objects.
    /// </summary>
    public static class YamlDocument
    {
        /// <summary>
        /// Loads document froms file.
        /// </summary>
        /// <param name="filePath">Full file path.</param>
        /// <returns>yaml document.</returns>
        public static dynamic FromFile(String filePath)
        {
            var document = YamlParser.Load(filePath).Documents.Last();

            Object result;
            if (TryMapValue(document.Root, out result))
            {
                return result;
            }

            throw new InvalidOperationException("Unexpected parsed value.");
        }

        /// <summary>
        /// Tries wrap <see cref="DataItem"/> object for using dynamic features.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="result">The result.</param>
        /// <returns>wrapped <see cref="DataItem"/> object.</returns>
        internal static bool TryMapValue(DataItem value, out Object result)
        {
            if (value is Scalar)
            {
                result = (value as Scalar).Text;
                return true;
            }

            if (value is Sequence)
            {
                result = (value as Sequence).Enties.Select(MapValue).ToList();
                return true;
            }

            if (value is Mapping)
            {
                result = new YamlMapping(value as Mapping);
                return true;
            }

            result = null;
            return false;
        }

        private static Object MapValue(DataItem value)
        {
            Object result;
            TryMapValue(value, out result);
            return result;
        }
    }
}