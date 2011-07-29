using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.Forms.NHibernate.Models
{
    /// <summary>
    /// Describes form element, which form can contain.
    /// </summary>
    public class FormElement : Entity, ILocalizable
    {
        #region Fields

        private readonly IList<FormElementLocale> _currentFormElementLocales = new List<FormElementLocale>();
        private IList<ILocale> _currentLocales = new List<ILocale>();
        private FormElementLocale _currentLocale;

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title
        {
            get
            {
                return ((FormElementLocale)CurrentLocale).Title;
            }
            set
            {
                ((FormElementLocale)CurrentLocale).Title = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of element.
        /// </summary>
        /// <value>The type.</value>
        public virtual FormElementType Type { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        public virtual String ElementValues
        {
            get
            {
                return ((FormElementLocale)CurrentLocale).ElementValues;
            }
            set
            {
                ((FormElementLocale)CurrentLocale).ElementValues = value;
            }
        }

        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        /// <value>The order number.</value>
        public virtual Int32 OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this element is required.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this element is required; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the form.
        /// </summary>
        /// <value>The form.</value>
        public virtual Form Form { get; set; }

        /// <summary>
        /// Gets or sets the length of the max.
        /// </summary>
        /// <value>The length of the max.</value>
        public virtual long? MaxLength { get; set; }

        /// <summary>
        /// Gets or sets the regex template.
        /// </summary>
        /// <value>The regex template.</value>
        public virtual RegexTemplate RegexTemplate { get; set; }

        public virtual IList<ILocale> CurrentLocales
        {
            get
            {
                if (_currentLocales.Count == 0 && _currentFormElementLocales.Count > 0)
                {
                    _currentLocales = _currentFormElementLocales.ToList().ConvertAll(mc => (ILocale)mc);
                }
                return _currentLocales;
            }
            set
            {
                _currentLocales = value;
            }
        }

        public virtual IList<FormElementLocale> CurrentFormElementLocales
        {
            get
            {
                return CurrentLocales.ToList().ConvertAll(mc => (FormElementLocale)mc);
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
                return typeof(FormElementLocale);
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
                            _currentLocale = (FormElementLocale)CurrentLocales[0];
                        }
                        else if (!CurrentLocales[0].Culture.Equals(CultureHelper.DefaultCultureName))
                        {
                            _currentLocale = (FormElementLocale)CurrentLocales[0];
                        }
                        else
                        {
                            _currentLocale = (FormElementLocale)CurrentLocales[1];
                        }
                    }
                    else
                    {
                        _currentLocale = new FormElementLocale
                        {
                            FormElement = this,
                            Culture = CultureHelper.DefaultCultureName
                        };
                    }
                }
                return _currentLocale;
            }
        }
        #endregion
    }
}
