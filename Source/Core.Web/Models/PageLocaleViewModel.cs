using System;
using System.Collections.Generic;
using Core.Web.NHibernate.Models;
using Framework.Core.DomainModel;
using Framework.Core.Localization;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Models
{
    public class PageLocaleViewModel : IMappedModel<Page, PageLocaleViewModel>
    {
        public IDictionary<String, String> Cultures { get; set; }
        public long Id { get; set; }
        public String SelectedCulture { get; set; }
        public String Title { get; set; }
        public String Url { get; set; }
        public long? ParentPageId { get; set; }
        public String LocalesString { get; set; }
        
        public PageLocaleViewModel MapFrom(Page from)
        {
            Id = from.Id;
            Cultures = CultureHelper.GetAvailableCultures();
            SelectedCulture = from.CurrentLocale.Culture;
            Title = from.Title;
            Url = from.Url;
            ParentPageId = from.ParentPageId;

            return this;
        }

        public Page MapTo(Page to)
        {
            to.Url = Url;

            return to;
        }
    }
}