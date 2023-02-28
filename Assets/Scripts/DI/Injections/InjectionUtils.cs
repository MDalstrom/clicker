using System;
using System.Reflection;

namespace Clicker.DI
{
    public static class InjectionUtils
    {
        public static void InjectFields(this IProvider provider, object target)
        {
            foreach (var field in target.GetType().GetFields(BindingFlags.Instance))
            {
                if (field.GetValue(target) is not null)
                    continue;

                var customAttribute = Attribute.GetCustomAttribute(field, typeof(InjectAttribute));
                if (customAttribute is not InjectAttribute)
                    continue;
                
                var dependency = provider.Get(field.FieldType);
                field.SetValue(target, dependency);
            }
        }
    }
}