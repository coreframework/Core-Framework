using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.News.Nhibernate.Models
{
    public class NewsCategory : Entity, ILocalizable
    {
        private IList<NewsCategoryLocale> _currentCategoryLocales = new List<NewsCategoryLocale>();
        private IList<ILocale> _currentLocales = new List<ILocale>();
        private NewsCategoryLocale _currentLocale;

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
                if (_currentLocales.Count == 0 && _currentCategoryLocales.Count > 0)
                {
                    _currentLocales = _currentCategoryLocales.ToList().ConvertAll(mc => (ILocale)mc);
                }
                return _currentLocales;
            }
            set
            {
                _currentLocales = value;
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
                if (_currentLocale == null)
                {
                    //2 - max locales number: current locale and default locale
                    if (CurrentLocales != null && CurrentLocales.Count > 0 && CurrentLocales.Count <= 2)
                    {
                        if (CurrentLocales.Count == 1)
                        {
                            _currentLocale = (NewsCategoryLocale)CurrentLocales[0];
                        }
                        else if (!CurrentLocales[0].Culture.Equals(CultureHelper.DefaultCultureName))
                        {
                            _currentLocale = (NewsCategoryLocale)CurrentLocales[0];
                        }
                        else
                        {
                            _currentLocale = (NewsCategoryLocale)CurrentLocales[1];
                        }
                    }
                    else
                    {
                        _currentLocale = new NewsCategoryLocale
                        {
                            Category = this,
                            Culture = CultureHelper.DefaultCultureName
                        };
                    }
                }
                return _currentLocale;
            }
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
