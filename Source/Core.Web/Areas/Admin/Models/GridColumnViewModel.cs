using System;

namespace Core.Web.Areas.Admin.Models
{
    public class GridColumnViewModel
    {
        public String Name { get; set; }
        public String Index { get; set; }
        public String Align { get; set; }
        public bool Resizable { get; set; }
        public int? Width { get; set; }
        public bool Sortable { get; set; }
        public bool Hidden { get; set; }
   
        public GridColumnViewModel()
        {
            Align = "left";
            Sortable = true;
        }
    }
}