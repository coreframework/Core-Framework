using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Core.Web.NHibernate.Models;
using Framework.Core.DomainModel;
using Framework.Core.Localization;

namespace Core.Web.Areas.Admin.Models
{
    public class WidgetViewModel : IMappedModel<Widget, WidgetViewModel>
    {
        public IDictionary<String, String> Cultures { get; set; }
        public String SelectedCulture { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [AllowHtml]
        public String Title { get; set; }

        #region IMappedModel members

        public WidgetViewModel MapFrom(Widget from)
        {
            Id = from.Id;
            Title = from.Title;
            Cultures = CultureHelper.GetAvailableCultures();
            SelectedCulture = from.CurrentLocale.Culture;

            return this;
        }

        public Widget MapTo(Widget to)
        {
            to.Title = Title;

            return to;
        }

        #endregion
    }
}