﻿using System;
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
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        String Version { get;}

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
        /// Gets the name of the plugin area directory.
        /// </summary>
        /// <value>The name of the plugin area directory.</value>
        String PluginAreaDirectoryName { get; }

        /// <summary>
        /// Gets the plugin identifiers config.
        /// </summary>
        IPluginSetting PluginSetting { get; }

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
        /// Starts this instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Gets the plugin migrations assembly.
        /// </summary>
        /// <returns></returns>
        Assembly GetPluginMigrationsAssembly();

        /// <summary>
        /// Gets the Plugin Css and JS config path.
        /// </summary>
        String CssJsConfigPath { get; }

        /// <summary>
        /// Gets the Plugin Identifiers config path.
        /// </summary>
        String PluginConfigPath { get; }

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

        /// <summary>
        /// Gets the js path.
        /// </summary>
        /// <value>The js path.</value>
        String JsPath { get;}

        /// <summary>
        /// Gets the js package name.
        /// </summary>
        /// <value>The js package.</value>
        String JsPack { get; }
    }
}
