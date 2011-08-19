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
    }
}
