using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Core.Framework.MEF.Composition
{
    /// <summary>
    /// Defines an import for a part factory.
    /// </summary>
    internal class PartFactoryImport
    {
        #region Fields

        private const String SetterPrefix = "set_";
        private static readonly Type partFactoryImportType = typeof(PartFactoryImport);
        private static readonly MethodInfo createPartFactoryMethod =
            partFactoryImportType.GetMethod("CreatePartCreatorOfT", BindingFlags.Static | BindingFlags.NonPublic);
        private static readonly MethodInfo createPartFactoryMetaMethod =
            partFactoryImportType.GetMethod("CreatePartCreatorOfTWithMetadata", BindingFlags.Static | BindingFlags.NonPublic);

        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="PartFactoryImport" />.
        /// </summary>
        /// <param name="definition">The import defintion.</param>
        public PartFactoryImport(ContractBasedImportDefinition definition)
        {
            Throw.Throw.IfArgumentNull(definition, "definition");

            ContractName = definition.ContractName;
            PartFactoryType = ResolvePartFactoryType(definition);
            ExportedValueType = PartFactoryType.GetGenericArguments()[0];
            MetadataViewType = null;
            if (PartFactoryType.GetGenericArguments().Length == 2)
                MetadataViewType = PartFactoryType.GetGenericArguments()[1];

            ImportDefinition = GetImportDefinition(definition, ExportedValueType, MetadataViewType);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the contract name.
        /// </summary>
        public String ContractName { get; private set; }

        /// <summary>
        /// Gets the type of part factory.
        /// </summary>
        public Type PartFactoryType { get; private set; }

        /// <summary>
        /// Gets the type of the exported value.
        /// </summary>
        public Type ExportedValueType { get; private set; }

        /// <summary>
        /// Gets the type of the metadata view.
        /// </summary>
        public Type MetadataViewType { get; private set; }

        /// <summary>
        /// Gets the import definition.
        /// </summary>
        public ContractBasedImportDefinition ImportDefinition { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new instance of <see cref="ExportDefinition" /> as a clone of the specified definition.
        /// </summary>
        /// <param name="definition">The export definition to clone.</param>
        /// <returns>A new instance of <see cref="ExportDefinition" /> as a clone of the specified definition.</returns>
        public ExportDefinition CreateMatchingExportDefinition(ExportDefinition definition)
        {
            return new ExportDefinition(ContractName, GetPartFactoryExportMetadata(definition.Metadata));
        }

        /// <summary>
        /// Returns a new collection of metadata.
        /// </summary>
        /// <param name="metadata">The original metadata collection.</param>
        /// <returns>A new collection of metadata.</returns>
        private IDictionary<String, object> GetPartFactoryExportMetadata(IDictionary<string, object> metadata)
        {
            var result = new Dictionary<String, object>(metadata);
            result[CompositionConstants.ExportTypeIdentityMetadataName] =
                AttributedModelServices.GetTypeIdentity(PartFactoryType);
            return result;
        }

        /// <summary>
        /// Gets a new instance of <see cref="ContractBasedImportDefinition" /> for the part definition.
        /// </summary>
        /// <param name="definition">The source definition.</param>
        /// <param name="createdType">The type created.</param>
        /// <param name="metadataViewType">The metadata view type.</param>
        /// <returns>A new instance of <see cref="ContractBasedImportDefinition" /> for the part definition.</returns>
        private static ContractBasedImportDefinition GetImportDefinition(ContractBasedImportDefinition definition, Type createdType, Type metadataViewType)
        {
            String identity = AttributedModelServices.GetTypeIdentity(createdType);
            var contractName = definition.ContractName == definition.RequiredTypeIdentity
                                   ? identity
                                   : definition.ContractName;

            return new ContractBasedImportDefinition(
                contractName,
                identity,
                GetRequiredMetadataKeys(definition.RequiredMetadata, metadataViewType),
                definition.Cardinality,
                definition.IsRecomposable,
                definition.IsPrerequisite,
                CreationPolicy.NonShared);
        }

        /// <summary>
        /// Resolves the required part factory type for the given definition.
        /// </summary>
        /// <param name="definition">The import definition.</param>
        /// <returns>The resolved part factory type.</returns>
        /// <exception cref="NotSupportedException">If the import type is not supported.</exception>
        private static Type ResolvePartFactoryType(ContractBasedImportDefinition definition)
        {
            Type importType;

            var member = ReflectionModelServices.GetImportingMember(definition);
            var setter = member.GetAccessors()
                    .Where(a => a.Name.StartsWith(SetterPrefix))
                    .OfType<MethodInfo>()
                    .SingleOrDefault();

            if (setter != null)
                importType = setter.GetParameters()
                    .First()
                    .ParameterType;
            else
                importType = member.GetAccessors()
                    .OfType<FieldInfo>()
                    .Single()
                    .FieldType;

            if (definition.Cardinality == ImportCardinality.ZeroOrMore)
                importType = importType.IsGenericType
                                 ? importType.GetGenericArguments()[0]
                                 : importType.GetElementType();

            if (!(importType.IsGenericType
                && (importType.GetGenericTypeDefinition() == typeof(PartFactory<>)
                    || importType.GetGenericTypeDefinition() == typeof(PartFactory<,>))))
            {

                Throw.Throw.NotSupported(String.Format(CultureInfo.CurrentUICulture,
                                                 Resources.Exceptions.ImportTypeNotSupported, importType.FullName));
            }

            return importType;
        }

        /// <summary>
        /// Gets the set of required meta data keys.
        /// </summary>
        /// <param name="standardRequiredKeys">The standard keys for the source definition.</param>
        /// <param name="metadataViewType">The metadata view type.</param>
        /// <returns>The set of required meta data keys.</returns>
        private static IEnumerable<KeyValuePair<String, Type>> GetRequiredMetadataKeys(IEnumerable<KeyValuePair<String, Type>> standardRequiredKeys, Type metadataViewType)
        {
            var result = standardRequiredKeys;

            if (metadataViewType != null)
                result = result.Union(
                    metadataViewType
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty)
                        .Where(pi => pi.GetCustomAttributes(typeof(DefaultValueAttribute), false).Length == 0)
                        .Select(pi => new KeyValuePair<String, Type>(pi.Name, pi.PropertyType)));

            return result;
        }

        /// <summary>
        /// Creates a matching export for the given definition.
        /// </summary>
        /// <param name="definition">The definition to create an export for.</param>
        /// <param name="provider">The export provider.</param>
        /// <returns>A matching export for the given definition.</returns>
        public Export CreateMatchingExport(ExportDefinition definition, ExportProvider provider)
        {
            return new Export(
                    CreateMatchingExportDefinition(definition),
                    () => CreatePartFactory(ExportedValueType, MetadataViewType, ImportDefinition, provider, definition));
        }

        /// <summary>
        /// Creates a part factory for the specified type.
        /// </summary>
        /// <param name="createdType">The created type.</param>
        /// <param name="metadataViewType">The metadata view type.</param>
        /// <param name="partDefinition">The part definition.</param>
        /// <param name="provider">The export provider.</param>
        /// <param name="definition">The import definition.</param>
        /// <returns>A part factory for the specified type.</returns>
        private static object CreatePartFactory(Type createdType, Type metadataViewType, ContractBasedImportDefinition partDefinition, ExportProvider provider, ExportDefinition definition)
        {
            if (metadataViewType == null)
                return createPartFactoryMethod.MakeGenericMethod(createdType)
                    .Invoke(null, new object[] {
                        partDefinition, provider, definition
                    });

            return createPartFactoryMetaMethod.MakeGenericMethod(createdType, metadataViewType)
                .Invoke(null, new object[] {
                        partDefinition, provider, definition
                    });
        }

        /// <summary>
        /// Creates a part factory with metadata.
        /// </summary>
        /// <typeparam name="TPart">The type of part.</typeparam>
        /// <typeparam name="TMetadata">The type of metadata.</typeparam>
        /// <param name="importDefinition">The import definition.</param>
        /// <param name="provider">The export provider.</param>
        /// <param name="exportDefinition">The export definition.</param>
        /// <returns>A part factory with metadata.</returns>
        protected static PartFactory<TPart> CreatePartCreatorOfTWithMetadata<TPart, TMetadata>(ImportDefinition importDefinition, ExportProvider provider, ExportDefinition exportDefinition)
        {
            Func<PartLifetimeContext<TPart>> creator = CreatePartlifeTimeContext<TPart>(importDefinition, provider, exportDefinition);
            return new PartFactory<TPart, TMetadata>(creator, AttributedModelServices.GetMetadataView<TMetadata>(exportDefinition.Metadata));
        }

        /// <summary>
        /// Creates a part factory with metadata.
        /// </summary>
        /// <typeparam name="TPart">The type of part.</typeparam>
        /// <param name="importDefinition">The import definition.</param>
        /// <param name="provider">The export provider.</param>
        /// <param name="exportDefinition">The export definition.</param>
        /// <returns>A part factory with metadata.</returns>
        protected static PartFactory<TPart> CreatePartCreatorOfT<TPart>(ImportDefinition importDefinition, ExportProvider provider, ExportDefinition exportDefinition)
        {
            Func<PartLifetimeContext<TPart>> creator = CreatePartlifeTimeContext<TPart>(importDefinition, provider, exportDefinition);
            return new PartFactory<TPart>(creator);
        }

        /// <summary>
        /// Creates a part lifetime context for the given import definition.
        /// </summary>
        /// <typeparam name="TPart">The type of part to create.</typeparam>
        /// <param name="importDefinition">The import definition</param>
        /// <param name="provider">The export provider.</param>
        /// <param name="exportDefinition">The export definition.</param>
        /// <returns>A part lifetime context for the given import definition.</returns>
        protected static Func<PartLifetimeContext<TPart>> CreatePartlifeTimeContext<TPart>(ImportDefinition importDefinition, ExportProvider provider, ExportDefinition exportDefinition)
        {
            Func<PartLifetimeContext<TPart>> creator = () =>
            {
                var part = provider.GetExports(importDefinition)
                    .Single(e => e.Definition == exportDefinition);

                return new PartLifetimeContext<TPart>((TPart)part.Value, () =>
                {
                    if (part is IDisposable)
                        ((IDisposable)part).Dispose();
                });
            };

            return creator;
        }
        #endregion
    }
}
