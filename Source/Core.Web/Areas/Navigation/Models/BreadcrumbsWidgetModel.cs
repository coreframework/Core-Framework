using Core.Web.NHibernate.Models.Widgets;
using Framework.Core.DomainModel;

namespace Core.Web.Areas.Navigation.Models
{
    public class BreadcrumbsWidgetModel : IMappedModel<BreadcrumbsWidget, BreadcrumbsWidgetModel>
    {
        #region Properties

        public long Id { get; set; }

        public bool ShowHomePage { get; set; }
      
        #endregion

        public BreadcrumbsWidgetModel MapFrom(BreadcrumbsWidget from)
        {
            Id = from.Id;
            ShowHomePage = from.ShowHomePage;
            return this;
        }

        public BreadcrumbsWidget MapTo(BreadcrumbsWidget to)
        {
            to.Id = Id;
            to.ShowHomePage = ShowHomePage;
            return to;
        }
    }
}