using System;
using System.Collections.Generic;
using Core.ContentPages.NHibernate.Models;
using Framework.Core.DomainModel;
using Framework.Core.Localization;
using Microsoft.Practices.ServiceLocation;

namespace Core.ContentPages.Models
{
    public class ContentPageLocaleViewModel : IMappedModel<ContentPage, ContentPageLocaleViewModel>
    {
        public IDictionary<String, String> Cultures { get; set; }
        public long ContentPageId { get; set; }
        public String SelectedCulture { get; set; }
        public String Title { get; set; }
        public String Content { get; set; }

        public ContentPageLocaleViewModel MapFrom(ContentPage from)
        {
            ContentPageId = from.Id;
            Cultures = ServiceLocator.Current.GetInstance<ICultureProvider>().GetAvailableLanguages();
            SelectedCulture = from.CurrentLocale.Culture;
            Title = from.Title;
            Content = from.Content;

            return this;
        }

        public ContentPage MapTo(ContentPage to)
        {
            to.Title = Title;
            to.Content = Content;
            
            return to;
        }
    }
}