using System.Web.Mvc;

namespace Core.Framework.MEF.Web
{
    /// <summary>
    /// Provides view engine support for imported areas.
    /// </summary>
    public class AreaViewEngine : WebFormViewEngine
    {
        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="AreaViewEngine" />.
        /// </summary>
        public AreaViewEngine()
        {
            MasterLocationFormats = new[]
                                    {
                                        "~/Areas/{1}/Views/{0}.master",
                                        "~/Views/{1}/{0}.master",
                                        "~/Views/Shared/{0}.master"
                                    };

            ViewLocationFormats = new[]
                                    {
                                        "~/Areas/{1}/Views/{0}.aspx",
                                        "~/Views/{1}/{0}.aspx",
                                        "~/Views/Shared/{0}.aspx"
                                    };

            AreaPartialViewLocationFormats = new[]
                                    {
                                        "~/Areas/{1}/Views/{0}.ascx",
                                        "~/Views/{1}/{0}.ascx",
                                        "~/Views/Shared/{0}.ascx"
                                    };
        }
        #endregion
    }
}
