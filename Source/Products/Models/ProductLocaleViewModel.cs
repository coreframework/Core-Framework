using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Framework.Core.DomainModel;
using Framework.Core.Localization;
using Framework.MVC.Metadata.Attributes;
using Products.Helpers;
using Products.NHibernate.Models;

namespace Products.Models
{
    public class ProductLocaleViewModel : IMappedModel<Product, ProductLocaleViewModel>
    {
        public IDictionary<String, String> Cultures { get; set; }
        
        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Gets or sets the selected culture.
        /// </summary>
        public String SelectedCulture { get; set; }

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
        public virtual String Description { get; set; }

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

        public ProductLocaleViewModel MapFrom(Product @from)
        {
            ProductId = from.Id;
            Cultures = CultureHelper.GetAvailableCultures();
            SelectedCulture = from.CurrentLocale.Culture;
            
            Title = from.Title;
            Description = from.Description;
            FileName = from.FileName;
            Price = from.Price;

            return this;
        }

        public Product MapTo(Product to)
        {
           // to.Title = Title;
           // to.Description = Description;
            to.Price = Price;
            to.FileName = FileName;

            return to;
        }
    }
}