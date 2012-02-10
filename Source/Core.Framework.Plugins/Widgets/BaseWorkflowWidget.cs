using System.Collections.Generic;
using Core.Framework.Plugins.Web;

namespace Core.Framework.Plugins.Widgets
{
    public abstract class BaseWorkflowWidget : BaseWidget
    {
        protected readonly IList<ICorePlugin> innerPlugins = new List<ICorePlugin>();

        public IList<ICorePlugin> InnerPlugins
        {
            get { return innerPlugins; }
        }
    }
}
