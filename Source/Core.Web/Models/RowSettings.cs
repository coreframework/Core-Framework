using System.Collections.Generic;

namespace Core.Web.Models
{
    public class RowSettings
    {
        public long RowId { get; set; }
        public IList<int> ColumnsWidth { get; set; }
    }
}