using System;
using System.ComponentModel;

namespace Core.Web.Models
{
    public class PageModeModel
    {
        public PageMode PageMode { get; set; }
        public String PageUrl { get; set; }
    }

    public enum PageMode
    {
        [Description(".PanelView")]
        View = 1,

        [Description(".PanelEdit")]
        Edit = 2
    }
}