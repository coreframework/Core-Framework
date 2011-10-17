using Castle.Windsor;
using Core.Forms.NHibernate;
using Framework.Core;
using Framework.Core.Services;
using Framework.Facilities.NHibernate;
using NUnit.Framework;

namespace Core.Forms.Tests.Services
{
    /// <summary>
    /// CRUD unit test for data services.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TService">The type of the service.</typeparam>
    public abstract class AbstractServiceTest<TEntity, TService> where TService : IDataService<TEntity>
    {
        #region Fields

        private IWindsorContainer container;

        private IApplication application;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>The container.</value>
        protected IWindsorContainer Container
        {
            get
            {
                return container;
            }
        }

        /// <summary>
        /// Gets the application.
        /// </summary>
        /// <value>The application.</value>
        protected IApplication Application
        {
            get
            {
                return application;
            }
        }

        #endregion

        #region Test methods

        /// <summary>
        /// Initalizes execution context.
        /// </summary>
        [TestFixtureSetUp]
        public void Init()
        {
            InitializeContainer();
            ConfigureApplication();
            ExecuteBootstrapperTasks();
        }

        /// <summary>
        /// Removes all <typeparamref name="TEntity"/> items.
        /// </summary>
        [TearDown]
        public void Clean()
        {
            Container.Resolve<TService>().DeleteAll();
        }

        #endregion

        #region Initialization

        private void InitializeContainer()
        {
            container = new WindsorContainer();
            container.Install(new CoreInstaller());
            container.Install(new NHibernateInstaller());
            CoreFormsNHibernateModule.Install(container);
        }

        private void ConfigureApplication()
        {
            application = new Application();
            application.Configure(container);
        }

        private void ExecuteBootstrapperTasks()
        {
            foreach (var task in container.ResolveAll<IBootstrapperTask>())
            {
                task.Execute(application, container.Kernel);
            }
        }

        #endregion
    }
}