using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Permissions.Operations;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.Web.NHibernate.Models
{
    [Export(typeof(IPermissible))]
    public class Page : Entity, IPermissible, ILocalizable
    {
        #region Fields

        private readonly IList<PageWidget> widgets = new List<PageWidget>();
        private readonly IList<Page> children = new List<Page>();

        private readonly IList<PageLocale> currentPageLocales = new List<PageLocale>();
        private IList<ILocale> currentLocales = new List<ILocale>();
        private PageLocale currentLocale;

        private String permissionTitle = "Pages";
        private IEnumerable<IPermissionOperation> operations = OperationsHelper.GetOperations<PageOperations>();

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
                return ((PageLocale)CurrentLocale).Title;
            }
            set { ((PageLocale)CurrentLocale).Title = value; }
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public virtual String Url { get; set; }

        /// <summary>
        /// Gets or sets the parent page id.
        /// </summary>
        /// <value>The parent page id.</value>
        public virtual long? ParentPageId { get; set; }

        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        /// <value>The order number.</value>
        public virtual Int32 OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the widgets.
        /// </summary>
        /// <value>The widgets.</value>
        public virtual IEnumerable<PageWidget> Widgets
        {
            get
            {
                return widgets;
            }
        }

        public virtual IEnumerable<Page> Children
        {
            get
            {
                return children;
            }
        }

        public virtual PageLayout PageLayout { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public virtual PageSettings Settings { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [hide in main menu].
        /// </summary>
        /// <value><c>true</c> if [hide in main menu]; otherwise, <c>false</c>.</value>
        public virtual bool HideInMainMenu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is service page.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is service page; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsServicePage { get; set; }

        /// <summary>
        /// Removes the widget.
        /// </summary>
        /// <param name="widget">The widget.</param>
        public virtual void RemoveWidget(PageWidget widget)
        {
            widgets.Remove(widget);
        }

        public virtual void AddWidget(PageWidget widget)
        {
            widgets.Add(widget);
        }

        #region IPermissible Members

        /// <summary>
        /// Gets or sets the permission title.
        /// </summary>
        /// <value>The permission title.</value>
        public virtual String PermissionTitle
        {
            get { return permissionTitle; }
            set { permissionTitle = value; }
        }

        /// <summary>
        /// Gets or sets the permission operations.
        /// </summary>
        /// <value>The permission operations.</value>
        public virtual IEnumerable<IPermissionOperation> Operations
        {
            get { return operations; }
            set { operations = value; }
        }

        #endregion

        public virtual IList<ILocale> CurrentLocales
        {
            get
            {
                if (currentLocales.Count == 0 && currentPageLocales.Count > 0)
                {
                    currentLocales = currentPageLocales.ToList().ConvertAll(mc => (ILocale)mc);
                }
                return currentLocales;
            }
            set
            {
                currentLocales = value;
            }
        }

        public virtual IList<PageLocale> CurrentPageLocales
        {
            get
            {
                return CurrentLocales.ToList().ConvertAll(mc => (PageLocale)mc);
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
                    currentLocale = CultureHelper.GetCurrentLocale(CurrentLocales) as PageLocale;
                    if (currentLocale == null)
                    {
                        currentLocale = new PageLocale
                        {
                            Page = this,
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
