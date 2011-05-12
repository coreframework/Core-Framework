using System;
using System.ComponentModel.DataAnnotations;
using Core.Web.NHibernate.Models;
using Framework.Core.DomainModel;

namespace Core.Web.Models
{
    public class WidgetCSSModel : IMappedModel<PageWidgetSettings, WidgetCSSModel>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the setting id.
        /// </summary>
        /// <value>The setting id.</value>
        public long SettingId { get; set; }

        /// <summary>
        /// Gets or sets the widget id.
        /// </summary>
        /// <value>The widget id.</value>
        public long WidgetId { get; set; }

        /// <summary>
        /// Gets or sets the custom CSS classes.
        /// </summary>
        /// <value>The custom CSS classes.</value>
        [DataType("String")]
        public String CustomCSSClasses { get; set; }

        public PageCSSModel PageCssModel { get; set; }

        #endregion

        #region Methods

        public WidgetCSSModel MapFrom(PageWidgetSettings from)
        {
            SettingId = from.Id;
            WidgetId = from.Widget.Id;
            CustomCSSClasses = from.CustomCSSClasses;
            PageCssModel =
                new PageCSSModel().MapFrom(from.Widget.Page.Settings ?? new PageSettings {Page = from.Widget.Page});
            return this;
        }

        public PageWidgetSettings MapTo(PageWidgetSettings to)
        {
            to.CustomCSSClasses = CustomCSSClasses;
            return to;
        }

        #endregion

    }
}