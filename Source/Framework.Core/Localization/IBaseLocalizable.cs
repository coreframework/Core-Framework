using System.Collections.Generic;

namespace Framework.Core.Localization
{
    public interface ILocalizable
    {
        ILocale CurrentLocale { get; }
        bool ContainsLocale(ILocale locale);
        void AddLocale(ILocale locale);
    }
}
