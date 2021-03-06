﻿using System;
using System.ComponentModel.Composition;
using Core.Forms.Helpers;
using Core.Forms.Permissions.Operations;
using Core.Forms.Verbs.Widgets;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;

namespace Core.Forms.Widgets
{
    [Export(typeof(ICoreWidget))]
    [Export(typeof(IPermissible))]
    public class FormsBuilderWidget : BaseWidget
    {
        #region Singleton

        private static readonly Lazy<FormsBuilderWidget> instance = new Lazy<FormsBuilderWidget>(() => new FormsBuilderWidget());

        public static FormsBuilderWidget Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private FormsBuilderWidget()
        {
            Operations = OperationsHelper.GetOperations<FormsBuilderWidgetOperations>();
        }

        #endregion

        public override ICorePlugin Plugin
        {
            get { return FormsPlugin.Instance; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return FormsBuilderWidgetViewerVerb.Instance; }

        }
        public override IWidgetActionVerb EditAction
        {
            get { return FormsBuilderWidgetEditorVerb.Instance; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return FormsBuilderWidgetSaveSettingsVerb.Instance; }
        }

        public override void Remove(ICoreWidgetInstance coreWidgetInstance)
        {
            FormsBuilderWidgetHelper.Remove(coreWidgetInstance);
        }

        public override long? Clone(ICoreWidgetInstance coreWidgetInstance)
        {
            return FormsBuilderWidgetHelper.CloneFormsBuilderWidget(coreWidgetInstance);
        }
    }
}