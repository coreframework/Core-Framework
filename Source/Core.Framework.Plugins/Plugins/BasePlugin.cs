using System.Reflection;
using Castle.Windsor;
using Core.Framework.Plugins.Web;

namespace Core.Framework.Plugins.Plugins
{
    public abstract class BasePlugin : ICorePlugin
    {
        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public abstract string Title { get; }

        /// <summary>
        /// Gets the resources directory.
        /// </summary>
        /// <value>The resources directory.</value>
        public abstract string ResourcesDirectory { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public abstract string Description { get; }

        /// <summary>
        /// Registers the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public abstract void Register(IWindsorContainer container);

        /// <summary>
        /// Installs this instance.
        /// </summary>
        public abstract void Install();

        /// <summary>
        /// Uninstalls this instance.
        /// </summary>
        public abstract void Uninstall();

        /// <summary>
        /// Gets the plugin migrations assembly.
        /// </summary>
        /// <returns></returns>
        public abstract Assembly GetPluginMigrationsAssembly();

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public abstract string Identifier { get; }

        #endregion

    }
}
