using System.Collections.Generic;

namespace Framework.Core.Localization
{
    public interface ILocalizable
    {
        IList<ILocale> CurrentLocales { get; set; }
        ILocale CurrentLocale { get;}
    }
}
