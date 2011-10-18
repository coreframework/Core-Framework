using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.ContentPages.NHibernate.Models
{
    public class ContentPage : Entity, ILocalizable
    {
        private readonly IList<ContentPageLocale> currentContentPageLocales = new List<ContentPageLocale>();
        private IList<ILocale> currentLocales = new List<ILocale>();
        private ContentPageLocale currentLocale;

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
                if(currentLocales.Count == 0 && currentContentPageLocales.Count > 0)
                {
                    currentLocales = currentContentPageLocales.ToList().ConvertAll(mc => (ILocale)mc);
                }
                return currentLocales;
            }
            set
            {
                currentLocales = value;
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
                if (currentLocale == null)
                {
                    currentLocale = CultureHelper.GetCurrentLocale(CurrentLocales) as ContentPageLocale;
                    if(currentLocale == null)
                    {
                        currentLocale = new ContentPageLocale
                                             {
                                                 ContentPage = this,
                                                 Culture = null
                                             };
                    }
                }
                return currentLocale;
            }
        }

        #endregion
    }
}
