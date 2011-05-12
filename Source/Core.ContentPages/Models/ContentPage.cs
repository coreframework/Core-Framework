using System;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Data;

namespace Core.ContentPages.Models
{
    public class ContentPage : Entity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public virtual String Content { get; set; }

        #endregion
    }
}
