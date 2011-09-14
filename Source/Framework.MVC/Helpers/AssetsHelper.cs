// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssetsHelper.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Core.Framework.Plugins.Constants;
using Core.Framework.Plugins.Web;
using Framework.Core.Helpers.Yaml;
using Yahoo.Yui.Compressor;
using Environment = Framework.Core.Configuration.Environment;

namespace Framework.MVC.Helpers
{
    /// <summary>
    /// Provides helper functionality for resources packaging.
    /// </summary>
    public static class AssetsHelper
    {
        #region Constants

        /// <summary>
        /// Default assets configuration application relative path.
        /// </summary>
        public const String DefaultConfigPath = "Config/asset_packages.yml";

        private const String CssPackageNameTemplate = "{0}_packed.css";

        private const String JavascriptPackageNameTemplate = "{0}_packed.js";

        private const String CssVirtualPath = "{0}";

        private const String PhysicalDelimeter = "\\";

        private const String VirtualDelimeter = "/";

        private const String UrlRegex = @"url\(\s*[""']?([\.\.\/]+)?([^""']+)[""']?\s*\)";

        private const String UrlPattern = "url('{0}{1}')";

        #endregion

        private static dynamic assetsConfig;

        /// <summary>
        /// Reads assets config from <see name="DefaultConfigPath"/>.
        /// </summary>
        /// <remarks>
        /// If <paramref name="environment"/> equals to <see cref="Environment.Development"/> configuration will be read on each call;
        /// otherwise configuration will be read only on first call.
        /// </remarks>
        /// <param name="environment">The environment.</param>
        /// <returns>Assets configuration.</returns>
        public static dynamic GetAssetsConfig(Environment environment)
        {
            return GetAssetsConfig(environment, DefaultConfigPath);
        }

        /// <summary>
        /// Reads assets config from <paramref name="configPath"/>.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name="configPath">The configuration path.</param>
        /// <returns>Assets configuration.</returns>
        /// <remarks>
        /// If <paramref name="environment"/> equals to <see cref="Environment.Development"/> configuration will be read on each call;
        /// otherwise configuration will be read only on first call.
        /// </remarks>
        public static dynamic GetAssetsConfig(Environment environment, String configPath)
        {
            if (Environment.Development.Equals(environment) || assetsConfig == null)
            {
                assetsConfig = YamlDocument.FromFile(configPath);
            }
            return assetsConfig;
        }

        /// <summary>
        /// Gets list of files included in css package specified in <see name="DefaultConfigPath"/>.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name="packageName">Name of the package.</param>
        /// <returns>List of files relative to css directory.</returns>
        public static IEnumerable<String> GetCssPackFiles(Environment environment, String packageName)
        {
            return GetCssPackFiles(environment, packageName, DefaultConfigPath);
        }

        /// <summary>
        /// Gets list of files included in css package specified in <paramref name="configPath"/>.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name="packageName">Name of the package.</param>
        /// <param name="configPath">The configuration path.</param>
        /// <returns>List of files relative to css directory.</returns>
        public static IEnumerable<String> GetCssPackFiles(Environment environment, String packageName, String configPath)
        {
            dynamic config = GetAssetsConfig(environment, configPath);
            var files = new List<String>();
            foreach (var file in config.stylesheets[packageName])
            {
                files.Add(file);
            }
            return files;
        }

        /// <summary>
        /// Gets list of files included in javascript package specified in <see name="DefaultConfigPath"/>.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name="packageName">Name of the package.</param>
        /// <returns>List of files relative to javascript directory.</returns>
        public static IEnumerable<String> GetJavascriptPackFiles(Environment environment, String packageName)
        {
            return GetJavascriptPackFiles(environment, packageName, DefaultConfigPath);
        }

        /// <summary>
        /// Gets list of files included in javascript package specified in <paramref name="configPath"/>.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name="packageName">Name of the package.</param>
        /// <param name="configPath">The configuration path.</param>
        /// <returns>
        /// List of files relative to javascript directory.
        /// </returns>
        public static IEnumerable<String> GetJavascriptPackFiles(Environment environment, String packageName, String configPath)
        {
            dynamic config = GetAssetsConfig(environment, configPath);
            var files = new List<String>();
            foreach (var file in config.javascripts[packageName])
            {
                files.Add(file);
            }
            return files;
        }

        /// <summary>
        /// Builds the css package file specified in <see name="DefaultConfigPath"/>.
        /// </summary>
        /// <remarks>
        /// If package file already exists it will be rebuilt according with files access time.
        /// </remarks>
        /// <param name="environment">The environment.</param>
        /// <param name="packageName">Name of the package.</param>
        /// <param name="cssPath">The css directory server path.</param>
        /// <returns>Generated package file name.</returns>
        public static String BuildCssPack(Environment environment, String packageName, String cssPath)
        {
            return BuildCssPack(environment, packageName, cssPath, DefaultConfigPath);
        }

