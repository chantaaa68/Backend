using System.Reflection;

namespace Backend.Annotation
{
    public static class ComponentLoader
    {
        public static IEnumerable<ComponentInfo> Load(string assemblyName)
        {
            try
            {
                return Load(Assembly.Load(assemblyName));
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to load assembly \"{assemblyName}\"", e);
            }
        }

        public static IEnumerable<ComponentInfo> Load(Assembly assembly)
        {
            // アセンブリ内のクラスから指定の属性を持つもののみを抽出
            return assembly.GetTypes()
                .SelectMany(type =>
                    type.GetCustomAttributes<ComponentAttribute>().Select(attr => (Type: type, Attr: attr)))
                .Select(x => new ComponentInfo(x.Attr.Scope, x.Attr.TargetType ?? x.Type, x.Type));
        }

        // ComponentAttributeの情報を保持しておくための箱
        public class ComponentInfo
        {
            public ComponentScope Scope { get; }
            public Type TargetType { get; }
            public Type ImplementType { get; }

            public ComponentInfo(ComponentScope scope, Type targetType, Type implementType)
            {
                Scope = scope;
                TargetType = targetType;
                ImplementType = implementType;

                if (!TargetType.IsAssignableFrom(ImplementType))
                {
                    throw new ArgumentException($"Type \"{TargetType.FullName}\" is not assignable from \"{ImplementType.FullName}\"");
                }
            }
        }
    }
}
