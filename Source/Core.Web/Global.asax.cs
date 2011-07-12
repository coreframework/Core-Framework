using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Core.Logging;
using Castle.Facilities.Logging;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using Core.Framework.MEF.Composition;
using Core.Framework.MEF.ServiceLocation;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Web.NHibernate;
using Core.Web.NHibernate.Contracts;
using Framework.Core;
using Framework.Facilities.NHibernate;
using Framework.MVC.Resources;
using Microsoft.Practices.ServiceLocation;
using Application = Framework.Core.Application;

namespace Core.Web
{
    public class MvcApplication : Framework.MEF.Web.Application
    {
        #region Fields

        private static IApplication _application;

        private static ILogger _logger = NullLogger.Instance;

        private CSLExportProvider _exportProvider;

        #endregion

        #region Properties

        public static IEnumerable<ICoreWidget> Widgets { get; set; }

        public static List<IPermissible> PermissibleObjects { get; set; }

        /// <summary>
        /// Gets the application instance.
        /// </summary>
        /// <value>The application instance.</value>
        public static IApplication App
        {
            get
            {
                return _application;
            }
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public static ILogger Logger
        {
            get
            {
                return _logger;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Creates the instance of the Unity container.
        /// </summary>
        protected override void PreCompose()
        {
            _container = new WindsorContainer();
            _exportProvider = new CSLExportProvider(new WindsorServiceLocator(_container));
         
            RegisterTypes();
          
            InitializeLogger();
        }

        protected override void Compose()
        {
            base.Compose();

            foreach (var plugin in Plugins)
            {
                plugin.Register(_container);
            }

            Widgets = Composer.ResolveAll<ICoreWidget>();

            ConfigureApplication();
            ExecuteBootstrapperTasks();
            RegisterPermissibleObjects();
            RegisterAreas();

        }

        /// <summary>
        /// Executes the bootstrapper tasks.
        /// </summary>
        private void ExecuteBootstrapperTasks()
        {
            foreach (var task in _container.ResolveAll<IBootstrapperTask>())
            {
                task.Execute(_application, _container.Kernel);
            }
          
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        private static void ConfigureApplication()
        {
            _application = new Application();
            _application.Configure(_container);

        }

        /// <summary>
        /// Initializes the logger.
        /// </summary>
        private static void InitializeLogger()
        {
            _container.AddFacility("logging", new LoggingFacility(LoggerImplementation.Log4net, "Config/log4net.config"));
            _logger = _container.Resolve<ILogger>();
        }

        /// <summary>
        /// Registers any types required for the container.
        /// </summary>
        private static void RegisterTypes()
        {
            _container.Install(new CoreInstaller());
            _container.Install(new NHibernateInstaller());
            _container.Install(new YamlResourcesInstaller());
            _container.Install(new CoreWebNHibernateModule());
            _container.Install(new CoreWebInstaller());
        }

        /// <summary>
        /// Handles the PostAuthenticateRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            if (context.User != null)
            {
                var userService = ServiceLocator.Current.GetInstance<IUserService>();
                context.User = userService.FindByUsername(context.User.Identity.Name);
            }
        }

        /// <summary>
        /// Registers the areas.
        /// </summary>
        protected static void RegisterAreas()
        {
            AreaRegistration.RegisterAllAreas();
        }

        /// <summary>
        /// Creates the composer used for composition.
        /// </summary>
        /// <returns></returns>
        protected override Composer CreateComposer()
        {
            var composer = base.CreateComposer();
            composer.AddExportProvider(_exportProvider);

            return composer;
        }

        protected void RegisterPermissibleObjects()
        {
            PermissibleObjects = Composer.ResolveAll<IPermissible>().ToList();
        }

        #endregion
    }
}