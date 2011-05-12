// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Check.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Framework.Core
{
    /// <summary>
    /// Arguments validation helper.
    /// </summary>
    public static class Check
    {
        /// <summary>
        /// Validates that <paramref name="argument"/> is not empty.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="argumentName">Name of the argument.</param>
        [DebuggerStepThrough]
        public static void IsNotEmpty(Guid argument, String argumentName)
        {
            if (argument == Guid.Empty)
            {
                throw new ArgumentException(String.Format("\"{0}\" cannot be empty guid.", argumentName), argumentName);
            }
        }

        /// <summary>
        /// Validates that <paramref name="argument"/> is not null or empty.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="argumentName">Name of the argument.</param>
        [DebuggerStepThrough]
        public static void IsNotEmpty(String argument, String argumentName)
        {
            if (String.IsNullOrEmpty((argument ?? String.Empty).Trim()))
            {
                throw new ArgumentException(String.Format("\"{0}\" cannot be blank.", argumentName), argumentName);
            }
        }

        /// <summary>
        /// Validates that <paramref name="argument"/> length not exceed <paramref name="length"/>.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="length">The length.</param>
        /// <param name="argumentName">Name of the argument.</param>
        [DebuggerStepThrough]
        public static void IsNotOutOfLength(String argument, int length, String argumentName)
        {
            if (argument.Trim().Length > length)
            {
                throw new ArgumentException(String.Format("\"{0}\" cannot be more than {1} character.", argumentName, length), argumentName);
            }
        }

        /// <summary>
        /// Validates that <paramref name="argument"/> is not null.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="argumentName">Name of the argument.</param>
        [DebuggerStepThrough]
        public static void IsNotNull(Object argument, String argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Validates that <paramref name="argument"/> is not null.
        /// </summary>
        /// <typeparam name="T">Required argument type.</typeparam>
        /// <param name="argument">The argument.</param>
        /// <param name="argumentName">Name of the argument.</param>
        [DebuggerStepThrough]
        public static void IsValidType<T>(Object argument, String argumentName) 
            where T : class
        {
            if (argument as T == null)
            {
                throw new ArgumentException(String.Format("\"{0}\" could not be casted to \"{1}\".", argumentName, typeof(T).Name));
            }
        }

        /// <summary>
        /// Validates that <paramref name="argument"/> is not negative number.
        /// </summary>
        /// <typeparam name="T">Argument type.</typeparam>
        /// <param name="argument">The argument.</param>
        /// <param name="argumentName">Name of the argument.</param>
        [DebuggerStepThrough]
        public static void IsNotNegative<T>(T argument, String argumentName)
            where T : IComparable
        {
            if (argument.CompareTo(0) < 0)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        /// <summary>
        /// Validates that <paramref name="argument"/> is positive number.
        /// </summary>
        /// <typeparam name="T">Argument type.</typeparam>
        /// <param name="argument">The argument.</param>
        /// <param name="argumentName">Name of the argument.</param>
        [DebuggerStepThrough]
        public static void IsPositive<T>(T argument, string argumentName)
            where T : IComparable
        {
            if (argument.CompareTo(0) <= 0)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        /// <summary>
        /// Validates that <paramref name="argument"/> is not empty.
        /// </summary>
        /// <typeparam name="T">Generic enumeration type.</typeparam>
        /// <param name="argument">The argument.</param>
        /// <param name="argumentName">Name of the argument.</param>
        [DebuggerStepThrough]
        public static void IsNotEmpty<T>(IEnumerable<T> argument, string argumentName)
        {
            IsNotNull(argument, argumentName);

            if (!argument.Any())
            {
                throw new ArgumentException("Collection cannot be empty.", argumentName);
            }
        }

        /// <summary>
        /// Validates that file specified by path <paramref name="argument"/> exists.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="argumentName">Name of the argument.</param>
        [DebuggerStepThrough]
        public static void IsFileExists(String argument, String argumentName)
        {
            IsNotEmpty(argument, argumentName);
            if (!File.Exists(argument))
            {
                throw new ArgumentException(String.Format("File \"{0}\" does not exist.", argumentName), argumentName);
            }
        }
    }
}