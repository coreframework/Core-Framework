using System;
using System.Web.Mvc;

namespace Core.Framework.MEF.Web
{
    public abstract class CoreController : Controller
    {
        public abstract String ControllerPluginIdentifier { get; }
    }
}
