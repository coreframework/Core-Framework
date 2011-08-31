using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Core.Framework.MEF.Web;
using Core.Framework.Plugins.Handlers;
using Framework.Core;
using Framework.Core.Utilities;
using Microsoft.Practices.ServiceLocation;
using Application = Core.Framework.MEF.Web.Application;

namespace Core.Web.Modules
{
    /// <summary>
    /// Module checks every re
    /// </summary>
    public class HttpHandlersManagerModule: IHttpModule
    {
        private const String AsterixExpression = "*";

        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequestHandler;
        }

        private static void BeginRequestHandler(object sender, EventArgs e)
        {
            var pluginHelper = ServiceLocator.Current.GetInstance<IPluginHelper>();
            var installedPlugins = pluginHelper.GetInstalledPlugins();

            //find any http handlers registered in modules and check if request is qualified for any handler.)
            foreach (PluginHttpHandler handler in
                installedPlugins.Where(plugin => plugin.PluginSetting.HttpHandlers != null).SelectMany(
                        plugin => plugin.PluginSetting.HttpHandlers))
            {
                if (handler.Verb.Trim().Equals(AsterixExpression) || handler.Verb.ToLower().Contains(HttpContext.Current.Request.RequestType.ToLower()))
                {
                    if (Regex.IsMatch(ApplicationUtility.GetUrlPath(HttpContext.Current.Request.RawUrl), RegexConverter.FilterToRegex(handler.Path), RegexOptions.IgnoreCase))
                    {
                        //get handler's type and check if this type inherited from IHttpHandler interface.
                        var handlerType = Type.GetType(handler.HandlerType);
                        if (handlerType.GetInterfaces().Contains(typeof(IHttpHandler)))
                        {
                            HttpContext.Current.RemapHandler((IHttpHandler)Activator.CreateInstance(handlerType));
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            
        }
    }
}