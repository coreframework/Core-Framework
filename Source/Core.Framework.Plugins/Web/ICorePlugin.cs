using System;
using System.Reflection;
using Castle.Windsor;

namespace Core.Framework.Plugins.Web
{
    /// <summary>
    /// Defines a plugin
    /// </summary>
    public interface ICorePlugin
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        String Identifier { get;}

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title.</value>
        String Title { get;}

        /// <summary>
        /// Gets the plugin directory.
        /// </summary>
        /// <value>The plugin directory.</value>
        String PluginLocation { get; }

        /// <summary>
        /// Gets the plugin directory.
        /// </summary>
        /// <value>The plugin directory.</value>
        String PluginDirectory { get; }

        /// <summary>
        /// Gets the resources directory.
        /// </summary>
        /// <value>The resources directory.</value>
        String ResourcesDirectory { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        String Description { get;}

        /// <summary>
        /// Registers the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        void Register(IWindsorContainer container);

        /// <summary>
        /// Installs this instance.
        /// </summary>
        void Install();

        /// <summary>
        /// Uninstalls this instance.
        /// </summary>
        void Uninstall();

        /// <summary>
        /// Gets the plugin migrations assembly.
        /// </summary>
        /// <returns></returns>
        Assembly GetPluginMigrationsAssembly();

        /// <summary>
        /// Gets the Plugin config path.
        /// </summary>
        String ConfigPath { get; }

        /// <summary>
        /// Gets the Plugin images path.
        /// </summary>
        String ImagesPath { get; }

        /// <summary>
        /// Gets the Plugin CSS path.
        /// </summary>
        String CssPath { get; }

        /// <summary>
        /// Gets the Plugin CSS pack.
        /// </summary>
        String CssPack { get; }
    }
}
