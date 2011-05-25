using System;

namespace Core.Web.Models
{
    public class PageModeModel
    {
        public PageMode PageMode { get; set; }
        public String PageUrl { get; set; }
    }

    public enum PageMode
    {
        View = 1,
        Edit = 2
    }
}