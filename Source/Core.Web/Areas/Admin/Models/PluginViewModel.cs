using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Plugins.Plugins;
using Core.Web.NHibernate.Models;
using Framework.Core.DomainModel;
using Framework.Core.Localization;
using Microsoft.Practices.ServiceLocation;
using System.Linq;

namespace Core.Web.Areas.Admin.Models
{
    public class PluginViewModel : IMappedModel<Plugin, PluginViewModel>
    {
        public IDictionary<String, String> Cultures { get; set; }
        public String SelectedCulture { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [AllowHtml]
        public String Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [AllowHtml]
        public String Description { get; set; }

        /// <summary>
        /// Gets or sets the missing dependencies.
        /// </summary>
        /// <value>The missing dependencies.</value>
        public IList<PluginDependency> MissingDependencies { get; set; }

        #region IMappedModel members

        public PluginViewModel MapFrom(Plugin from)
        {
            Id = from.Id;
            Title = from.Title;
            Description = from.Description;
            Cultures = CultureHelper.GetAvailableCultures();
            SelectedCulture = from.CurrentLocale.Culture;
            MissingDependencies = ServiceLocator.Current.GetInstance<IPluginHelper>().GetMissingPlugins(
                MvcApplication.Plugins.Where(plugin => plugin.Identifier.Equals(from.Identifier)).FirstOrDefault());

            return this;
        }

        public Plugin MapTo(Plugin to)
        {
            to.Title = Title;
            to.Description = Description;

            return to;
        }

        #endregion

    }
}