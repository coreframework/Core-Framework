using System;
using Framework.Core.Localization;
using Framework.Facilities.NHibernate.Objects;

namespace Core.Web.NHibernate.Models
{
    public class Widget : LocalizableEntity<WidgetLocale>
    {
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

        public virtual WidgetLocale Locale { get; set; }

        public override ILocale InitializeLocaleEntity()
        {
            return new WidgetLocale
                       {
                           Widget = this,
                           Culture = null
                       };
        }

        public virtual bool IsDetailsWidget { get; set; }
    }
}
