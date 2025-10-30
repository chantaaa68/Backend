namespace Backend.Annotation
{
    /// <summary>
    /// <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection" />にコンポーネントを自動登録するための属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class AutoDIAttribute : Attribute
    {
        /// <summary>
        /// コンポーネントのライフサイクル。未設定の場合は<see cref="AutoDIScope.Scoped"/>。
        /// </summary>
        public AutoDIScope Scope { get; set; } = AutoDIScope.Scoped;

        /// <summary>
        /// コンポーネントの登録対象の型。未設定の場合はコンポーネント自身の型。
        /// </summary>
        public Type? TargetType { get; set; }

        public AutoDIAttribute() { }

        public AutoDIAttribute(AutoDIScope scope, Type? targetType = null)
        {
            this.Scope = scope;
            this.TargetType = targetType;
        }
    }
}
