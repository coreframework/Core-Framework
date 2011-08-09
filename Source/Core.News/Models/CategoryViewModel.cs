using System;
using System.ComponentModel.DataAnnotations;
using Framework.Core.DomainModel;
using Core.News.Nhibernate.Models;

namespace Core.News.Models
{
    public class CategoryViewModel : IMappedModel<NewsCategory, CategoryViewModel>
    {
        #region Properties
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public virtual String Title { get; set; }


        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [DataType("FckEditorText"), Required]
        public virtual String Description { get; set; }
        #endregion

        public CategoryViewModel MapFrom(NewsCategory from)
        {
            Title = from.Title;
            Description = from.Description;
            return this;
        }

        public NewsCategory MapTo(NewsCategory to)
        {
            to.Title = Title;
            to.Description = Description;
            return to;
        }
    }
}