using System;
using Framework.Core.Controllers;

namespace Core.Framework.MEF.Web
{
    public abstract class CorePluginController: BaseController
    {
        public abstract String ControllerPluginIdentifier { get; }
    }
}
