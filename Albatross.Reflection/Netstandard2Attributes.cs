// ReSharper disable CheckNamespace
#if NETSTANDARD2_0
namespace System.Diagnostics.CodeAnalysis {
    internal sealed class NotNullAttribute : Attribute {
    }
    internal  sealed class NotNullWhenAttribute : Attribute {
        public NotNullWhenAttribute(bool returnValue) => ReturnValue = returnValue;
        public bool ReturnValue { get; }
    }
    internal  sealed class NotNullIfNotNullAttribute : Attribute {
        public NotNullIfNotNullAttribute(string parameterName) => ParameterName = parameterName;
        public string ParameterName { get; }
    }
}
#endif
