using System.Collections.Generic;
using Core.Web.Helpers.HtmlExtensions.MenuTreeView;
using Core.Web.NHibernate.Models;

namespace Core.Web.Areas.Navigation.Models
{
    public class SiteMapViewWidgetModel : IComposite<SiteMapViewWidgetModel>
    {
        public Page Page { get; set; }

        public SiteMapViewWidgetModel Parent { get; set; }

        public IEnumerable<SiteMapViewWidgetModel> Children { get; set; }
    }
}