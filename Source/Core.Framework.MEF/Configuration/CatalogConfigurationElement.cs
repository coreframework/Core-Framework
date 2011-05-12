using System.Configuration;

namespace Core.Framework.MEF.Configuration
{
    /// <summary>
    /// Represents a configuration of a singular catalog.
    /// </summary>
    public class CatalogConfigurationElement : ConfigurationElement
    {
        #region Fields
        private const string NameAttribute = "name";
        private const string PathAttribute = "path";
        #endregion

        #region Methods
        /// <summary>
        /// Gets or sets the name of the catalog.
        /// </summary>
        [ConfigurationProperty(NameAttribute, IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this[NameAttribute]; }
            set { this[NameAttribute] = value; }
        }

        /// <summary>
        /// Gets or sets the path of the catalog.
        /// </summary>
        [ConfigurationProperty(PathAttribute, IsRequired = true)]
        public string Path
        {
            get { return (string)this[PathAttribute]; }
            set { this[PathAttribute] = value; }
        }
        #endregion
    }
}