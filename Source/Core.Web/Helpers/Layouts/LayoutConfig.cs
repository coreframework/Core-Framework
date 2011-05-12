using System.Collections.Generic;
using Core.Web.NHibernate.Models;

namespace Core.Web.Helpers.Layouts
{
    public class LayoutConfig
    {
        #region Fields

        private static Dictionary<PageLayout, LayoutSettings> _layouts;

        #endregion

        #region Properties

        public static  IDictionary<PageLayout, LayoutSettings> Layouts
        {
            get 
            {
                return _layouts ?? (_layouts = new Dictionary<PageLayout, LayoutSettings>
                                                   {
                                                       {
                                                           PageLayout.Layout1,
                                                           new LayoutSettings {ColumnsNumber = 1}
                                                       },
                                                       {
                                                            PageLayout.Layout2,
                                                            new LayoutSettings {ColumnsNumber = 3}
                                                        },
                                                        {
                                                            PageLayout.Layout3,
                                                            new LayoutSettings {ColumnsNumber = 2}
                                                        },
                                                        {
                                                            PageLayout.Layout4,
                                                            new LayoutSettings {ColumnsNumber = 2}
                                                        },
                                                        {
                                                            PageLayout.Layout5,
                                                            new LayoutSettings {ColumnsNumber = 3}
                                                        },
                                                        {
                                                            PageLayout.Layout6, 
                                                            new LayoutSettings {ColumnsNumber = 3}
                                                        }
                                                   });

            }
        }

        #endregion
    }
}