using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using Core.Framework.MEF.Configuration;
using Core.Framework.MEF.Extensions;
using Core.Framework.MEF.ServiceLocation;

namespace Core.Framework.MEF.Composition
{
    /// <summary>
    /// Provides a common mechanism for composing parts.
    /// </summary>
    public class Composer
    {
        #region Fields

        private bool modified;

        private readonly IDictionary<ExportProvider, Action<ExportProvider, CompositionContainer>> postContainerModifiers =
                new Dictionary<ExportProvider, Action<ExportProvider, CompositionContainer>>();

        #endregion

        #region Properties
        
        public WindsorContainer WindsorContainer { get; private set; }

        /// <summary>
        /// Gets the catalog to use for composition.
        /// </summary>
        public ComposablePartCatalog Catalog { get; private set; }

        /// <summary>
        /// Gets the container to use for composition.
        /// </summary>
        public CompositionContainer Container { get; private set; }

        /// <summary>
        /// Gets the export provider to use for composition.
        /// </summary>
        public IList<ExportProvider> ExportProviders { get; private set; }

        #endregion

        #region Singleton

        private static readonly Lazy<Composer> instance = new Lazy<Composer>(() => new Composer());

        public static Composer Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private Composer()
        {
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            ExportProviders = new List<ExportProvider>();
            GetDirectoryCatalogs()
                .ForEach(AddCatalog);

            AddExportProvider(
                new DynamicInstantiationExportProvider(),
                (provider, container) => ((DynamicInstantiationExportProvider)provider).SourceProvider = container);

            WindsorContainer = new WindsorContainer();
            AddExportProvider(new CslExportProvider(new WindsorServiceLocator(WindsorContainer)));

            //Compose(this);
        }

        /// <summary>
        /// Adds the specified catalog to the composer.
        /// </summary>
        /// <param name="catalog">The catalog to add to the composer.</param>
        public void AddCatalog(ComposablePartCatalog catalog)
        {
            if (catalog == null)
                throw new ArgumentNullException("catalog");

            if (Catalog == null)
                Catalog = catalog;
            else
            {
                var aggregate = Catalog as AggregateCatalog;
                if (aggregate != null)
                    aggregate.Catalogs.Add(catalog);
                else
                {
                    aggregate = new AggregateCatalog();
                    aggregate.Catalogs.Add(Catalog);
                    aggregate.Catalogs.Add(catalog);

                    Catalog = aggregate;
                }
            }
            modified = true;
        }

        /// <summary>
        /// Adds the specific export provider to the composer.
        /// </summary>
        /// <param name="provider">The export provider add to the composer.</param>
        /// <param name="postContainerModifier">A modifier action called after the container has been created.</param>
        public void AddExportProvider(ExportProvider provider, Action<ExportProvider, CompositionContainer> postContainerModifier = null)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            ExportProviders.Add(provider);
            if (postContainerModifier != null)
                postContainerModifiers.Add(provider, postContainerModifier);
        }

        /// <summary>
        /// Composes the specified object.
        /// </summary>
        /// <param name="object">The object to be composed.</param>
        public void Compose(object @object)
        {
            if (@object == null)
                throw new ArgumentNullException("object");

            if (Catalog == null)
                return;

            EnsureContainer();
            try
            {
                Container.ComposeParts(@object);
            }
            catch (ReflectionTypeLoadException e)
            {
                var builder = new StringBuilder(e.Message);
                foreach (var loaderException in e.LoaderExceptions)
                {
                    builder.AppendLine(loaderException.Message);
                }

                throw new Exception(builder.ToString());
            }
        }

        /// <summary>
        /// Gets an instance of the specified type from the <see cref="Composer" />.
        /// </summary>
        /// <typeparam name="T">The type of instance to resolve.</typeparam>
        /// <returns>The resolved instance.</returns>
        public T Resolve<T>()
        {
            return Resolve<T>(null);
        }

        /// <summary>
        /// Gets an instance of the specified type from the <see cref="Composer" />.
        /// </summary>
        /// <typeparam name="T">The type of instance to resolve.</typeparam>
        /// <param name="contractName">The contract name the type was exported with.</param>
        /// <returns>The resolved instance.</returns>
        public T Resolve<T>(String contractName)
        {
            if (Catalog == null)
                return default(T);

            EnsureContainer();
            return String.IsNullOrEmpty(contractName)
                       ? Container.GetExportedValue<T>()
                       : Container.GetExportedValue<T>(contractName);
        }

        /// <summary>
        /// Gets an instance of <see cref="Lazy{T,TMetadata}" /> for the specified trip from the <see cref="Composer" />.
        /// </summary>
        /// <typeparam name="T">The type of instance to resolve.</typeparam>
        /// <typeparam name="TMetadata">The metadata type to resolve.</typeparam>
        /// <returns>A <see cref="Lazy{T,TMetadata}" /> that allows lazy-loading.</returns>
        public Lazy<T, TMetadata> Resolve<T, TMetadata>()
        {
            return Resolve<T, TMetadata>(null);
        }

        /// <summary>
        /// Gets an instance of <see cref="Lazy{T,TMetadata}" /> for the specified trip from the <see cref="Composer" />.
        /// </summary>
        /// <typeparam name="T">The type of instance to resolve.</typeparam>
        /// <typeparam name="TMetadata">The metadata type to resolve.</typeparam>
        /// <param name="contractName">The contract name the type was exported with.</param>
        /// <returns>A <see cref="Lazy{T,TMetadata}" /> that allows lazy-loading.</returns>
        public Lazy<T, TMetadata> Resolve<T, TMetadata>(String contractName)
        {
            if (Catalog == null)
                return null;

            EnsureContainer();
            return String.IsNullOrEmpty(contractName)
                       ? Container.GetExport<T, TMetadata>()
                       : Container.GetExport<T, TMetadata>(contractName);
        }

