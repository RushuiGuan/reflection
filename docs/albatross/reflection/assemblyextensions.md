[`< Back`](../../)

---

# AssemblyExtensions

Namespace: Albatross.Reflection

Provides extension methods for Assembly objects to facilitate type discovery and resource management.

```csharp
public static class AssemblyExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [AssemblyExtensions](./albatross/reflection/assemblyextensions)<br>
Attributes [NullableContextAttribute](./system/runtime/compilerservices/nullablecontextattribute), [NullableAttribute](./system/runtime/compilerservices/nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **GetConcreteClasses&lt;T&gt;(Assembly)**

Return all concrete classes that derive from base class T in an assembly

```csharp
public static IEnumerable<Type> GetConcreteClasses<T>(Assembly assembly)
```

#### Type Parameters

`T`<br>
The base type to filter by

#### Parameters

`assembly` [Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly)<br>
The assembly to search

#### Returns

[IEnumerable&lt;Type&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
An enumerable of concrete types that derive from T

### **GetConcreteClasses(Assembly)**

Return all Concrete classes in an assembly

```csharp
public static IEnumerable<Type> GetConcreteClasses(Assembly assembly)
```

#### Parameters

`assembly` [Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly)<br>
The assembly to search

#### Returns

[IEnumerable&lt;Type&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
An enumerable of all concrete types in the assembly

### **GetEmbeddedFile(Type, String, String)**

Retrieves the content of an embedded resource file from the assembly.
 Uses the DefaultNamespaceAttribute if available, otherwise falls back to assembly name.

```csharp
public static string GetEmbeddedFile(Type type, string name, string folder)
```

#### Parameters

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
A type from the assembly containing the embedded resource

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the embedded file

`folder` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The folder path within the assembly resources (default: "Embedded")

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The content of the embedded file as a string

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown when the specified resource doesn't exist

**Remarks:**

This method constructs the resource name as: {namespace}.{folder}.{name}
 If the assembly name doesn't match the default namespace, use DefaultNamespaceAttribute.

---

[`< Back`](../../)
