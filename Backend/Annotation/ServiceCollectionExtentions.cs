namespace Backend.Annotation
{
    public static class ServiceCollectionExtentions
    {
        /// <summary>
        /// 指定のアセンブリからコンポーネントを検索して自動登録します。
        /// </summary>
        /// <param name="services">コンポーネントを登録するServiceCollection</param>
        /// <param name="assemblyNames">コンポーネントを検索するアセンブリの名前一覧</param>
        public static IServiceCollection RegisterComponents(this IServiceCollection services, params string[] assemblyNames)
        {
            foreach (var ci in assemblyNames.SelectMany(ComponentLoader.Load))
            {
                var lifetime = ci.Scope switch
                {
                    ComponentScope.Singleton => ServiceLifetime.Singleton,
                    ComponentScope.Scoped => ServiceLifetime.Scoped,
                    ComponentScope.Transient => ServiceLifetime.Transient,
                    _ => throw new InvalidOperationException("Unknown ComponentScope value")
                };

                services.Add(new ServiceDescriptor(ci.TargetType, ci.ImplementType, lifetime));
            }

            return services;
        }
    }
}
