using System;
using System.Configuration;

namespace Core.Framework.MEF.Configuration
{
    /// <summary>
    /// Represents a configuration of a singular catalog.
    /// </summary>
    public class CatalogConfigurationElement : ConfigurationElement
    {
        #region Fields
        private const String NameAttribute = "name";
        private const String PathAttribute = "path";
        #endregion

        #region Methods
        /// <summary>
        /// Gets or sets the name of the catalog.
        /// </summary>
        [ConfigurationProperty(NameAttribute, IsRequired = true, IsKey = true)]
        public String Name
        {
            get { return (String)this[NameAttribute]; }
            set { this[NameAttribute] = value; }
        }

        /// <summary>
        /// Gets or sets the path of the catalog.
        /// </summary>
        [ConfigurationProperty(PathAttribute, IsRequired = true)]
        public String Path
        {
            get { return (String)this[PathAttribute]; }
            set { this[PathAttribute] = value; }
        }
        #endregion
    }
}