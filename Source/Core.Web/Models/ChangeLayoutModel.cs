using System.Collections.Generic;
using Core.Web.NHibernate.Models;

namespace Core.Web.Models
{
    public class ChangeLayoutModel
    {
        public long PageId { get; set; }
        
        public PageLayout CurrentLayout { get; set; }

        public IEnumerable<PageLayoutTemplate> Layouts { get; set; }
    }
}