using System;

namespace Clicker.DI
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InjectAttribute : Attribute
    {
        public InjectAttribute()
        {
        }
    }
}