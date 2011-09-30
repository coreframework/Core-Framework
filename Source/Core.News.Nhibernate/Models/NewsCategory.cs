using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.News.Nhibernate.Models
{
    public class NewsCategory : Entity, ILocalizable
    {
        private readonly IList<NewsCategoryLocale> currentCategoryLocales = new List<NewsCategoryLocale>();
        private IList<ILocale> currentLocales = new List<ILocale>();
        private NewsCategoryLocale currentLocale;

        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title
        {
            get
            {
                return ((NewsCategoryLocale)CurrentLocale).Title;
            }
            set { ((NewsCategoryLocale)CurrentLocale).Title = value; }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual String Description
        {
            get
            {
                return ((NewsCategoryLocale)CurrentLocale).Description;
            }
            set { ((NewsCategoryLocale)CurrentLocale).Description = value; }
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
                if (currentLocales.Count == 0 && currentCategoryLocales.Count > 0)
                {
                    currentLocales = currentCategoryLocales.ToList().ConvertAll(mc => (ILocale)mc);
                }
                return currentLocales;
            }
            set
            {
                currentLocales = value;
            }
        }

        public virtual IList<NewsCategoryLocale> CurrentCategoryLocales
        {
            get
            {
                return CurrentLocales.ToList().ConvertAll(mc => (NewsCategoryLocale)mc);
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
                return typeof(NewsCategoryLocale);
            }
        }

        public virtual ILocale CurrentLocale
        {
            get
            {
                if (currentLocale == null)
                {
                    currentLocale = CultureHelper.GetCurrentLocale(CurrentLocales) as NewsCategoryLocale;
                    if (currentLocale == null)
                    {
                        currentLocale = new NewsCategoryLocale
                        {
                            Category = this,
                            Culture = null
                        };
                    }
                }
                return currentLocale;
            }
        }
    }
}
