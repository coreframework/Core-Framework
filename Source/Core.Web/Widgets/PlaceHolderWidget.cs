﻿using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Web;
using System.Xml.Serialization;
using Core.Framework.Plugins.Configs;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;
using Core.Web.Verbs;

namespace Core.Web.Widgets
{
    [Export(typeof(ICoreWidget))]
    public class PlaceHolderWidget : BaseWidget
    {
        #region Fields

        private IWidgetSetting widgetSetting;
        private const String WidgetConfig = "~/Config/PlaceHolderWidgetConfig.xml";

        #endregion


        #region Singleton

        private static readonly Lazy<PlaceHolderWidget> instance = new Lazy<PlaceHolderWidget>(() => new PlaceHolderWidget());

        public static PlaceHolderWidget Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private PlaceHolderWidget()
        {
        }

        #endregion

        public override ICorePlugin Plugin
        {
            get { return null; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return PlaceHolderWidgetViewVerb.Instance; }

        }

        public override IWidgetActionVerb EditAction
        {
            get { return null; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return null; }
        }

        public override bool IsPlaceHolder
        {
            get { return true; }
        }

        public override IWidgetSetting WidgetSetting
        {
            get
            {
                if(widgetSetting == null)
                {
                    widgetSetting = GetSettings(HttpContext.Current.Server.MapPath(WidgetConfig));
                }
                return widgetSetting;
            }
        }

        private static IWidgetSetting GetSettings(String configPath)
        {
            try
            {
                WidgetSetting doc;
                var serializer = new XmlSerializer(typeof(WidgetSetting));
                using (var reader = new FileStream(configPath, FileMode.Open))
                {
                    doc = serializer.Deserialize(reader) as WidgetSetting;
                }
                return doc ?? new WidgetSetting();
            }
            catch (Exception)
            {
                return new WidgetSetting();
            }
        }
    }
}