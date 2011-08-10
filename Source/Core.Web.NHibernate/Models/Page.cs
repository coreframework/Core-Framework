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
        private readonly IList<PageWidget> _widgets;
        private readonly IList<Page> _children;
        private IList<PageLocale> _currentPageLocales = new List<PageLocale>();
        private IList<ILocale> _currentLocales = new List<ILocale>();
        private PageLocale _currentLocale;

        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class.
        /// </summary>
        public Page()
        {
            _widgets = new List<PageWidget>();
            _children = new List<Page>();
            PermissionTitle = "Pages";
            Operations = OperationsHelper.GetOperations<PageOperations>();
        }
       
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
                return _widgets;
            }
        }

        public virtual IEnumerable<Page> Children
        {
            get
            {
                return _children;
            }
        }

        public virtual PageLayout PageLayout { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public virtual PageSettings Settings { get; set; }

        /// <summary>
        /// Removes the widget.
        /// </summary>
        /// <param name="widget">The widget.</param>
        public virtual void RemoveWidget(PageWidget widget)
        {
            _widgets.Remove(widget);
        }

        public virtual void AddWidget(PageWidget widget)
        {
            _widgets.Add(widget);
        }

        #region IPermissible Members

        /// <summary>
        /// Gets or sets the permission title.
        /// </summary>
        /// <value>The permission title.</value>
        public virtual string PermissionTitle { get; set; }

        /// <summary>
        /// Gets or sets the object permission operations.
        /// </summary>
        /// <value>The object permission operations.</value>
        public virtual IEnumerable<IPermissionOperation> Operations { get; set; }

        public virtual IList<ILocale> CurrentLocales
        {
            get
            {
                if (_currentLocales.Count == 0 && _currentPageLocales.Count > 0)
                {
                    _currentLocales = _currentPageLocales.ToList().ConvertAll(mc => (ILocale)mc);
                }
                return _currentLocales;
            }
            set
            {
                _currentLocales = value;
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
                if (_currentLocale == null)
                {
                    _currentLocale = CultureHelper.GetCurrentLocale(CurrentLocales) as PageLocale;
                    if (_currentLocale == null)
                    {
                        _currentLocale = new PageLocale
                        {
                            Page = this,
                            Culture = null
                        };
                    }
                }
                return _currentLocale;
            }
        }

        #endregion
    }
}
