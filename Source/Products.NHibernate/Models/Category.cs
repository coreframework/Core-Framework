using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Products.NHibernate.Models
{
    public class Category : Entity, ILocalizable
    {
        private IList<CategoryLocale> _currentCategoryLocales = new List<CategoryLocale>();
        private IList<ILocale> _currentLocales = new List<ILocale>();
        private CategoryLocale _currentLocale;

        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title
        {
            get
            {
                return ((CategoryLocale)CurrentLocale).Title;
            }
            set { ((CategoryLocale)CurrentLocale).Title = value; }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual String Description
        {
            get
            {
                return ((CategoryLocale)CurrentLocale).Description;
            }
            set { ((CategoryLocale)CurrentLocale).Description = value; }
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

        public virtual IList<CategoryLocale> CurrentCategoryLocales
        {
            get
            {
                return CurrentLocales.ToList().ConvertAll(mc => (CategoryLocale)mc);
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
                return typeof(CategoryLocale);
            }
        }

        public virtual ILocale CurrentLocale
        {
            get
            {
                if (_currentLocale == null)
                {
                    _currentLocale = CultureHelper.GetCurrentLocale(CurrentLocales) as CategoryLocale;
                    if (_currentLocale == null)
                    {
                        _currentLocale = new CategoryLocale
                        {
                            Category = this,
                            Culture = null
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
