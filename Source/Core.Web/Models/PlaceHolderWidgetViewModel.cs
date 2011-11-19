using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Web.Helpers;
using Core.Web.NHibernate.Models;
using Framework.Core.DomainModel;

namespace Core.Web.Models
{
    public class PlaceHolderWidgetViewModel : IMappedModel<PageWidget, PlaceHolderWidgetViewModel>
    {
        #region Fields

        private IList<Widget> availableWidgets;

        #endregion

        #region Properties

        public long Id { get; set; }

        public bool IsOnTemplate { get; set; }

        public IList<Widget> AvailableWidgets
        {
            get
            {
                if (availableWidgets == null)
                {
                    availableWidgets = WidgetHelper.GetAvailableWidgets(false);
                }
                return availableWidgets;
            }
        }

        [Required]
        public long? WidgetId { get; set; }

        #endregion


        public PlaceHolderWidgetViewModel MapFrom(PageWidget from)
        {
            Id = from.Id;
            IsOnTemplate = from.Page.IsTemplate;

            return this;
        }

        public PageWidget MapTo(PageWidget to)
        {
            return to;
        }
    }
}