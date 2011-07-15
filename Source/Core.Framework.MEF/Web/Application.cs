using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core.Framework.MEF.Composition;
using Core.Framework.MEF.Configuration;
using Core.Framework.MEF.Contracts.Web;
using Core.Framework.MEF.Extensions;
using Core.Framework.Plugins.Modules;
using Core.Framework.Plugins.Web;
using Microsoft.Practices.ServiceLocation;

namespace Core.Framework.MEF.Web
{
    /// <summary>
    /// Defines a modular application that supports imported types.
    /// </summary>
    public class Application : HttpApplication
    {
        #region Fields
#pragma warning disable 649
        [ImportMany]
        private IEnumerable<Lazy<IRouteRegistrar, IRouteRegistrarMetadata>> routeRegistrars;

        [ImportMany]
        private IEnumerable<Lazy<AreaRegistration, IRouteRegistrarMetadata>> routeAreaRegistrars;

        private static IEnumerable<Lazy<IActionVerb, IActionVerbMetadata>> actionVerbs;

        [Import]
        private ImportControllerFactory controllerFactory;
#pragma warning restore 649

        protected static WindsorContainer _container;

        #endregion

        #region Properties
        /// <summary>
        /// Gets the <see cref="Composer" /> used to compose parts.
        /// </summary>
        public static Composer Composer { get; private set; }

        /// <summary>
        /// Gets or sets the plugins.
        /// </summary>
        /// <value>The plugins.</value>
        public static IEnumerable<ICorePlugin> Plugins { get; set; }

        public static bool ModulesChanged { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// The start method of the application.
        /// </summary>
        protected void Application_Start()
        {
            // Perform any tasks required before composition.
            PreCompose();

            // Create the composer used for composition.
            Composer = CreateComposer();

            // Compose the application.
            Compose();

            // Set the controller factory.
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            // Set the view engine that supports imported areas.
            ViewEngines.Engines.Add(new AreaViewEngine());

            // Initialises the application.
            Initialise();

            //Register MVC areas.
            RegisterAreas();

            // Register MVC routes.
            RegisterRoutes();

            StartPlugins();
        }

        /// <summary>
        /// Initialises the application.
        /// </summary>
        protected virtual void Initialise() { }

        /// <summary>
        /// Creates a <see cref="Composer" /> used to compose parts.
        /// </summary>
        /// <returns></returns>
        protected virtual Composer CreateComposer()
        {
            var composer = new Composer();

            GetDirectoryCatalogs()
                .ForEach(composer.AddCatalog);

            composer.AddExportProvider(
                new DynamicInstantiationExportProvider(),
                (provider, container) => ((DynamicInstantiationExportProvider)provider).SourceProvider = container);

            return composer;
        }

        /// <summary>
        /// Gets an list of <see cref="DirectoryCatalog" />s of configured directories.
        /// </summary>
        /// <returns>An lsit of <see cref="DirectoryCatalog" />s of configured directories.</returns>
        private List<DirectoryCatalog> GetDirectoryCatalogs()
        {
            var list = new List<DirectoryCatalog>();

            GetDirectoryCatalogs(MapPath("~/bin")).ForEach(catalog => list.Add(catalog));

            GetDirectoryCatalogs(MapPath("~/Areas")).ForEach(catalog =>
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
                        if (!string.IsNullOrEmpty(c.Path))
                        {
                            string path = c.Path;
                            if (path.StartsWith("~") || path.StartsWith("/"))
                                path = MapPath(path);

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
        /// Registers the specified path for probing.
        /// </summary>
        /// <param name="path">The probable path.</param>
        private void RegisterPath(string path)
        {
#pragma warning disable 612,618
            AppDomain.CurrentDomain.AppendPrivatePath(path);
#pragma warning restore 612,618
        }

        /// <summary>
        /// Fired before the application is composed.
        /// </summary>
        protected virtual void PreCompose() { }

        /// <summary>
        /// Composes the application.
        /// </summary>
        protected virtual void Compose()
        {
            if (Composer == null)
                return;

            Composer.Compose(this);
            actionVerbs = Composer.ResolveAll<IActionVerb, IActionVerbMetadata>();
            Plugins = Composer.ResolveAll<ICorePlugin>();
        }

        protected virtual void RegisterAreas()
        {
            var routes = RouteTable.Routes;

            var areaRegistrars = routeAreaRegistrars.Select(lazy => lazy.Value);
            areaRegistrars.ForEach(r => r.RegisterArea(new AreaRegistrationContext(r.AreaName, routes)));
        }

        /// <summary>
        /// Registers any routes required by the application.
        /// </summary>
        protected virtual void RegisterRoutes()
        {
            if (routeRegistrars == null || routeRegistrars.Count() == 0)
                return;

            var routes = RouteTable.Routes;

            var registrars = routeRegistrars
                .OrderBy(lazy => lazy.Metadata.Order)
                .Select(lazy => lazy.Value);

            registrars.ForEach(r => r.RegisterIgnoreRoutes(routes));
            registrars.ForEach(r => r.RegisterRoutes(routes));
        }

        /// <summary>
        /// Maps the specified virtual path.
        /// </summary>
        /// <param name="virtualPath">The virtual path to map.</param>
        /// <returns>The specified virtual path as a mapped path.</returns>
        protected string MapPath(string virtualPath)
        {
            Throw.Throw.IfArgumentNullOrEmpty(virtualPath, "virtualPath");

            return HttpContext.Current.Server.MapPath(virtualPath);
        }

        /// <summary>
        /// Gets a set of <see cref="DirectoryCatalog" /> for the specified path and it's immediate child directories.
        /// </summary>
        /// <param name="path">The starting path.</param>
        /// <returns>An <see cref="IEnumerable{DirectoryCatalog}" /> of directory catalogs.</returns>
        protected IEnumerable<DirectoryCatalog> GetDirectoryCatalogs(string path)
        {
            Throw.Throw.IfArgumentNullOrEmpty(path, "path");

            List<DirectoryCatalog> list = new List<DirectoryCatalog>();
            list.Add(new DirectoryCatalog(path));

            list.AddRange(
                Directory.GetDirectories(path).Select(directory => new DirectoryCatalog(directory)));

            return list;
        }

        /// <summary>
        /// Gets the available verbs for the given category.
        /// </summary>
        /// <param name="category">The category of verbs to get.</param>
        /// <returns>An enumerable of verbs.</returns>
        public static IEnumerable<IActionVerb> GetVerbsForCategory(string category)
        {
            Throw.Throw.IfArgumentNullOrEmpty(category, "category");

            return actionVerbs
                .Where(l => l.Metadata.Category.Equals(category, StringComparison.InvariantCultureIgnoreCase))
                .Select(l => l.Value);
        }

        public static void RegisterHttpModule(Type moduleType)
        {
            if (!_container.Kernel.HasComponent(GetHttpModuleComponentName(moduleType)))
            {
                _container.Register(
                    Component.For<IPluginHttpModule>().ImplementedBy(moduleType).Named(
                        GetHttpModuleComponentName(moduleType)));
                ModulesChanged = true;
            }
        }

        public static void UnregisterHttpModule(Type moduleType)
        {
            _container.Kernel.RemoveComponent(GetHttpModuleComponentName(moduleType));
            ModulesChanged = true;
        }

        private static String GetHttpModuleComponentName(Type moduleType)
        {
            return String.Format("{0} / {1}", typeof (IHttpHandler).Name, moduleType.Name);
        }

        public static void StartPlugins()
        {
            foreach (var corePlugin in Plugins)
            {
                corePlugin.Start();
            }
        }

        #endregion
    }
}