        /// <summary>
        /// Builds the css package file specified in <paramref name="configPath"/>.
        /// </summary>
        /// <remarks>
        /// If package file already exists it will be rebuilt according with files access time.
        /// </remarks>
        /// <param name="environment">The environment.</param>
        /// <param name="packageName">Name of the package.</param>
        /// <param name="cssPath">The css directory server path.</param>
        /// <param name="configPath">Assets config path.</param>
        /// <returns>Generated package file name.</returns>
        public static String BuildCssPack(Environment environment, String packageName, String cssPath, String configPath)
        {
            var packageFileName = String.Format(CssPackageNameTemplate, packageName);
            var packagePath = Path.Combine(cssPath, packageFileName);
            var files = GetCssPackFiles(environment, packageName, configPath);

            if (!File.Exists(packagePath) || File.GetLastWriteTimeUtc(packagePath) < GetMaxLastModifyDate(cssPath, files))
            {
                var output = new StringBuilder();
                foreach (var file in files)
                {
                    using (var reader = new StreamReader(Path.Combine(cssPath, file)))
                    {
                        var temp = reader.ReadToEnd();
                        if (!String.IsNullOrEmpty(temp))
                        {
                            output.Append(CssCompressor.Compress(temp));
                        }
                    }
                }

                using (var writer = new StreamWriter(File.Create(packagePath)))
                {
                    writer.Write(output.ToString());
                }
            }

            return packageFileName;
        }

        /// <summary>
        /// Builds the javascript package file specified in <see name="DefaultConfigPath"/>.
        /// </summary>
        /// <remarks>
        /// If package file already exists it will be rebuilt according with files access time.
        /// </remarks>
        /// <param name="environment">The environment.</param>
        /// <param name="packageName">Name of the package.</param>
        /// <param name="javascriptPath">The javascript directory server path.</param>
        /// <returns>Generated package file name.</returns>
        public static String BuildJavascriptPack(Environment environment, String packageName, String javascriptPath)
        {
            return BuildJavascriptPack(environment, packageName, javascriptPath, DefaultConfigPath);
        }

        /// <summary>
        /// Builds the javascript package file specified in <paramref name="configPath"/>.
        /// </summary>
        /// <remarks>
        /// If package file already exists it will be rebuilt according with files access time.
        /// </remarks>
        /// <param name="environment">The environment.</param>
        /// <param name="packageName">Name of the package.</param>
        /// <param name="javascriptPath">The javascript directory server path.</param>
        /// <param name="configPath">Assets config path.</param>
        /// <returns>Generated package file name.</returns>
        public static String BuildJavascriptPack(Environment environment, String packageName, String javascriptPath, String configPath)
        {
            var packageFileName = String.Format(JavascriptPackageNameTemplate, packageName);
            var packagePath = Path.Combine(javascriptPath, packageFileName);
            var files = GetJavascriptPackFiles(environment, packageName, configPath);

            if (!File.Exists(packagePath) || File.GetLastWriteTimeUtc(packagePath) < GetMaxLastModifyDate(javascriptPath, files))
            {
                var output = new StringBuilder();
                foreach (var file in files)
                {
                    using (var reader = new StreamReader(Path.Combine(javascriptPath, file)))
                    {
                        var temp = reader.ReadToEnd();
                        if (!String.IsNullOrEmpty(temp))
                        {
                            output.Append(JavaScriptCompressor.Compress(temp));
                        }
                    }
                }

                using (var writer = new StreamWriter(File.Create(packagePath)))
                {
                    writer.Write(output.ToString());
                }
            }

            return packageFileName;
        }

        /// <summary>
        /// Builds the plugin CSS pack.
        /// </summary>
        /// <param name="corePlugin">The core plugin.</param>
        /// <param name="applicationServerPath">The application server path.</param>
        /// <returns>
        /// Return Html CSS links.
        /// </returns>
        public static String BuildPluginCssPack(ICorePlugin corePlugin, String applicationServerPath)
        {
            if (String.IsNullOrEmpty(corePlugin.CssJsConfigPath) || String.IsNullOrEmpty(corePlugin.CssPath) || String.IsNullOrEmpty(corePlugin.CssPack))
            {
                return string.Empty;
            }

            var pluginCssServerPath = Path.Combine(corePlugin.PluginDirectory, corePlugin.CssPath);
            var pluginPackageCssServerPath = Path.Combine(pluginCssServerPath, Constants.PluginCssPackage);
            var configPath = Path.Combine(corePlugin.PluginDirectory, corePlugin.CssJsConfigPath);
            var files = GetPluginCssPackFiles(corePlugin.CssPack, configPath);

            if (!File.Exists(pluginPackageCssServerPath) || File.GetLastWriteTimeUtc(pluginPackageCssServerPath) < GetMaxLastModifyDate(Path.Combine(corePlugin.PluginDirectory, corePlugin.CssPath), files))
            {
                var outputCss = new StringBuilder();

                foreach (var file in files)
                {
                    using (var reader = new StreamReader(Path.Combine(pluginCssServerPath, file)))
                    {
                        var temp = reader.ReadToEnd();
                        if (!String.IsNullOrEmpty(temp))
                        {
                            temp = ReplaceCssUrls(temp, MakeVirtualCssPath(corePlugin, applicationServerPath));
                            outputCss.Append(CssCompressor.Compress(temp));
                        }
                    }
                }
                using (var writer = new StreamWriter(File.Create(pluginPackageCssServerPath)))
                {
                    writer.Write(outputCss.ToString());
                }
            }
            return pluginPackageCssServerPath;
        }

