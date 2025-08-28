[`< Back`](../../)

---

# Enumerations

Namespace: Albatross.Reflection

Provides utility methods for enumerating and flattening object properties into dictionaries.
 Supports nested objects, arrays, and collections with hierarchical path-based keys.

```csharp
public static class Enumerations
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Enumerations](./albatross/reflection/enumerations)<br>
Attributes [NullableContextAttribute](./system/runtime/compilerservices/nullablecontextattribute), [NullableAttribute](./system/runtime/compilerservices/nullableattribute)

## Methods

### **Property(Object, String, Nullable&lt;Int32&gt;, Dictionary&lt;String, Object&gt;)**

Flatten an object into a dictionary. The key is the path to the property. The value is the value of the property.
 If a property is an array, then the index with square brackets is appended to the path.

```csharp
public static void Property(object value, string path, Nullable<int> index, Dictionary<string, object> result)
```

#### Parameters

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The object to flatten. Can be a primitive type, string, complex object, array, or collection

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The current property path. Used for nested objects and arrays

`index` [Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
The array index when processing array elements. Used to generate indexed keys like "path[0]"

`result` [Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
The dictionary to store the flattened key-value pairs

**Remarks:**

Null values are skipped. Arrays and collections are recursively processed with indexed paths.
 Complex objects have their public instance properties enumerated recursively.

---

[`< Back`](../../)
