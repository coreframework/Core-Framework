using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Reflection;
using Castle.Windsor;
using Core.ContentPages.Migrations;
using Core.Framework.MEF.ServiceLocation;
using Core.Framework.Plugins.Web;

namespace Core.TestModule
{
    [Export(typeof(ICorePlugin))]
    public class TestPlugin : ICorePlugin
    {
        private const String identifierKey = "Identifier";

        public string Identifier
        {
            get
            {
                return GetPluginIdentifier();
            }
        }

        public string Title
        {
            get
            {
                return "Test";
            }
        }

        public string Description
        {
            get
            {
                return "Test Plugin Description";
            }
        }

        public void Register(IWindsorContainer container)
        {
         
        }

        public void Install()
        {
            
        }

        public void Uninstall()
        {

        }

        public Assembly GetPluginMigrationsAssembly()
        {
            return Assembly.Load("Core.TestModule.Migrations");
        }

        //TODO: kill it!!!
        public static string GetPluginIdentifier()
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = String.Format("{0}.config", Assembly.GetExecutingAssembly().Location);
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            return config.AppSettings.Settings[identifierKey].Value;
        }
    }
}
