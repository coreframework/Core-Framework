using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public String Title { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [DataType("FckEditorText"), Required]
        public String Content { get; set; }

        public ContentPageLocaleViewModel MapFrom(ContentPage from)
        {
            ContentPageId = from.Id;
            Cultures = CultureHelper.GetAvailableCultures();
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