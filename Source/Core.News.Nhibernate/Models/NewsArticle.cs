using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.News.Nhibernate.Models
{
    public class NewsArticle : Entity, ILocalizable
    {
        private IList<NewsArticleLocale> _currentNewsArticleLocales = new List<NewsArticleLocale>();
        private IList<ILocale> _currentLocales = new List<ILocale>();
        private NewsArticleLocale _currentLocale;

        public NewsArticle()
        {
            PublishDate = DateTime.Now;
        }

        #region Properties

        public virtual long WidgetId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title
        {
            get
            {
                return ((NewsArticleLocale)CurrentLocale).Title;
            }
            set { ((NewsArticleLocale)CurrentLocale).Title = value; }
        }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        public virtual String Summary
        {
            get
            {
                return ((NewsArticleLocale)CurrentLocale).Summary;
            }
            set { ((NewsArticleLocale)CurrentLocale).Summary = value; }
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public virtual String Content
        {
            get
            {
                return ((NewsArticleLocale)CurrentLocale).Content;
            }
            set { ((NewsArticleLocale)CurrentLocale).Content = value; }
        }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        public virtual IList<NewsCategory> Categories { get; set; }

        /// <summary>
        /// Gets or sets the status Id.
        /// </summary>
        /// <value>The status Id.</value>
        public virtual int StatusId { get; set; }

        public virtual DateTime CreateDate { get; set; }

        public virtual DateTime PublishDate { get; set; }

        public virtual DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the widgets.
        /// </summary>
        /// <value>The widgets.</value>
//        public virtual IEnumerable<NewsArticleWidget> Widgets { get; set; }

        public virtual IList<ILocale> CurrentLocales
        {
            get
            {
                if (_currentLocales.Count == 0 && _currentNewsArticleLocales.Count > 0)
                {
                    _currentLocales = _currentNewsArticleLocales.ToList().ConvertAll(mc => (ILocale)mc);
                }
                return _currentLocales;
            }
            set
            {
                _currentLocales = value;
            }
        }

        public virtual IList<NewsArticleLocale> CurrentNewsArticleLocales
        {
            get
            {
                return CurrentLocales.ToList().ConvertAll(mc => (NewsArticleLocale)mc);
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
                return typeof(NewsArticleLocale);
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
                            _currentLocale = (NewsArticleLocale)CurrentLocales[0];
                        }
                        else if (!CurrentLocales[0].Culture.Equals(CultureHelper.DefaultCultureName))
                        {
                            _currentLocale = (NewsArticleLocale)CurrentLocales[0];
                        }
                        else
                        {
                            _currentLocale = (NewsArticleLocale)CurrentLocales[1];
                        }
                    }
                    else
                    {
                        _currentLocale = new NewsArticleLocale
                        {
                            NewsArticle = this,
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
