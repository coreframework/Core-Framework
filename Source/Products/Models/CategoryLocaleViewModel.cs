using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Framework.Core.DomainModel;
using Framework.Core.Localization;
using Products.NHibernate.Models;

namespace Products.Models
{
    public class CategoryLocaleViewModel : IMappedModel<Category, CategoryLocaleViewModel>
    {
        public IDictionary<String, String> Cultures { get; set; }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the selected culture.
        /// </summary>
        public String SelectedCulture { get; set; }
        
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public String Title { get; set; }
       
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [DataType("FckEditorText"), Required]
        public String Description { get; set; }

        public CategoryLocaleViewModel MapFrom(Category from)
        {
            CategoryId = from.Id;
            Cultures = CultureHelper.GetAvailableCultures();
            SelectedCulture = from.CurrentLocale.Culture;
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