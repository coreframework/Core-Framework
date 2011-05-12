using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models
{
    public class SchemaInfo : Entity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public virtual long Version { get; set; }
    
        #endregion
    }
}
