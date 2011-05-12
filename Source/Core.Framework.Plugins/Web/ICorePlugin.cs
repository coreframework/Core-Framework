using System;
using System.Reflection;
using Castle.Windsor;
using Core.Framework.MEF.ServiceLocation;

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
    }
}
