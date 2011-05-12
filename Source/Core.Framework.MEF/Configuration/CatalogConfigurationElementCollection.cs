using System.Configuration;

namespace Core.Framework.MEF.Configuration
{
    /// <summary>
    /// Represents a collection of <see cref="CatalogConfigurationElement" /> items.
    /// </summary>
    [ConfigurationCollection(typeof(CatalogConfigurationElement), AddItemName = "catalog")]
    public class CatalogConfigurationElementCollection : ConfigurationElementCollection
    {
        #region Methods
        /// <summary>
        /// Gets a unique key for the given element.
        /// </summary>
        /// <param name="element">The element to get a key for.</param>
        /// <returns>A unique key for the given element.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CatalogConfigurationElement)element).Name;
        }

        /// <summary>
        /// Creates a new instance of <see cref="CatalogConfigurationElement" /> for use with this collection.
        /// </summary>
        /// <returns>A new instance of <see cref="CatalogConfigurationElement" />.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new CatalogConfigurationElement();
        }
        #endregion
    }
}
