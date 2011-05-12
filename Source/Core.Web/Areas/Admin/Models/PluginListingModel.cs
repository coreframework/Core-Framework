using System;
using Core.Web.NHibernate.Models;
using Framework.Core.DomainModel;

namespace Core.Web.Areas.Admin.Models
{
    public class PluginListModel : IMappedModel<Plugin, PluginListModel>
    {
        #region Properties

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

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public String Description { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public String Identifier { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public PluginStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>The create date.</value>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public String Version { get; set; }

        #endregion

        #region IMappedModel members

        public PluginListModel MapFrom(Plugin from)
        {
            Id = from.Id;
            Status = from.Status;
            Version = from.Version;
            CreateDate = from.CreateDate;
            Identifier = from.Identifier;

            return this;
        }

        public Plugin MapTo(Plugin to)
        {
            return to;
        }

        #endregion

    }
}