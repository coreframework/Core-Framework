using System;
using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models
{
    public class PageSettings : Entity
    {
        private LookAndFeelSettings lookAndFeelSettings = new LookAndFeelSettings();

        /// <summary>
        /// Gets or sets the custom CSS.
        /// </summary>
        /// <value>The custom CSS.</value>
        public virtual String CustomCSS { get; set; }

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
        public virtual Page Page { get; set; }
    }
}
