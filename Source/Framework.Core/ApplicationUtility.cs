using System;
using System.Web;

namespace Framework.Core
{
    /// <summary>
    /// Application helper class. 
    /// </summary>
    public static class ApplicationUtility
    {
        #region Fields

        ///<summary>
        /// Base Url Compulsory End.
        ///</summary>
        public static String BaseUrlCompulsoryEnd = "/";

        #endregion

        #region Properties

        /// <summary>
        /// Full application path.
        /// </summary>
        public static String Path
        {
            get
            {
                String path = HttpContext.Current.Request.ApplicationPath;
                if (path != null)
                {
                    if (!path.EndsWith(BaseUrlCompulsoryEnd))
                    {
                        path += BaseUrlCompulsoryEnd;
                    }
                }
                return path;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the short URL.
        /// </summary>
        /// <param name="rawUrl">The raw URL.</param>
        /// <returns>Exclude scheme, host part from full url.</returns>
        public static String GetUrlPath(String rawUrl)
        {
            return rawUrl.StartsWith(Path) ? rawUrl.Substring(Path.Length) : rawUrl;
        }

        #endregion
    }
}
