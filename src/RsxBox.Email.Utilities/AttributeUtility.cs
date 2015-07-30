using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RsxBox.Email.Utilities
{
    public static class AttributeUtility
    {
        public static IEnumerable<PropertyInfo> GetProperties(Type t, Type attribute)
        {
            
            IEnumerable<PropertyInfo> props = t.GetProperties().Where(
                prop => Attribute.IsDefined(prop, attribute));

            return props; 
            
        }
    }
}
