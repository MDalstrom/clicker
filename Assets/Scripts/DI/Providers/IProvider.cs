using System;

namespace Clicker.DI
{
    public interface IProvider
    {
        object Get(Type dependencyType);
    }
}