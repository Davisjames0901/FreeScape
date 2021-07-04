using System.Collections.Generic;

namespace FreeScape.Engine.Providers
{
    public interface IProvider<T>
    {
        IEnumerable<T> ProvideAll(string path);
    }
}