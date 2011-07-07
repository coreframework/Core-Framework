using System;
using Framework.Core.Controllers;

namespace Core.Framework.MEF.Web
{
    public abstract class CoreWidgetController: BaseController
    {
        public abstract String ControllerWidgetIdentifier { get; }
    }
}
