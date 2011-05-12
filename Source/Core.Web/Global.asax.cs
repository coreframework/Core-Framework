using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Core.Logging;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using Core.Framework.MEF.Composition;
using Core.Framework.MEF.ServiceLocation;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Web.Helpers;
using Core.Web.NHibernate;
using Core.Web.NHibernate.Contracts;
using Framework.Core;
using Framework.Facilities.NHibernate;
using Framework.MVC.Resources;
using Microsoft.Practices.ServiceLocation;
using Application = Framework.Core.Application;

namespace Core.Web
{
    public class MvcApplication : Core.Framework.MEF.Web.Application
    {
        #region Fields

        private static WindsorContainer container;

        private static IApplication application;

        private static ILogger logger = NullLogger.Instance;

        private CSLExportProvider exportProvider;

        #endregion

        #region Properties

        public static IEnumerable<ICorePlugin> Plugins { get; set; }

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
                return application;
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
                return logger;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Creates the instance of the Unity container.
        /// </summary>
        protected override void PreCompose()
        {
            container = new WindsorContainer();
            exportProvider = new CSLExportProvider(new WindsorServiceLocator(container));
         
            RegisterTypes();
          
            InitializeLogger();
        }

        protected override void Compose()
        {
            base.Compose();

            Plugins = Composer.ResolveAll<ICorePlugin>();

            foreach (var plugin in Plugins)
            {
                plugin.Register(container);
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
            foreach (var task in container.ResolveAll<IBootstrapperTask>())
            {
                task.Execute(application, container.Kernel);
            }
          
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        private static void ConfigureApplication()
        {
            application = new Application();
            application.Configure(container);

        }

        /// <summary>
        /// Initializes the logger.
        /// </summary>
        private static void InitializeLogger()
        {
            container.AddFacility("logging", new LoggingFacility(LoggerImplementation.Log4net, "Config/log4net.config"));
            logger = container.Resolve<ILogger>();
        }

        /// <summary>
        /// Registers any types required for the container.
        /// </summary>
        private static void RegisterTypes()
        {
            container.Install(new CoreInstaller());
            container.Install(new NHibernateInstaller());
            container.Install(new YamlResourcesInstaller());
            container.Install(new CoreWebNHibernateModule());
            //TODO: find better place for this registration
            container.Register(Component.For<IPluginHelper>().ImplementedBy<PluginHelper>().LifeStyle.Transient);
            container.Register(Component.For<IWidgetHelper>().ImplementedBy<WidgetHelper>().LifeStyle.Transient);
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
            composer.AddExportProvider(exportProvider);

            return composer;
        }

        protected void RegisterPermissibleObjects()
        {
            PermissibleObjects = Composer.ResolveAll<IPermissible>().ToList();
        }

        #endregion
    }
}