using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.Web.NHibernate.Models
{
    [Export(typeof(IPermissible))]
    public class Plugin : Entity, IPermissible, ILocalizable
    {
        #region Fields

        private readonly IList<PluginLocale> currentPluginLocales = new List<PluginLocale>();
        private IList<ILocale> currentLocales = new List<ILocale>();
        private PluginLocale currentLocale;

        #endregion

        public Plugin()
        {
            PermissionTitle = "Modules";
            Operations = OperationsHelper.GetOperations<BaseEntityOperations>();
        }

        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public virtual String Identifier { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Plugin"/> is status.
        /// </summary>
        /// <value><c>true</c> if status; otherwise, <c>false</c>.</value>
        public virtual PluginStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>The create date.</value>
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public virtual String Version { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title
        {
            get
            {
                return ((PluginLocale)CurrentLocale).Title;
            }
            set { ((PluginLocale)CurrentLocale).Title = value; }
        }

        public virtual PluginLocale Locale { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual String Description
        {
            get
            {
                return ((PluginLocale)CurrentLocale).Description;
            }
            set { ((PluginLocale)CurrentLocale).Description = value; }
        }

        public virtual IList<ILocale> CurrentLocales
        {
            get
            {
                if (currentLocales.Count == 0 && currentPluginLocales.Count > 0)
                {
                    currentLocales = currentPluginLocales.ToList().ConvertAll(mc => (ILocale)mc);
                }
                return currentLocales;
            }
            set
            {
                currentLocales = value;
            }
        }

        public virtual IList<PluginLocale> CurrentPluginLocales
        {
            get
            {
                return CurrentLocales.ToList().ConvertAll(mc => (PluginLocale)mc);
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
                    currentLocale = CultureHelper.GetCurrentLocale(CurrentLocales) as PluginLocale;
                    if (currentLocale == null)
                    {
                        currentLocale = new PluginLocale
                        {
                            Plugin = this,
                            Culture = null
                        };
                    }
                }
                return currentLocale;
            }
        }

        #endregion

        #region IPermissible Members

        /// <summary>
        /// Gets or sets the permission title.
        /// </summary>
        /// <value>The permission title.</value>
        public virtual String PermissionTitle { get; set; }

        /// <summary>
        /// Gets or sets the permission groups.
        /// </summary>
        /// <value>The permission groups.</value>
        public virtual IEnumerable<IPermissionOperation> Operations { get; set; }

        #endregion
    }
}
