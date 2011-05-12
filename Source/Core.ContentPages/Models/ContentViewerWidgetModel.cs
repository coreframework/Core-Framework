using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.ContentPages.NHibernate.Contracts;
using Core.ContentPages.NHibernate.Models;
using Framework.Core.DomainModel;
using Microsoft.Practices.ServiceLocation;

namespace Core.ContentPages.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ContentViewerWidgetModel : IMappedModel<ContentPageWidget, ContentViewerWidgetModel>
    {
        #region Fields

        private List<ContentPage> _contentPages;

        #endregion

        #region Properties

        public long Id { get; set; }


        /// <summary>
        /// Gets the content pages.
        /// </summary>
        /// <value>The content pages.</value>
        public List<ContentPage> ContentPages
        {
            get
            {
                if (_contentPages == null)
                {
                    var contentPageService = ServiceLocator.Current.GetInstance<IContentPageService>();
                    _contentPages = (List<ContentPage>)contentPageService.GetAll();
                }
                return _contentPages;
            }
        }

        /// <summary>
        /// Gets or sets the content page.
        /// </summary>
        /// <value>The content page.</value>
        [Required]
        public long ContentPageId { get; set; }

        #endregion

        public ContentViewerWidgetModel MapFrom(ContentPageWidget from)
        {
            Id = from.Id;
            ContentPageId = from.ContentPage != null ? from.ContentPage.Id : 0;
            return this;
        }

        public ContentPageWidget MapTo(ContentPageWidget to)
        {
            to.Id = Id;
            to.ContentPage = new ContentPage()
                                 {
                                    Id = ContentPageId
                                 };
            return to;
        }
    }
}