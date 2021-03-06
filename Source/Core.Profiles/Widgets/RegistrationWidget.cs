﻿using System;
using System.ComponentModel.Composition;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;
using Core.Profiles.Verbs.Widgets;

namespace Core.Profiles.Widgets
{
    [Export(typeof(ICoreWidget))]
    [Export(typeof(IPermissible))]
    public class RegistrationWidget : BaseWidget
    {
        #region Singleton

        private static readonly Lazy<RegistrationWidget> instance = new Lazy<RegistrationWidget>(() => new RegistrationWidget());

        public static RegistrationWidget Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private RegistrationWidget()
        {
        }

        #endregion

        public override ICorePlugin Plugin
        {
            get { return ProfilesPlugin.Instance; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return RegistrationWidgetViewerVerb.Instance; }

        }
        public override IWidgetActionVerb EditAction
        {
            get { return RegistrationWidgetEditorVerb.Instance; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return RegistrationWidgetSaveSettingsVerb.Instance; }
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