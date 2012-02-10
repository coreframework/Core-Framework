namespace Framework.Core.Localization
{
    public interface ILocalizable<T>: ILocalizable where T: ILocale
    {
        Iesi.Collections.Generic.ISet<T> CurrentLocales { get; }
    }
}
