using System;
using System.IO;
using System.Reflection;
using Castle.Windsor;
using Core.Framework.Plugins.Web;

namespace Core.Framework.Plugins.Plugins
{
    public abstract class BasePlugin : ICorePlugin
    {
        #region Constants

        public const String PluginCssPackage = "plugin_package.css";

        #endregion

        #region Fields

        private String pluginLocation;

        private String pluginDirectory;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public abstract string Title { get; }

        public String PluginLocation
        {
            get
            {
                if (String.IsNullOrEmpty(pluginLocation))
                {
                    pluginLocation = Assembly.GetAssembly(GetType()).Location;
                }
                return pluginLocation;
            }
        }

        /// <summary>
        /// Gets the plugin directory.
        /// </summary>
        public String PluginDirectory
        {
            get
            {
                if (String.IsNullOrEmpty(pluginDirectory))
                {
                    var file = new FileInfo(PluginLocation);
                    if (file.Exists)
                    {
                        pluginDirectory = file.Directory.ToString();
                    }
                }
                return pluginDirectory;
            }
        }

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
        /// Gets the config path. Default String.Empty.
        /// [Example: @"Config\asset_packages.yml"]
        /// </summary>
        public virtual string ConfigPath
        {
            get { return String.Empty; }
        }

        /// <summary>
        /// Gets the images path. Default String.Empty.
        /// [Example: @"Content\Images\"]
        /// </summary>
        public virtual string ImagesPath
        {
            get { return String.Empty; }
        }

        /// <summary>
        /// Gets the CSS path. Default String.Empty.
        /// [Example: @"Content\Css\"]
        /// </summary>
        public virtual string CssPath
        {
            get { return String.Empty; }
        }

        /// <summary>
        /// Gets the CSS pack which placed in the config. Default String.Empty.
        /// [Example: "base1"]
        /// </summary>
        public virtual string CssPack
        {
            get { return String.Empty; }
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public abstract string Identifier { get; }

        #endregion

    }
}
