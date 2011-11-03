using System;
using System.ComponentModel.DataAnnotations;
using Core.ContentPages.NHibernate.Models;
using Framework.Core.DomainModel;

namespace Core.ContentPages.Models
{
    public class ContentPageViewModel : IMappedModel<ContentPage, ContentPageViewModel>
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public virtual String Title { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [DataType("FckEditorText"), Required]
        public virtual String Content { get; set; }

        public ContentPageViewModel MapFrom(ContentPage from)
        {
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
