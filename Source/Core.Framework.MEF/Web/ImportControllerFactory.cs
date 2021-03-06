﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using Core.Framework.MEF.Composition;
using Core.Framework.MEF.Contracts.Web;
using Microsoft.Practices.ServiceLocation;

namespace Core.Framework.MEF.Web
{
    /// <summary>
    /// Provides creation of controllers using imports.
    /// </summary>
    [Export]
    public class ImportControllerFactory : DefaultControllerFactory
    {
        #region Fields

        [ImportMany]
#pragma warning disable 649
        private IEnumerable<PartFactory<IController, IControllerMetadata>> controllerFactories;
#pragma warning restore 649

        #endregion

        #region Methods
        /// <summary>
        /// Creates an instance of a controller for the specified name.
        /// </summary>
        /// <param name="requestContext">The current request context.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <returns>An instance of a controller for the specified name.</returns>
        public override IController CreateController(System.Web.Routing.RequestContext requestContext, String controllerName)
        {
            object areaNameObj = requestContext.RouteData.Values["area"];
            if (String.IsNullOrEmpty(areaNameObj as String))
            {
                requestContext.RouteData.DataTokens.TryGetValue("area", out areaNameObj);
            }
            var areaName = areaNameObj as String;
            PartFactory<IController, IControllerMetadata> factory = null;
            var matchedFactories = controllerFactories.Where(
                        f => f.Metadata.Name.Equals(controllerName, StringComparison.InvariantCultureIgnoreCase));
            var matchedFactoriesCount = matchedFactories.Count();
            if (matchedFactoriesCount > 1)
            {
                factory = controllerFactories
                    .Where(
                        f =>
                        f.Metadata.Name.Equals(controllerName, StringComparison.InvariantCultureIgnoreCase) &&
                        (String.IsNullOrEmpty(areaName) || areaName.Equals(f.Metadata.Area))).FirstOrDefault();
            }
            if (factory == null && matchedFactoriesCount > 0)
            {
                factory = matchedFactories.FirstOrDefault();
            }
            if (factory != null)
            {
                IController controller = factory.CreatePart();
                if (controller != null)
                {
                    if (controller is CorePluginController)
                    {
                        IPluginHelper pluginHelper = ServiceLocator.Current.GetInstance<IPluginHelper>();
                        if (pluginHelper.IsPluginEnabled(((CorePluginController)controller).ControllerPluginIdentifier))
                        {
                            return controller;
                        }
                    }
                    else if (controller is CoreWidgetController)
                    {
                        IWidgetHelper widgetHelper = ServiceLocator.Current.GetInstance<IWidgetHelper>();
                        if (widgetHelper.IsWidgetEnabled(((CoreWidgetController)controller).ControllerWidgetIdentifier))
                        {
                            return controller;
                        }
                    }
                    else return controller;
                }
            }

            return base.CreateController(requestContext, controllerName);
        }

        #endregion
    }
}

