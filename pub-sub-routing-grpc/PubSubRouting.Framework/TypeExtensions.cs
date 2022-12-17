using System.Reflection;

namespace PubSubRouting.Framework
{
    public static class TypeExtensions
    {
        public static string? GetSimpleAssemblyQualifiedName(this Type type)
        {
            if (type.FullName == null)
            {
                return null;
            }

            return Assembly.CreateQualifiedName(AssemblyNameCache.GetName(type.Assembly).Name, type.FullName);
        }
    }
}
