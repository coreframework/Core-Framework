using System;
using System.Linq;
using Core.Web.NHibernate.Models;
using Framework.Core.DomainModel;

namespace Core.Web.Areas.Admin.Models
{
    public class PluginViewModel : IMappedModel<Plugin, PluginViewModel>
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public String Title { get; set; }

        #region IMappedModel members

        public PluginViewModel MapFrom(Plugin from)
        {
            Id = from.Id;
            if (MvcApplication.Plugins != null)
            {
                Title = MvcApplication.Plugins.FirstOrDefault(plugin => plugin.Identifier == from.Identifier).Title;    
            }
            return this;
        }

        public Plugin MapTo(Plugin to)
        {
            return to;
        }

        #endregion

    }
}