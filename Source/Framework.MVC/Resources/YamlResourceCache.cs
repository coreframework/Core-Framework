// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YamlResourceCache.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using Castle.Core.Logging;

using Microsoft.Practices.ServiceLocation;

using Yaml.Grammar;

using Environment = Framework.Core.Configuration.Environment;

namespace Framework.MVC.Resources
{
    /// <summary>
    /// Parses resources from yaml-files. Monitors resource files changes for development environment.
    /// </summary>
    /// <remarks>
    /// <see cref="YamlResourceCache"/> is thread safe.
    /// </remarks>
    public class YamlResourceCache : IResourceCache, IDisposable
    {
        #region Fields

        private const String ScopeSeparator = ".";

        private const String YamlFilesPattern = "*.yml";

        private static readonly Object syncRoot = new Object();

        private readonly String resourcesPath;

        private readonly FileSystemWatcher fileMonitor;

        private readonly TimeSpan maximumRetryPeriod = new TimeSpan(0, 0, 30);

        private readonly TimeSpan retryDelay = new TimeSpan(0, 0, 5);

        private IDictionary<String, String> resources;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="YamlResourceCache"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name="resourcesPath">The resources path.</param>
        public YamlResourceCache(Environment environment, String resourcesPath)
        {
            this.resourcesPath = resourcesPath;

            ReadResources();

            if (Environment.Development.Equals(environment))
            {
                fileMonitor = new FileSystemWatcher(resourcesPath, YamlFilesPattern);
                fileMonitor.IncludeSubdirectories = true;
                fileMonitor.Changed += ResourcesChangedHandler;
                fileMonitor.EnableRaisingEvents = true;
            }
        }

        #endregion

        #region IResourceCache members

        /// <summary>
        /// Updates resources cache.
        /// </summary>
        public void Update()
        {
            ReadResources();
        }

        /// <summary>
        /// Gets the resource by key.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <returns>value associated with <paramref name="key"/> specified or <c>null</c>.</returns>
        public String GetResource(String key)
        {
            lock (syncRoot)
            {
                String value;
                resources.TryGetValue(key.ToLowerInvariant(), out value);
                return value;
            }
        }

        /// <summary>
        /// Gets resources enumerator.
        /// </summary>
        /// <returns>resources enumerator.</returns>
        public IEnumerator<KeyValuePair<String, String>> GetEnumerator()
        {
            lock (syncRoot)
            {
                return resources.GetEnumerator();
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region IDisposable members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            fileMonitor.Dispose();
        }

        #endregion

        #region Helper members

        private void ResourcesChangedHandler(Object sender, FileSystemEventArgs e)
        {
            ReadResources();
        }

        private void ReadResources()
        {
            lock (syncRoot)
            {
                resources = new Dictionary<String, String>();

                foreach (var file in Directory.GetFiles(resourcesPath, YamlFilesPattern, SearchOption.AllDirectories))
                {
                    ThreadPool.QueueUserWorkItem(ProcessFile, file);
                }
            }
        }

        private void ProcessFile(Object state)
        {
            var logger = ServiceLocator.Current.GetInstance<ILogger>();
            var file = state as String;
            if (!String.IsNullOrEmpty(file) && File.Exists(file))
            {
                DateTime startTime = DateTime.Now;

                while (true)
                {
                    if (IsFileAccessible(file))
                    {
                        try
                        {
                            var yaml = YamlParser.Load(file).Documents;
                            foreach (var document in yaml)
                            {
                                AddNode(document.Root, String.Empty);
                            }

                            break;
                        }
                        catch (Exception e)
                        {
                            logger.Error("Some erro was occured during \"{0}\" processing ({1}).", file, e.Message);
                        }
                    }

                    // Calculate the elapsed time and stop if the maximum retry
                    // period has been reached.
                    TimeSpan timeElapsed = DateTime.Now - startTime;

                    if (timeElapsed > maximumRetryPeriod)
                    {
                        logger.Error("The file \"{0}\" could not be processed.", file);
                        break;
                    }

                    Thread.Sleep(retryDelay);
                }
            }
        }

        /// <summary>
        /// Check if the file is accesible.
        /// </summary>
        /// <param name="filename">The name of file to check.</param>
        /// <returns>
        /// <c>true</c> if the specified file is accessible; <c>false</c> otherwise.
        /// </returns>
        private bool IsFileAccessible(string filename)
        {
            bool success;

            // If the file can be opened for exclusive access it means that the file
            // is no longer locked by another process.
            try
            {
                using (File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    success = true;
                }
            }
            catch (IOException)
            {
                success = false;
            }

            return success;
        }

        private void AddNode(DataItem node, String path)
        {
            var mapping = node as Mapping;
            if (mapping != null)
            {
                foreach (var record in mapping.Enties)
                {
                    var key = record.Key as Scalar;
                    if (key != null)
                    {
                        AddNode(record.Value, Combine(key.Text, path));
                    }
                }
            }

            var scalar = node as Scalar;
            if (scalar != null)
            {
                resources[path] = scalar.Text;
            }
        }

        private String Combine(String key, String prefix)
        {
            if (String.IsNullOrEmpty(prefix))
            {
                return key.ToLowerInvariant();
            }

            return prefix + ScopeSeparator + key.ToLowerInvariant();
        }

        #endregion
    }
}