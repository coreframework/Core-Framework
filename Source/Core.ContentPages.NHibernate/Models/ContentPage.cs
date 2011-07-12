using System;
using System.Collections.Generic;
using FluentNHibernate.Data;

namespace Core.ContentPages.NHibernate.Models
{
    public class ContentPage : Entity
    {
        private IList<ContentPageLocale> _currentContentPageLocales = new List<ContentPageLocale>();
        private IList<ILocale> _currentLocales = new List<ILocale>();
        private ContentPageLocale _currentLocale;

        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title
        {
            get
            {
                return ((ContentPageLocale)CurrentLocale).Title;
            }
            set { ((ContentPageLocale)CurrentLocale).Title = value; }
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public virtual String Content
        {
            get
            {
                return ((ContentPageLocale)CurrentLocale).Content;
            }
            set { ((ContentPageLocale)CurrentLocale).Content = value; }
        }

        /// <summary>
        /// Gets or sets the widgets.
        /// </summary>
        /// <value>The widgets.</value>
        public virtual IEnumerable<ContentPageWidget> Widgets { get; set; }

        public virtual IList<ILocale> CurrentLocales
        {
            get
            {
                if(_currentLocales.Count == 0 && _currentContentPageLocales.Count > 0)
                {
                    _currentLocales = _currentContentPageLocales.ToList().ConvertAll(mc => (ILocale)mc);
                }
                return _currentLocales;
            }
            set
            {
                _currentLocales = value;
            }
        }

        public virtual IList<ContentPageLocale> CurrentContentPageLocales
        {
            get
            {
                return CurrentLocales.ToList().ConvertAll(mc => (ContentPageLocale) mc);
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
                return typeof(ContentPageLocale);
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
                            _currentLocale = (ContentPageLocale) CurrentLocales[0];
                        }
                        else if (!CurrentLocales[0].Culture.Equals(CultureHelper.DefaultCultureName))
                        {
                            _currentLocale = (ContentPageLocale) CurrentLocales[0];
                        }
                        else
                        {
                            _currentLocale = (ContentPageLocale)CurrentLocales[1];
                        }
                    }
                    else
                    {
                        _currentLocale = new ContentPageLocale
                                             {
                                                 ContentPage = this,
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
