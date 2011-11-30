using System;
using System.ComponentModel.Composition;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;
using Core.Profiles.Verbs.Widgets;

namespace Core.Profiles.Widgets
{
    [Export(typeof(ICoreWidget))]
    [Export(typeof(IPermissible))]
    public class LoginWidget : BaseWidget
    {
        #region Singleton

        private static LoginWidget instance;

        private static readonly Object syncRoot = new Object();

        public static LoginWidget Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new LoginWidget());
                }
            }
        }

        private LoginWidget()
        {
            
        }

        #endregion

        public override ICorePlugin Plugin
        {
            get { return ProfilesPlugin.Instance; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return LoginWidgetViewerVerb.Instance; }

        }
        public override IWidgetActionVerb EditAction
        {
            get { return null; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return null; }
        }

      /*  public override void Remove(ICoreWidgetInstance coreWidgetInstance)
        {
            WebContentWidgetHelper.Remove(coreWidgetInstance);
        }

        public override long? Clone(ICoreWidgetInstance coreWidgetInstance)
        {
            return WebContentWidgetHelper.CloneWebContentWidget(coreWidgetInstance);
        }*/
    }
}