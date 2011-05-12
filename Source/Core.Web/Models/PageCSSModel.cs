using System;
using System.ComponentModel.DataAnnotations;
using Core.Web.NHibernate.Models;
using Framework.Core.DomainModel;

namespace Core.Web.Models
{
    public class PageCSSModel : IMappedModel<PageSettings, PageCSSModel>
    {
        #region Properties

        public long SettingId { get; set; }

        public long PageId { get; set; }

        [DataType("MultilineText")]
        public String CustomCSS { get; set; }

        #endregion

        #region Methods

        public PageCSSModel MapFrom(PageSettings from)
        {
            SettingId = from.Id;
            PageId = from.Page.Id;
            CustomCSS = from.CustomCSS;
            return this;
        }

        public PageSettings MapTo(PageSettings to)
        {
            to.CustomCSS = CustomCSS;
            return to;
        }

        #endregion

    }
}