        /// <summary>
        /// Gets list of files included in plugin css package specified in <paramref name="configPath"/>.
        /// </summary>
        /// <param name="packageName">Name of the package.</param>
        /// <param name="configPath">The configuration path.</param>
        /// <returns>List of files relative to css directory.</returns>
        public static IEnumerable<String> GetPluginCssPackFiles(String packageName, String configPath)
        {
            dynamic config = GetPluginAssetsConfig(configPath);
            var files = new List<String>();
            foreach (var file in config.stylesheets[packageName])
            {
                files.Add(file);
            }
            return files;
        }

        /// <summary>
        /// Gets list of files included in plugin js package specified in <paramref name="configPath"/>.
        /// </summary>
        /// <param name="packageName">Name of the package.</param>
        /// <param name="configPath">The configuration path.</param>
        /// <param name="scriptType">The type of script (internal/external).</param>
        /// <returns>List of files relative to js directory.</returns>
        public static IEnumerable<String> GetPluginJsPackFiles(String packageName, String configPath, String scriptType)
        {
            dynamic config = GetPluginAssetsConfig(configPath);
            var files = new List<String>();

            if (config.javascripts != null && config.javascripts[packageName] != null && config.javascripts[packageName][scriptType] != null)
            {
                foreach (dynamic file in config.javascripts[packageName][scriptType])
                {
                    files.Add(file);
                }
            }
           
            return files;
        }

        /// <summary>
        /// Gets the plugin inner js path.
        /// </summary>
        /// <param name="corePlugin">The core plugin.</param>
        /// <param name="applicationVirtualPath">The application virtual path.</param>
        /// <param name="applicationServerPath">The application server path.</param>
        /// <returns>Returns javascript package virtual path.</returns>
        public static String GetPluginInnerJsVirtualPath(ICorePlugin corePlugin, String applicationVirtualPath, String applicationServerPath)
        {
            var pluginPackageJsServerPath = GetPluginInnerJsPath(corePlugin, applicationServerPath);

            if (!String.IsNullOrEmpty(pluginPackageJsServerPath))
            {
                // get javascript package virtual path
                pluginPackageJsServerPath = pluginPackageJsServerPath.Replace(applicationServerPath.ToLower(), applicationVirtualPath).Replace("\\", "/");
            }

            return pluginPackageJsServerPath;
        }

        /// <summary>
        /// Gets the plugin inner js path.
        /// </summary>
        /// <param name="corePlugin">The core plugin.</param>
        /// <param name="applicationServerPath">The application server path.</param>
        /// <returns>Returns javascript package path.</returns>
        public static String GetPluginInnerJsPath(ICorePlugin corePlugin, String applicationServerPath)
        {
            if (String.IsNullOrEmpty(corePlugin.CssJsConfigPath) || String.IsNullOrEmpty(corePlugin.JsPath) || String.IsNullOrEmpty(corePlugin.JsPack))
            {
                return string.Empty;
            }

            var pluginJsServerPath = Path.Combine(corePlugin.PluginDirectory, corePlugin.JsPath);

            var pluginPackageJsServerPath = Path.Combine(pluginJsServerPath, Constants.PluginJsPackage).ToLower();

            var configPath = Path.Combine(corePlugin.PluginDirectory, corePlugin.CssJsConfigPath);
            var files = GetPluginJsPackFiles(corePlugin.JsPack, configPath, Constants.InnerJsTypeName);

            if (!File.Exists(pluginPackageJsServerPath) || File.GetLastWriteTimeUtc(pluginPackageJsServerPath) < GetMaxLastModifyDate(Path.Combine(corePlugin.PluginDirectory, corePlugin.JsPath), files))
            {
                var outputJs = new StringBuilder();

                foreach (var file in files)
                {
                    if (File.Exists(Path.Combine(pluginJsServerPath, file)))
                    {
                        using (var reader = new StreamReader(Path.Combine(pluginJsServerPath, file)))
                        {
                            string content = reader.ReadToEnd();
                            if (!String.IsNullOrEmpty(content))
                            {
                                outputJs.Append(JavaScriptCompressor.Compress(content));
                            }
                        }
                    }
                }
                using (var writer = new StreamWriter(File.Create(pluginPackageJsServerPath)))
                {
                    writer.Write(outputJs.ToString());
                }
            }
         
            return pluginPackageJsServerPath;
        }

