using System;
using System.ComponentModel.DataAnnotations;
using Framework.Core.DomainModel;
using Framework.Mvc.Metadata.Attributes;
using Products.Helpers;
using Products.NHibernate.Models;

namespace Products.Models
{
    public class ProductViewModel : IMappedModel<Product, ProductViewModel>
    {
        #region Properties
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public virtual String Title { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        [Required]
        public virtual String Summary { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [DataType("FckEditorText"), Required]
        public virtual String Description { get; set; }

        public DateTime? PublishedDate { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [DataType("ImageUpload")]
        [ImageUpload(Resize = true, ResizeWidth = 120, ResizeHeight = 100)]
        [FileType(Preset = FileTypesPreset.Images)]
        public virtual String FileName { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>The price.</value>
        [Required]
        [PositiveNumber]
        public virtual int Price { get; set; }
        #endregion

        public ProductViewModel MapFrom(Product from)
        {
           // Id = from.Id;
            Title = from.Title;
            Description = from.Description;
            FileName = from.FileName;
            Price = from.Price;
            return this;

        }

        public Product MapTo(Product to)
        {
           // to.Id = Id;
            to.Title = Title;
            to.Description = Description;
            to.FileName = FileName;
            to.Price = Price;
            return to;
        }
    }
}
