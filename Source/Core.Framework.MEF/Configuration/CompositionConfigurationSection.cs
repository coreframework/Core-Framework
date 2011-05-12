using System.Configuration;

namespace Core.Framework.MEF.Configuration
{
    /// <summary>
    /// Represets the composition configuration.
    /// </summary>
    public class CompositionConfigurationSection : ConfigurationSection
    {
        #region Fields
        private const string SectionPath = "mef/composition";
        private const string CatalogsElement = "catalogs";
        #endregion

        #region Properties
        /// <summary>
        /// Gets the collection of catalog configurations.
        /// </summary>
        [ConfigurationProperty(CatalogsElement, IsDefaultCollection = true)]
        public CatalogConfigurationElementCollection Catalogs
        {
            get { return (CatalogConfigurationElementCollection)this[CatalogsElement]; }
            set { this[CatalogsElement] = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets an instance of <see cref="CompositionConfigurationSection" /> that represents the current configuration.
        /// </summary>
        /// <returns>An instance of <see cref="CompositionConfigurationSection" />, or null.</returns>
        public static CompositionConfigurationSection GetInstance()
        {
            return ConfigurationManager.GetSection(SectionPath) as CompositionConfigurationSection;
        }
        #endregion
    }
}