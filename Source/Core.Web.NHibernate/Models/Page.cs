using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Permissions.Operations;
using Framework.Core.Localization;
using Framework.Facilities.NHibernate.Objects;

namespace Core.Web.NHibernate.Models
{
    [Export(typeof(IPermissible))]
    public class Page : LocalizableEntity<PageLocale>, IPermissible
    {
        #region Fields

        private readonly IList<PageWidget> widgets = new List<PageWidget>();
        private readonly IList<Page> children = new List<Page>();

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
        /// Gets or sets a value indicating whether this instance is template.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is template; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsTemplate { get; set; }

        /// <summary>
        /// Gets or sets the place holders count.
        /// </summary>
        /// <value>The place holders count.</value>
        public virtual int PlaceHoldersCount { get; set; }

        public virtual Page Template { get; set; }

        public virtual int InheritedPagesCount { get; set; }

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

        #endregion

        public override ILocale InitializeLocaleEntity()
        {
            return new PageLocale
                       {
                           Page = this,
                           Culture = null
                       };
        }
    }
}
