using System;
using System.Collections.Generic;
using System.Web;
using Core.Framework.MEF.Web;
using Core.Framework.Plugins.Modules;
using Core.Web.Modules;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: PreApplicationStartMethod(typeof(ContainerHttpModule), "Start")]
namespace Core.Web.Modules
{
    public class ContainerHttpModule : IHttpModule
    {
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(ContainerHttpModule));
        }

        private IEnumerable<IPluginHttpModule> modules;

        private static IEnumerable<IPluginHttpModule> RetrieveModules()
        {
            return ServiceLocator.Current.GetAllInstances<IPluginHttpModule>();
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;
            modules = RetrieveModules();
        }

        private void OnBeginRequest(object sender, EventArgs e)
        {
            if(Application.ModulesChanged)
            {
                modules = RetrieveModules();
                Application.ModulesChanged = false;
            }
            foreach (var pluginHttpModule in modules)
            {
                pluginHttpModule.OnBeginRequest(sender, e);
            }
        }

        public void Dispose()
        { }
    }
}