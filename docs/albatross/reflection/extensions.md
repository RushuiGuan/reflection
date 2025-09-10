[`< Back`](../../)

---

# Extensions

Namespace: Albatross.Reflection

Provides utility methods for enumerating and flattening object properties into dictionaries.
 Supports nested objects, arrays, and collections with hierarchical path-based keys.

```csharp
public static class Extensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Extensions](./albatross/reflection/extensions)<br>
Attributes [NullableContextAttribute](./system/runtime/compilerservices/nullablecontextattribute), [NullableAttribute](./system/runtime/compilerservices/nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **ToDictionary(Object, Dictionary&lt;String, Object&gt;)**

Flatten an object into a dictionary. The key is the path to the property. The value is the value of the property.
 If a property is an array, then the index with square brackets is appended to the path.

```csharp
public static void ToDictionary(object value, Dictionary<string, object> result)
```

#### Parameters

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The object to flatten. Can be a primitive type, string, complex object, array, or collection

`result` [Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
The dictionary to store the flattened key-value pairs

**Remarks:**

Null values are skipped. Arrays and collections are recursively processed with indexed paths.
 Complex objects have their public instance properties enumerated recursively.

---

[`< Back`](../../)
