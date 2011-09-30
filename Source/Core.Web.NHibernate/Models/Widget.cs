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

        private readonly IList<WidgetLocale> currentWidgetLocales = new List<WidgetLocale>();
        private IList<ILocale> currentLocales = new List<ILocale>();
        private WidgetLocale currentLocale;

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
                if (currentLocales.Count == 0 && currentWidgetLocales.Count > 0)
                {
                    currentLocales = currentWidgetLocales.ToList().ConvertAll(mc => (ILocale)mc);
                }
                return currentLocales;
            }
            set
            {
                currentLocales = value;
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
                if (currentLocale == null)
                {
                    currentLocale = CultureHelper.GetCurrentLocale(CurrentLocales) as WidgetLocale;
                    if (currentLocale == null)
                    {
                        currentLocale = new WidgetLocale
                        {
                            Widget = this,
                            Culture = null
                        };
                    }
                }
                return currentLocale;
            }
        }

        public virtual WidgetLocale Locale { get; set; }
    }
}
