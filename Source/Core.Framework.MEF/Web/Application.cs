using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Core.Framework.MEF.Composition;
using Core.Framework.MEF.Contracts.Web;
using Core.Framework.MEF.Extensions;
using Core.Framework.Plugins.Modules;
using Core.Framework.Plugins.Web;

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

        [Import]
        private ImportControllerFactory controllerFactory;
#pragma warning restore 649

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the plugins.
        /// </summary>
        /// <value>The plugins.</value>
        public static IEnumerable<ICorePlugin> Plugins { get; set; }

        /// <summary>
        /// Gets or sets the action verbs.
        /// </summary>
        /// <value>The action verbs.</value>
        private static IEnumerable<Lazy<IActionVerb, IActionVerbMetadata>> ActionVerbs { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [modules changed].
        /// </summary>
        /// <value><c>true</c> if [modules changed]; otherwise, <c>false</c>.</value>
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
        /// Fired before the application is composed.
        /// </summary>
        protected virtual void PreCompose() { }

        /// <summary>
        /// Composes the application.
        /// </summary>
        protected virtual void Compose()
        {
            if (Composer.Instance == null)
                return;

            Composer.Instance.Compose(this);
            ActionVerbs = Composer.Instance.ResolveAll<IActionVerb, IActionVerbMetadata>();
            Plugins = Composer.Instance.ResolveAll<ICorePlugin>();
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
            if (routeRegistrars == null || !routeRegistrars.Any())
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
        protected static String MapPath(String virtualPath)
        {
            Throw.Throw.IfArgumentNullOrEmpty(virtualPath, "virtualPath");

            return HttpContext.Current.Server.MapPath(virtualPath);
        }

        /// <summary>
        /// Gets the available verbs for the given category.
        /// </summary>
        /// <param name="category">The category of verbs to get.</param>
        /// <returns>An enumerable of verbs.</returns>
        public static IEnumerable<IActionVerb> GetVerbsForCategory(String category)
        {
            Throw.Throw.IfArgumentNullOrEmpty(category, "category");

            return ActionVerbs
                .Where(l => l.Metadata.Category.Equals(category, StringComparison.InvariantCultureIgnoreCase))
                .Select(l => l.Value);
        }

        public static void RegisterHttpModule(Type moduleType)
        {
            if (!Composer.Instance.WindsorContainer.Kernel.HasComponent(GetHttpModuleComponentName(moduleType)))
            {
                Composer.Instance.WindsorContainer.Register(
                    Component.For<IPluginHttpModule>().ImplementedBy(moduleType).Named(
                        GetHttpModuleComponentName(moduleType)));
                ModulesChanged = true;
            }
        }

        public static void UnregisterHttpModule(Type moduleType)
        {
            //Composer.Instance.WindsorContainer.Kernel.RemoveComponent(GetHttpModuleComponentName(moduleType));
            ModulesChanged = true;
        }

        private static String GetHttpModuleComponentName(Type moduleType)
        {
            return String.Format("{0} / {1}", typeof(IHttpHandler).Name, moduleType.Name);
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