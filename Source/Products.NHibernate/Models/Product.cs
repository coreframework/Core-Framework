using System;
using System.Collections.Generic;
using Framework.Core.Localization;
using Framework.Facilities.NHibernate.Objects;

namespace Products.NHibernate.Models
{
    public class Product : LocalizableEntity<ProductLocale>
    {
        private  IList<Category> categories = new List<Category>();

        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title
        {
            get
            {
                return ((ProductLocale)CurrentLocale).Title;
            }
            set { ((ProductLocale)CurrentLocale).Title = value; }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual String Description
        {
            get
            {
                return ((ProductLocale)CurrentLocale).Description;
            }
            set { ((ProductLocale)CurrentLocale).Description = value; }
        }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        /// <value>The file name.</value>
        public virtual String FileName { get; set; }

        /// <summary>
        ///  Gets or sets the price.
        /// </summary>
        public virtual int Price { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        public virtual IList<Category> Categories
        {
            get { return categories; }
            set { categories = value; }
        }

        /// <summary>
        /// Gets or sets the Create Date
        /// </summary>
        public virtual DateTime? CreateDate { get; set; }

        #endregion

        public virtual Type LocaleType
        {
            get
            {
                return typeof(ProductLocale);
            }
        }

        public override ILocale InitializeLocaleEntity()
        {
            return new ProductLocale
            {
                Product = this,
                Culture = null
            };
        }
    }
}
