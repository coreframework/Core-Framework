using System;
using System.Collections.Generic;

namespace Framework.Core.Localization
{
    public interface ILocalizable
    {
        //Type LocaleType { get; }
        IList<ILocale> CurrentLocales { get; set; }
        ILocale CurrentLocale { get;}
    }
}
