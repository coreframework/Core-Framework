using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Framework.Core.DomainModel;
using Framework.Core.Localization;
using Core.News.Nhibernate.Models;

namespace Core.News.Models
{
    public class CategoryLocaleViewModel : IMappedModel<NewsCategory, CategoryLocaleViewModel>
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

        public CategoryLocaleViewModel MapFrom(NewsCategory from)
        {
            CategoryId = from.Id;
            Cultures = CultureHelper.GetAvailableCultures();
            SelectedCulture = from.CurrentLocale.Culture;
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