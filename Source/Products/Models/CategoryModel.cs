using System;
using System.Collections.Generic;
using Products.NHibernate.Models;

namespace Products.Models
{
    public class CategoryModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public virtual long Id {get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title { get; set; }


        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual String Description { get; set; }

        /// <summary>
        /// Gets or sets products assigned to the category
        /// </summary>
        public virtual List<Product> Products { get; set; }

        #endregion
    }
}