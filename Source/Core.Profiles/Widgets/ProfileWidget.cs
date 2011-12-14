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
    public class ProfileWidget : BaseWidget
    {
        #region Singleton

        private static ProfileWidget instance;

        private static readonly Object syncRoot = new Object();

        public static ProfileWidget Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new ProfileWidget());
                }
            }
        }

        private ProfileWidget()
        {

        }

        #endregion

        public override ICorePlugin Plugin
        {
            get { return ProfilesPlugin.Instance; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return ProfileWidgetViewerVerb.Instance; }

        }
        public override IWidgetActionVerb EditAction
        {
            get { return ProfileWidgetEditorVerb.Instance; ; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return ProfileWidgetSaveSettingsVerb.Instance; ; }
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