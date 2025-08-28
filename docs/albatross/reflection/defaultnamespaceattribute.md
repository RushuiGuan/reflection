[`< Back`](../../)

---

# DefaultNamespaceAttribute

Namespace: Albatross.Reflection

Use this attribute to specify the default namespace for embedded resources. 
 Useful when the assembly name doesn't match the default namespace name.

```csharp
public class DefaultNamespaceAttribute : System.Attribute
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Attribute](https://docs.microsoft.com/en-us/dotnet/api/system.attribute) → [DefaultNamespaceAttribute](./albatross/reflection/defaultnamespaceattribute)<br>
Attributes [NullableContextAttribute](./system/runtime/compilerservices/nullablecontextattribute), [NullableAttribute](./system/runtime/compilerservices/nullableattribute), [AttributeUsageAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.attributeusageattribute)

## Properties

### **DefaultNamespace**

Gets the default namespace for embedded resources.

```csharp
public string DefaultNamespace { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **TypeId**

```csharp
public object TypeId { get; }
```

#### Property Value

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

## Constructors

### **DefaultNamespaceAttribute(String)**

Initializes a new instance of the DefaultNamespaceAttribute class.

```csharp
public DefaultNamespaceAttribute(string defaultNamespace)
```

#### Parameters

`defaultNamespace` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The default namespace to use for embedded resources

---

[`< Back`](../../)
