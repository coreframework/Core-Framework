using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.Web.NHibernate.Models
{
    public class Widget : Entity, ILocalizable
    {
        #region Fields

        private IList<WidgetLocale> _currentWidgetLocales = new List<WidgetLocale>();
        private IList<ILocale> _currentLocales = new List<ILocale>();
        private WidgetLocale _currentLocale;

        #endregion

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public virtual String Identifier { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public virtual WidgetStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title
        {
            get
            {
                return ((WidgetLocale)CurrentLocale).Title;
            }
            set { ((WidgetLocale)CurrentLocale).Title = value; }
        }

        /// <summary>
        /// Gets or sets the plugin.
        /// </summary>
        /// <value>The plugin.</value>
        public virtual Plugin Plugin { get; set; }

        public virtual IList<ILocale> CurrentLocales
        {
            get
            {
                if (_currentLocales.Count == 0 && _currentWidgetLocales.Count > 0)
                {
                    _currentLocales = _currentWidgetLocales.ToList().ConvertAll(mc => (ILocale)mc);
                }
                return _currentLocales;
            }
            set
            {
                _currentLocales = value;
            }
        }

        public virtual IList<WidgetLocale> CurrentWidgetLocales
        {
            get
            {
                return CurrentLocales.ToList().ConvertAll(mc => (WidgetLocale)mc);
            }
            set
            {
                CurrentLocales = value.ToList().ConvertAll(mc => (ILocale)mc);
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
                            _currentLocale = (WidgetLocale)CurrentLocales[0];
                        }
                        else if (!CurrentLocales[0].Culture.Equals(CultureHelper.DefaultCultureName))
                        {
                            _currentLocale = (WidgetLocale)CurrentLocales[0];
                        }
                        else
                        {
                            _currentLocale = (WidgetLocale)CurrentLocales[1];
                        }
                    }
                    else
                    {
                        _currentLocale = new WidgetLocale
                        {
                            Widget = this,
                            Culture = CultureHelper.DefaultCultureName
                        };
                    }
                }
                return _currentLocale;
            }
        }
    }
}
