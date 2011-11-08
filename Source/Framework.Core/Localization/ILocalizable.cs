using System.Collections.Generic;

namespace Framework.Core.Localization
{
    public interface ILocalizable<T>: ILocalizable where T: ILocale
    {
        IList<T> CurrentLocales { get; set; }
    }
}
