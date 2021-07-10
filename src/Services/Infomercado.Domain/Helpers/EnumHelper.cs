using System;
using System.ComponentModel.DataAnnotations;

namespace Infomercado.Domain.Helpers
{
    public static class EnumHelper<T>
    {
        public static T? GetValueFromName(string name)
        {
            var type = typeof (T);
            if (!type.IsEnum) throw new InvalidOperationException();

            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof (DisplayAttribute)) is DisplayAttribute attribute)
                {
                    if (string.Equals(attribute.Name, name, StringComparison.OrdinalIgnoreCase))
                        return (T) field.GetValue(null);
                }
                else
                    if (string.Equals(field.Name, name, StringComparison.InvariantCultureIgnoreCase))
                        return (T) field.GetValue(null);
            }

            return default;
        }
    }
}