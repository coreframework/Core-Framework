using System;
using System.Web.Mvc;

namespace Core.Framework.MEF.Web
{
    public abstract class CoreWidgetController: Controller
    {
        public abstract String ControllerWidgetIdentifier { get; }
    }
}
