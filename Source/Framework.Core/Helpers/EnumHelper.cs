// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumHelper.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Framework.Core.Helpers
{
    /// <summary>
    /// Provides helper methods for enumerations.
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>parsed value or default enum value.</returns>
        public static T Parse<T>(String value, T defaultValue)
            where T : struct
        {
            T result;
            if (!Enum.TryParse(value, true, out result))
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>parsed value or default enum value.</returns>
        public static T Parse<T>(String value)
            where T : struct
        {
            return Parse(value, default(T));
        }

        /// <summary>
        /// Gets the <see cref="String"/> key for value specified.
        /// </summary>
        /// <typeparam name="T">Type of enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>the <see cref="String"/> key.</returns>
        public static String GetKey<T>(T value)
            where T : struct
        {
            return value.ToString().ToLowerInvariant();
        }

        /// <summary>
        /// Gets the <see cref="String"/> keys for values of enum specified.
        /// </summary>
        /// <typeparam name="T">Type of enum.</typeparam>
        /// <returns>collection of enum <see cref="String"/> key.</returns>
        public static String[] GetKeys<T>()
            where T : struct
        {
            var keys = new List<String>();
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                keys.Add(GetKey((T)value));
            }
            return keys.ToArray();
        }

        /// <summary>
        /// Gets the string representation for value specified.
        /// </summary>
        /// <typeparam name="T">Type of enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>Enum value description or name.</returns>
        public static String Humanize<T>(T value)
            where T : struct
        {
            var text = Enum.GetName(typeof(T), value);
            var description = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            if (description != null)
            {
                text = description.Description;
            }
            return text;
        }
    }
}