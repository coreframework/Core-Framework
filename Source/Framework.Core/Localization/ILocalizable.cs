using System.Collections.Generic;

namespace Framework.Core.Localization
{
    public interface ILocalizable<T> where T: ILocale
    {
        IList<T> CurrentLocales { get; set; }
        ILocale CurrentLocale { get;}
    }
}