        /// <summary>
        /// Gets all instances of the specified type from the <see cref="Composer" />.
        /// </summary>
        /// <typeparam name="T">The type of instance to resolve.</typeparam>
        /// <returns>An enumerable of resolved instances.</returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            return ResolveAll<T>(null);
        }

        /// <summary>
        /// Gets all instances of the specified type from the <see cref="Composer" />.
        /// </summary>
        /// <typeparam name="T">The type of instance to resolve.</typeparam>
        /// <param name="contractName">The contract name the type was exported with.</param>
        /// <returns>An enumerable of resolved instances.</returns>
        public IEnumerable<T> ResolveAll<T>(String contractName)
        {
            if (Catalog == null)
                return new T[0];

            EnsureContainer();
            return String.IsNullOrEmpty(contractName)
                       ? Container.GetExportedValues<T>()
                       : Container.GetExportedValues<T>(contractName);
        }

        /// <summary>
        /// Gets all instances of <see cref="Lazy{T,TMetadata}" /> of the specified type from the <see cref="Composer" />.
        /// </summary>
        /// <typeparam name="T">The type of instance to resolve.</typeparam>
        /// <typeparam name="TMetadata">The metadata type to resolve.</typeparam>
        /// <returns>An enumerable of <see cref="Lazy{T,TMetadata}" />.</returns>
        public IEnumerable<Lazy<T, TMetadata>> ResolveAll<T, TMetadata>()
        {
            return ResolveAll<T, TMetadata>(null);
        }

        /// <summary>
        /// Gets all instances of <see cref="Lazy{T,TMetadata}" /> of the specified type from the <see cref="Composer" />.
        /// </summary>
        /// <typeparam name="T">The type of instance to resolve.</typeparam>
        /// <typeparam name="TMetadata">The metadata type to resolve.</typeparam>
        /// <param name="contractName">The contract name the type was exported with.</param>
        /// <returns>An enumerable of <see cref="Lazy{T,TMetadata}" />.</returns>
        public IEnumerable<Lazy<T, TMetadata>> ResolveAll<T, TMetadata>(String contractName)
        {
            if (Catalog == null)
                return null;

            EnsureContainer();
            return String.IsNullOrEmpty(contractName)
                       ? Container.GetExports<T, TMetadata>()
                       : Container.GetExports<T, TMetadata>(contractName);
        }

        /// <summary>
        /// Ensures the Container has been instantiated/re-instantiated if the Composer has been modified.
        /// </summary>
        private void EnsureContainer()
        {
            if (modified || Container == null)
            {
                if (Container != null)
                    Container.Dispose();

                Container = new CompositionContainer(Catalog, ExportProviders.ToArray());

                foreach (var provider in postContainerModifiers.Keys)
                    postContainerModifiers[provider](provider, Container);

                Container.ComposeExportedValue(this);

                modified = false;
            }
        }

        /// <summary>
        /// Gets an list of <see cref="DirectoryCatalog" />s of configured directories.
        /// </summary>
        /// <returns>An lsit of <see cref="DirectoryCatalog" />s of configured directories.</returns>
        private static List<DirectoryCatalog> GetDirectoryCatalogs()
        {
            var list = new List<DirectoryCatalog>();

            GetDirectoryCatalogs(HttpContext.Current.Server.MapPath("~/bin")).ForEach(list.Add);

            GetDirectoryCatalogs(HttpContext.Current.Server.MapPath("~/Areas")).ForEach(catalog =>
            {
                list.Add(catalog);
                RegisterPath(catalog.FullPath);
            });

            var config = CompositionConfigurationSection.GetInstance();
            if (config != null && config.Catalogs != null)
            {
                config.Catalogs
                    .Cast<CatalogConfigurationElement>()
                    .ForEach(c =>
                    {
                        if (!String.IsNullOrEmpty(c.Path))
                        {
                            String path = c.Path;
                            if (path.StartsWith("~") || path.StartsWith("/"))
                                path = HttpContext.Current.Server.MapPath(path);

                            GetDirectoryCatalogs(path)
                                .ForEach(catalog =>
                                {
                                    list.Add(catalog);
                                    RegisterPath(catalog.FullPath);
                                });
                        }
                    });
            }

            return list;
        }

        /// <summary>
        /// Gets a set of <see cref="DirectoryCatalog" /> for the specified path and it's immediate child directories.
        /// </summary>
        /// <param name="path">The starting path.</param>
        /// <returns>An <see cref="IEnumerable{DirectoryCatalog}" /> of directory catalogs.</returns>
        protected static IEnumerable<DirectoryCatalog> GetDirectoryCatalogs(String path)
        {
            Throw.Throw.IfArgumentNullOrEmpty(path, "path");

            var list = new List<DirectoryCatalog>
                           {
                               new DirectoryCatalog(path)
                           };

            list.AddRange(
                Directory.GetDirectories(path).Select(directory => new DirectoryCatalog(directory)));

            return list;
        }

        /// <summary>
        /// Registers the specified path for probing.
        /// </summary>
        /// <param name="path">The probable path.</param>
        private static void RegisterPath(String path)
        {
#pragma warning disable 612,618
            AppDomain.CurrentDomain.AppendPrivatePath(path);
#pragma warning restore 612,618
        }

        #endregion
    }
}