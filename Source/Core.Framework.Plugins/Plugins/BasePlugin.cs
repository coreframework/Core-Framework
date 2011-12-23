using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using Castle.Windsor;
using Core.Framework.Plugins.Configs;
using Core.Framework.Plugins.Web;

namespace Core.Framework.Plugins.Plugins
{
    public abstract class BasePlugin : ICorePlugin
    {
        #region Fields

        private String pluginLocation;

        private String pluginDirectory;

        private IPluginSetting pluginConfig;

        private String pluginAreaDirectoryName;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public virtual String Identifier
        {
            get { return PluginSetting.Identifier; }
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version
        {
            get { return PluginSetting.Version; }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title
        {
            get { return PluginSetting.Title; }
        }

        /// <summary>
        /// Gets the plugin's full location.
        /// </summary>
        /// <value>The plugin's full location.</value>
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
        /// Gets the plugin's directory.
        /// </summary>
        /// <value>The plugin's directory.</value>
        public virtual String PluginDirectory
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

        public virtual String PluginAreaDirectoryName
        {
            get
            {
                if(String.IsNullOrEmpty(pluginAreaDirectoryName))
                {
                    var file = new FileInfo(PluginLocation);
                    if (file.Exists)
                    {
                        pluginAreaDirectoryName = file.Directory.Name;
                    }
                }
                return pluginAreaDirectoryName;
            }
        }

        /// <summary>
        /// Gets the plugin identifiers config.
        /// </summary>
        public IPluginSetting PluginSetting
        {
            get { return pluginConfig ?? (pluginConfig = GetSettings(Path.Combine(PluginDirectory, PluginConfigPath))); }
        }

        /// <summary>
        /// Gets the resources directory.
        /// </summary>
        /// <value>The resources directory.</value>
        public virtual String ResourcesDirectory
        {
            get { return PluginSetting.ResourcesDirectory; }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual String Description
        {
            get { return PluginSetting.Description; }
        }

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
        /// Starts this instance.
        /// </summary>
        public virtual void Start()
        {

        }

        /// <summary>
        /// Gets the plugin migrations assembly.
        /// </summary>
        /// <returns></returns>
        public abstract Assembly GetPluginMigrationsAssembly();

        /// <summary>
        /// Gets the config path. Default String.Empty.
        /// [Example: @"Config\asset_packages.yml"]
        /// </summary>
        public virtual String CssJsConfigPath
        {
            get { return PluginSetting.CssJsConfigPath; }
        }

        /// <summary>
        /// Gets the Plugin Identifiers config path.
        /// </summary>
        public virtual String PluginConfigPath
        {
            get { return String.Empty; }
        }

        /// <summary>
        /// Gets the images path. Default String.Empty.
        /// [Example: @"Content\Images\"]
        /// </summary>
        public virtual String ImagesPath
        {
            get { return PluginSetting.ImagesPath; }
        }

        /// <summary>
        /// Gets the CSS path. Default String.Empty.
        /// [Example: @"Content\Css\"]
        /// </summary>
        public virtual String CssPath
        {
            get { return PluginSetting.CssPath; }
        }

        /// <summary>
        /// Gets the CSS pack which placed in the config. Default String.Empty.
        /// [Example: "base1"]
        /// </summary>
        public virtual String CssPack
        {
            get { return PluginSetting.CssPack; }
        }

        public String JsPath
        {
            get { return PluginSetting.JsPath; }
        }

        public String JsPack
        {
            get { return PluginSetting.JsPack; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Reads the XML config.
        /// </summary>
        /// <param name="configPath">The config path.</param>
        /// <returns></returns>
        private static IPluginSetting GetSettings(String configPath)
        {
            try
            {
                PluginSetting doc;
                var serializer = new XmlSerializer(typeof(PluginSetting));
                using (var reader = new FileStream(configPath, FileMode.Open))
                {
                    doc = serializer.Deserialize(reader) as PluginSetting;
                }
                return doc ?? new PluginSetting();
            }
            catch (Exception)
            {
                return new PluginSetting();
            }
        }

        #endregion
    }
}
