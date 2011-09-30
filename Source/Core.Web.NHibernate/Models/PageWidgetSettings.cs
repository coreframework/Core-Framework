using System;
using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PageWidgetSettings : Entity
    {
        private LookAndFeelSettings lookAndFeelSettings = new LookAndFeelSettings();

        /// <summary>
        /// Gets or sets the custom CSS classes.
        /// </summary>
        /// <value>The custom CSS classes.</value>
        public virtual String CustomCSSClasses { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public virtual LookAndFeelSettings LookAndFeelSettings
        {
            get { return lookAndFeelSettings; }
            set { lookAndFeelSettings = value; }
        }

        /// <summary>
        /// Gets or sets the widget.
        /// </summary>
        /// <value>The widget.</value>
        public virtual PageWidget Widget { get; set; }
    }
}
