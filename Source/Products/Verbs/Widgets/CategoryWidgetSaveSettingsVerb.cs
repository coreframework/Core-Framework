﻿using System;
using Core.Framework.Plugins.Web;

namespace Products.Verbs.Widgets
{
    public class CategoryWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static CategoryWidgetSaveSettingsVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static CategoryWidgetSaveSettingsVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new CategoryWidgetSaveSettingsVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public string Action
        {
            get { return "UpdateWidget"; }
        }

        public string Controller
        {
            get { return "CategoryViewerWidget"; }
        }

        public string Area
        {
            get { return "Product"; }
        }

        #endregion
    }
}