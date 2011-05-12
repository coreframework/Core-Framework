// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PathHelper.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Framework.MVC.Helpers
{
    /// <summary>
    /// Provides helper methods for paths processing.
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// Path separator symbol (/).
        /// </summary>
        public const String PathSeparator = "/";

        /// <summary>
        /// Virtual path start (~/).
        /// </summary>
        public const String VirtualPathStart = "~/";

        /// <summary>
        /// Splits path to chains using <see cref="PathSeparator"/>.
        /// </summary>
        /// <param name="path">The path to split.</param>
        /// <returns>Path chains or empty array.</returns>
        public static IEnumerable<String> SplitPath(String path)
        {
            String[] chains;
            if (!String.IsNullOrEmpty(path))
            {
                chains = path.Split(PathSeparator.ToCharArray());
            }
            else
            {
                chains = new String[0];
            }
            return chains;
        }

        /// <summary>
        /// Trims the virtual path start.
        /// </summary>
        /// <param name="path">The virtual path.</param>
        /// <returns>The string that remains after all occurrences of <see cref="VirtualPathStart"/> are removed from the start of <paramref name="path"/>.</returns>
        public static String TrimVirtualPathStart(String path)
        {
            return path.TrimStart(VirtualPathStart.ToCharArray());
        }
    }
}