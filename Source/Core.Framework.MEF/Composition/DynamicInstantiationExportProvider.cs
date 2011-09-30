using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using System.Linq;

namespace Core.Framework.MEF.Composition
{
    /// <summary>
    /// Provides dynamic creation and instantiation of exports.
    /// </summary>
    public class DynamicInstantiationExportProvider : ExportProvider
    {
        #region Fields
        private static readonly Type partFactoryType = typeof(PartFactory<>);
        private static readonly IEnumerable<Export> emptyExports = Enumerable.Empty<Export>();
        private static readonly String partFactoryContractPrefix =
            partFactoryType.FullName.Substring(0, partFactoryType.FullName.IndexOf('`'));

        private readonly ConcurrentCache<ContractBasedImportDefinition, PartFactoryImport> definitionCache =
            new ConcurrentCache<ContractBasedImportDefinition, PartFactoryImport>();

        private ExportProvider exportProvider;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the source <see cref="ExportProvider" />
        /// </summary>
        public ExportProvider SourceProvider
        {
            get { return exportProvider; }
            set
            {
                if (value != exportProvider)
                {
                    if (exportProvider != null)
                    {
                        exportProvider.ExportsChanged -= SourceExportsChanged;
                        exportProvider.ExportsChanging -= SourceExportsChanging;
                    }

                    exportProvider = value;

                    if (exportProvider != null)
                    {
                        exportProvider.ExportsChanged += SourceExportsChanged;
                        exportProvider.ExportsChanging += SourceExportsChanging;
                    }
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the available set of exports for the given import definition.
        /// </summary>
        /// <param name="definition">The defintion of the import.</param>
        /// <param name="atomicComposition">The atomic composition of the import.</param>
        /// <returns>The available set of exports for the given import definition.</returns>
        protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
        {
            Throw.Throw.IfArgumentNull(definition, "definition");
            if (SourceProvider == null)
                Throw.Throw.InvalidOperation(
                    String.Format(
                        CultureInfo.CurrentUICulture,
                        Resources.Exceptions.PropertyCannotBeNull,
                        "Sourceprovider"));

            var contractDefinition = definition as ContractBasedImportDefinition;
            if (contractDefinition == null
                || !contractDefinition.RequiredTypeIdentity.StartsWith(partFactoryContractPrefix))
                return emptyExports;

            var info = definitionCache.Fetch(contractDefinition, () => new PartFactoryImport(contractDefinition));

            var exports = SourceProvider.GetExports(info.ImportDefinition, atomicComposition);
            var result = exports
                .Select(e => info.CreateMatchingExport(e.Definition, SourceProvider))
                .ToArray();

            foreach (var export in exports.OfType<IDisposable>())
                export.Dispose();

            return result;
        }
        /// <summary>
        /// Fired when exports are changing.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void SourceExportsChanging(object sender, ExportsChangeEventArgs e)
        {
            if (!SelfSent(e))
                return;

            OnExportsChanging(ProjectChangeEvent(e));
        }
        /// <summary>
        /// Fired when exports have changed..
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void SourceExportsChanged(object sender, ExportsChangeEventArgs e)
        {
            if (!SelfSent(e))
                return;

            OnExportsChanged(ProjectChangeEvent(e));
        }

        /// <summary>
        /// Creates a new instance of <see cref="TaggedExportsChangeEventArgs" /> for the given event arguments.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        /// <returns>A new instance of <see cref="TaggedExportsChangeEventArgs" /> for the given event arguments.</returns>
        private TaggedExportsChangeEventArgs ProjectChangeEvent(ExportsChangeEventArgs e)
        {
            var satisfiedImports = definitionCache.Values;

            return new TaggedExportsChangeEventArgs(
                this,
                ProjectExportsDefinitions(satisfiedImports, e.AddedExports),
                ProjectExportsDefinitions(satisfiedImports, e.RemovedExports),
                e.AtomicComposition);
        }

        /// <summary>
        /// Creates a set of export definitions from the given satisfied imports.
        /// </summary>
        /// <param name="satisfiedImports">The set of satisified imports.</param>
        /// <param name="changedProductExports">The set of changed exports.</param>
        /// <returns>A set of export definitions from the given satisfied imports.</returns>
        private IEnumerable<ExportDefinition> ProjectExportsDefinitions(IEnumerable<PartFactoryImport> satisfiedImports, IEnumerable<ExportDefinition> changedProductExports)
        {
            return from s in satisfiedImports
                   from e in changedProductExports
                   where s.ImportDefinition.IsConstraintSatisfiedBy(e)
                   select s.CreateMatchingExportDefinition(e);
        }

        /// <summary>
        /// Determines if the specified event arguments were sent by this instance.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        /// <returns>True if the specified event arguments were sent by this instance, otherwise false.</returns>
        private bool SelfSent(ExportsChangeEventArgs e)
        {
            return e is TaggedExportsChangeEventArgs
                    && ((TaggedExportsChangeEventArgs)e).Sender == this;
        }
        #endregion
    }
}
