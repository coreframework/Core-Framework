using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Core.Logging;
using Castle.Facilities.Logging;
using Core.Framework.MEF.Composition;
using Core.Framework.NHibernate.Contracts;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Web.NHibernate;
using Framework.Core;
using Framework.Facilities.NHibernate;
using Framework.Mvc.Metadata;
using Framework.Mvc.Resources;
using Microsoft.Practices.ServiceLocation;
using Application = Core.Framework.MEF.Web.Application;

namespace Core.Web
{
    public class MvcApplication : Application
    {
        #region Fields

        private static IApplication application;

        private static ILogger logger = NullLogger.Instance;

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


            RegisterTypes();

            InitializeLogger();
        }

        protected override void Compose()
        {
            base.Compose();

            foreach (var plugin in Plugins)
            {
                plugin.Register(Composer.Instance.WindsorContainer);
            }

            Widgets = Composer.Instance.ResolveAll<ICoreWidget>();

            ConfigureApplication();
            ExecuteBootstrapperTasks();
            RegisterPermissibleObjects();
            RegisterAreas();
            ModelMetadataProviders.Current = new MetadataProvider(true, true);

        }

        /// <summary>
        /// Executes the bootstrapper tasks.
        /// </summary>
        private static void ExecuteBootstrapperTasks()
        {
            foreach (var task in Composer.Instance.WindsorContainer.ResolveAll<IBootstrapperTask>())
            {
                task.Execute(application, Composer.Instance.WindsorContainer.Kernel);
            }

        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        private static void ConfigureApplication()
        {
            application = new global::Framework.Core.Application();
            application.Configure(Composer.Instance.WindsorContainer);

        }

        /// <summary>
        /// Initializes the logger.
        /// </summary>
        private static void InitializeLogger()
        {
            Composer.Instance.WindsorContainer.AddFacility("logging", new LoggingFacility(LoggerImplementation.Log4net, "Config/log4net.config"));
            logger = Composer.Instance.WindsorContainer.Resolve<ILogger>();
        }

        /// <summary>
        /// Registers any types required for the container.
        /// </summary>
        private static void RegisterTypes()
        {
            Composer.Instance.WindsorContainer.Install(new CoreInstaller());
            Composer.Instance.WindsorContainer.Install(new NHibernateInstaller());
            Composer.Instance.WindsorContainer.Install(new YamlResourcesInstaller());
            Composer.Instance.WindsorContainer.Install(new CoreWebNHibernateModule());
            Composer.Instance.WindsorContainer.Install(new CoreWebInstaller());
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
                var user = userService.FindByUsername(context.User.Identity.Name);

                if (user != null)
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

        private static void RegisterPermissibleObjects()
        {
            PermissibleObjects = Composer.Instance.ResolveAll<IPermissible>().ToList();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
            Logger.Error(exc.Message);

#if !DEBUG
           
            if (Request.RawUrl.Contains("admin") && !Request.RawUrl.Contains("administration"))
                Response.Redirect("~/admin/error");
            else Response.Redirect("~/error");

#endif
        }

        #endregion
    }
}