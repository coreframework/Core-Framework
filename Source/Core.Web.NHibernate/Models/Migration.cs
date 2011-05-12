using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models
{
    public class Migration : Entity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public virtual long Version { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public virtual Plugin Plugin { get; set; }
    
        #endregion
    }
}
