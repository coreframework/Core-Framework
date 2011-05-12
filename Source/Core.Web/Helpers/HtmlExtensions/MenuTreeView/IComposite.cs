using System.Collections.Generic;

namespace Core.Web.Helpers.HtmlExtensions.MenuTreeView
{
    public interface IComposite<T>
    {
        T Parent { get; }
        IEnumerable<T> Children { get; }
    }
}