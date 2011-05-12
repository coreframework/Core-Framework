// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssetsHelper.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
        /// <summary>
        /// Default assets configuration application relative path.
        /// </summary>
        public const String DefaultConfigPath = "Config/asset_packages.yml";

        private const String CssPackageNameTemplate = "{0}_packed.css";

        private const String JavascriptPackageNameTemplate = "{0}_packed.js";

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
    }
}