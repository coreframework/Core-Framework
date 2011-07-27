using System;
using System.ComponentModel.DataAnnotations;
using Framework.Core.DomainModel;
using Products.NHibernate.Models;

namespace Products.Models
{
    public class CategoryViewModel : IMappedModel<Category, CategoryViewModel>
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

        public CategoryViewModel MapFrom(Category from)
        {
            Title = from.Title;
            Description = from.Description;
            return this;
        }

        public Category MapTo(Category to)
        {
            to.Title = Title;
            to.Description = Description;
            return to;
        }
    }
}