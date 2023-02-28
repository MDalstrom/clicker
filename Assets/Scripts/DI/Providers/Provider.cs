using System;
using System.Collections.Generic;
using System.Linq;

namespace Clicker.DI
{
    public class Provider : IProvider
    {
        private readonly List<object> _dependencies = new();

        public void Add(object dependency)
        {
            _dependencies.Add(dependency);
        }
        
        public object Get(Type type)
        {
            return _dependencies.FirstOrDefault(x => x.GetType().IsAssignableFrom(type));
        }
    }
}