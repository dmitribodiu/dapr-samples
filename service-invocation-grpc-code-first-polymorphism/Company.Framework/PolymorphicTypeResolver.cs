using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Company.Framework
{
    public class PolymorphicTypeResolver : DefaultJsonTypeInfoResolver
    {
        public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
        {
            var typeInfo = base.GetTypeInfo(type, options);

            if (type.IsAbstract && type.IsClass)
            {
                var derivedTypes = typeInfo.Type.Assembly.GetTypes()
                    .Where(t => !t.IsAbstract && t.IsClass && typeInfo.Type.IsAssignableFrom(t))
                    .Select(t => new JsonDerivedType(t, t.FullName))
                    .ToList();

                if (derivedTypes.Any())
                {
                    typeInfo.PolymorphismOptions = new();

                    foreach (var derivedType in derivedTypes)
                    {
                        typeInfo.PolymorphismOptions.DerivedTypes.Add(derivedType);
                    }
                }
            }
            else if (!type.IsAbstract && type.IsClass && type.BaseType != null &&
                type.BaseType.IsAbstract && type.BaseType.IsClass)
            {
                typeInfo.PolymorphismOptions = new()
                {
                    DerivedTypes =
                    {
                        new(type, type.FullName)
                    }
                };
            }

            return typeInfo;
        }
    }
}
