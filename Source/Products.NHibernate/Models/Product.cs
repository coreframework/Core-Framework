using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Products.NHibernate.Models
{
    public class Product : Entity, ILocalizable
    {
        private readonly IList<ProductLocale> currentProductLocales = new List<ProductLocale>();
        private IList<ILocale> currentLocales = new List<ILocale>();
        private ProductLocale currentLocale;

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

        public virtual IList<ILocale> CurrentLocales
        {
            get
            {
                if (currentLocales.Count == 0 && currentProductLocales.Count > 0)
                {
                    currentLocales = currentProductLocales.ToList().ConvertAll(mc => (ILocale) mc);
                }
                return currentLocales;
            }
            set { currentLocales = value; }
        }

        public virtual IList<ProductLocale> CurrentProductLocales
        {
            get
            {
                return CurrentLocales.ToList().ConvertAll(mc => (ProductLocale)mc);
            }
            set
            {
                CurrentLocales = value.ToList().ConvertAll(mc => (ILocale)mc);
            }
        }

        public virtual Type LocaleType
        {
            get
            {
                return typeof(ProductLocale);
            }
        }


        public virtual ILocale CurrentLocale
        {
            get
            {
                if (currentLocale == null)
                {
                    currentLocale = CultureHelper.GetCurrentLocale(CurrentLocales) as ProductLocale;
                    if (currentLocale == null)
                    {
                        currentLocale = new ProductLocale
                        {
                            Product = this,
                            Culture = null
                        };
                    }
                }
                return currentLocale;
            }
        }
    }
}
