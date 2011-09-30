using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace Core.Framework.MEF.ServiceLocation
{
    /// <summary>
    /// Provides exports from a CSL-compatible service.
    /// </summary>
    public class CSLExportProvider : ExportProvider
    {
        #region Fields
        private IServiceLocator serviceLocator;
        private IDictionary<String, Type> contractMapping;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="CSLExportProvider" />.
        /// </summary>
        /// <param name="serviceLocator"></param>
        public CSLExportProvider(IServiceLocator serviceLocator)
        {
            Throw.Throw.IfArgumentNull(serviceLocator, "serviceLocator");

            this.serviceLocator = serviceLocator;
            this.contractMapping = new Dictionary<string, Type>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Registers a type with the export provider.
        /// </summary>
        /// <param name="type">The type to register.</param>
        public void RegisterType(Type type)
        {
            contractMapping.Add(AttributedModelServices.GetContractName(type), type);
        }

        /// <summary>
        /// Gets all exports for this provider.
        /// </summary>
        /// <param name="definition">The import definition to match.</param>
        /// <param name="atomicComposition">The atomic composition.</param>
        /// <returns>An enumerable of available exports.</returns>
        protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
        {
            if (definition.ContractName != null)
            {
                Type contractType;
                if (contractMapping.TryGetValue(definition.ContractName, out contractType))
                {
                    if (definition.Cardinality == ImportCardinality.ExactlyOne
                        || definition.Cardinality == ImportCardinality.ZeroOrOne)
                    {
                        var export = new Export(definition.ContractName, () => serviceLocator.GetInstance(contractType));
                        return new List<Export> { export };
                    }
                }
            }
            return Enumerable.Empty<Export>();
        }
        #endregion
    }
}