        /// <summary>
        /// Reads plugin assets config from <paramref name="configPath"/>.
        /// </summary>
        /// <param name="configPath">The configuration path.</param>
        /// <returns>Plugin assets configuration.</returns>
        /// <remarks>
        /// </remarks>
        public static dynamic GetPluginAssetsConfig(String configPath)
        {
            return YamlDocument.FromFile(configPath);
        }

        /// <summary>
        /// Builds the plugins CSS pack.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name="corePlugins">The core widgets.</param>
        /// <param name="applicationServerPath">The application server path.</param>
        /// <param name="cssServerPath">The CSS server path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// Return Html CSS links.
        /// </returns>
        public static String BuildPluginsCssPack(Environment environment, IEnumerable<ICorePlugin> corePlugins, String applicationServerPath, String cssServerPath, String fileName)
        {
            var packageFileName = fileName;
            var packagePath = Path.Combine(cssServerPath, packageFileName);
            if (!File.Exists(packagePath))
            {
                if (!Directory.Exists(cssServerPath))
                {
                    Directory.CreateDirectory(cssServerPath);
                }
                var output = new StringBuilder();
                foreach (var plugin in corePlugins)
                {
                    if (String.IsNullOrEmpty(plugin.CssJsConfigPath) || String.IsNullOrEmpty(plugin.CssPath) || String.IsNullOrEmpty(plugin.CssPack))
                    {
                        continue;
                    }

                    var pluginPackageCssServerPath = Path.Combine(plugin.PluginDirectory, plugin.CssPath, Constants.PluginCssPackage);
                    var configPath = Path.Combine(plugin.PluginDirectory, plugin.CssJsConfigPath);
                    var files = GetPluginCssPackFiles(plugin.CssPack, configPath);

                    if (!File.Exists(pluginPackageCssServerPath) || File.GetLastWriteTimeUtc(pluginPackageCssServerPath) < GetMaxLastModifyDate(Path.Combine(plugin.PluginDirectory, plugin.CssPath), files))
                    {
                        BuildPluginCssPack(plugin, applicationServerPath);
                    }
                    using (var reader = new StreamReader(pluginPackageCssServerPath))
                    {
                        var temp = reader.ReadToEnd();
                        if (!String.IsNullOrEmpty(temp))
                        {
                            output.Append(CssCompressor.Compress(temp));
                        }
                    }
                }
                using (var writer = new StreamWriter(File.Create(packagePath)))
                {
                    writer.Write(output.ToString());
                }
            }
            return packagePath;
        }
        private static DateTime GetMaxLastModifyDate(String baseDirectory, IEnumerable<String> files)
        {
            var maxModifyDate = DateTime.MinValue;

            foreach (var file in files)
            {
                var modifyDate = File.GetLastWriteTimeUtc(Path.Combine(baseDirectory, file));
                if (modifyDate > maxModifyDate)
                {
                    maxModifyDate = modifyDate;
                }
            }

            return maxModifyDate;
        }

        /// <summary>
        /// Replaces the CSS urls.
        /// </summary>
        /// <param name="cssContent">Content of the CSS.</param>
        /// <param name="imagePluginPath">The image plugin path.</param>
        /// <returns>Return content Css with replaced urls.</returns>
        private static String ReplaceCssUrls(string cssContent, string imagePluginPath)
        {
            var regex = new Regex(UrlRegex);
            var vals = regex.Matches(cssContent).Cast<Match>().ToDictionary(mathe => mathe.Index.ToString(),
                                                                      mathe => mathe.Groups[2].Value);
            return regex.Replace(cssContent,
                                 match => string.Format(UrlPattern, imagePluginPath, vals[match.Index.ToString()]));
        }

        /// <summary>
        /// Makes the virtual CSS path.
        /// </summary>
        /// <param name="coreWidget">The core widget.</param>
        /// <param name="serverPath">The server path.</param>
        /// <returns>Return virtual Url.</returns>
        private static String MakeVirtualCssPath(ICorePlugin coreWidget, String serverPath)
        {
            var physicalContentPath = Path.Combine(coreWidget.PluginDirectory, coreWidget.ImagesPath);
            var partPhysicalPluginPath = physicalContentPath.ToLower().Replace(serverPath.ToLower(), String.Empty);
            var virtualPluginPath = partPhysicalPluginPath.Replace(PhysicalDelimeter, VirtualDelimeter);
            return String.Format(CssVirtualPath, virtualPluginPath);
        }
    }
}