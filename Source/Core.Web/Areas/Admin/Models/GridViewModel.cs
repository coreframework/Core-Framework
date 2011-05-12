using System;
using System.Collections.Generic;

namespace Core.Web.Areas.Admin.Models
{
    public class GridViewModel
    {
        public String DataUrl { get; set; }
        public String DetailsUrl { get; set; }
        public String SearchString { get; set; }
        public IList<GridColumnViewModel> Columns { get; set; }
        public String DefaultOrderColumn { get; set; }
        public bool IsAsc { get; set; }
        public String GridTitle { get; set; }

        public GridViewModel()
        {
            Columns = new List<GridColumnViewModel>();
            IsAsc = true;
        }
    }
}