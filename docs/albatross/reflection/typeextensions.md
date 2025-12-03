[`< Back`](../../)

---

# TypeExtensions

Namespace: Albatross.Reflection

Provides extension methods for Type objects to facilitate type inspection, property manipulation, and generic type operations.
 Includes utilities for nullable types, collections, reflection-based property access, and type compatibility checks.

```csharp
public static class TypeExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TypeExtensions](./albatross/reflection/typeextensions)<br>
Attributes [NullableContextAttribute](./system/runtime/compilerservices/nullablecontextattribute), [NullableAttribute](./system/runtime/compilerservices/nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **GetNullableValueType(Type, Type&)**

Determines if the specified type is a nullable value type and extracts the underlying value type.

```csharp
public static bool GetNullableValueType(Type nullableType, Type& valueType)
```

#### Parameters

`nullableType` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The type to check

`valueType` [Type&](https://docs.microsoft.com/en-us/dotnet/api/system.type&)<br>
When this method returns true, contains the underlying value type of the nullable type

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the type is Nullable&lt;T&gt;; otherwise, false

### **GetTaskResultType(Type, Type&)**

Determines if the specified type is a Task&lt;T&gt; and extracts the result type.

```csharp
public static bool GetTaskResultType(Type taskType, Type& resultType)
```

#### Parameters

`taskType` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The type to check

`resultType` [Type&](https://docs.microsoft.com/en-us/dotnet/api/system.type&)<br>
When this method returns true, contains the result type of the Task&lt;T&gt;

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the type is Task&lt;T&gt;; otherwise, false

### **GetCollectionElementType(Type, Type&)**

#### Caution

Use TryGetCollectionElementType instead.

---

Determines if the specified type is a collection and extracts the element type.
 Supports arrays, generic collections implementing IEnumerable&lt;T&gt;, and non-generic enumerables.

```csharp
public static bool GetCollectionElementType(Type collectionType, Type& elementType)
```

#### Parameters

`collectionType` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The type to check

`elementType` [Type&](https://docs.microsoft.com/en-us/dotnet/api/system.type&)<br>
When this method returns true, contains the element type of the collection

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the type is a collection type; otherwise, false

**Remarks:**

String types are not considered collections and will return false.

### **TryGetCollectionElementType(Type, Type&)**

#### Caution

Use TryGetGenericCollectionElementType instead.

---

Determines if the specified type is a collection and extracts the element type.
 Supports arrays, generic collections implementing IEnumerable&lt;T&gt;, and non-generic enumerables.

```csharp
public static bool TryGetCollectionElementType(Type collectionType, Type& elementType)
```

#### Parameters

`collectionType` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The type to check

`elementType` [Type&](https://docs.microsoft.com/en-us/dotnet/api/system.type&)<br>
When this method returns true, contains the element type of the collection

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the type is a collection type; otherwise, false

**Remarks:**

String types are not considered collections and will return false.

### **IsCollectionType(Type)**

Determines whether the specified type represents a collection type.

```csharp
public static bool IsCollectionType(Type type)
```

#### Parameters

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The type to check

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the type implements IEnumerable and is not a string; otherwise, false

**Remarks:**

String types are not considered collections and will return false.

### **TryGetGenericCollectionElementType(Type, Type&)**

Determines if the specified type is a generic collection and extracts the element type.
 Supports arrays and generic collections implementing IEnumerable&lt;T&gt;.

```csharp
public static bool TryGetGenericCollectionElementType(Type collectionType, Type& elementType)
```

#### Parameters

`collectionType` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The type to check

`elementType` [Type&](https://docs.microsoft.com/en-us/dotnet/api/system.type&)<br>
When this method returns true, contains the element type of the collection

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the type is a generic collection type; otherwise, false

**Remarks:**

String types and non-generic enumerables are not supported and will return false.

### **GetGenericTypeName(String)**

Extracts the generic type name by removing the backtick and type parameter count.

```csharp
public static string GetGenericTypeName(string name)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The full generic type name (e.g., "List`1")

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The generic type name without the parameter count (e.g., "List")

### **GetClassNameNeat(Type)**

Creates a neat class name combining the full type name with the assembly name.

```csharp
public static string GetClassNameNeat(Type type)
```

#### Parameters

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The type to get the neat name for

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A string in the format "FullName,AssemblyName"

### **IsAnonymousType(Type)**

Check if a type is anoymous

```csharp
public static bool IsAnonymousType(Type type)
```

#### Parameters

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsConcreteType(Type)**

Determines whether the specified type is a concrete class (not abstract, not interface, not generic type definition).

```csharp
public static bool IsConcreteType(Type type)
```

#### Parameters

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The type to check

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the type is a concrete class; otherwise, false

### **IsDerived&lt;T&gt;(Type)**

Determines whether the specified type is derived from or implements the generic type T.

```csharp
public static bool IsDerived<T>(Type type)
```

#### Type Parameters

`T`<br>
The base type or interface to check against

#### Parameters

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The type to check

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the type derives from or implements type T; otherwise, false

### **IsDerived(Type, Type)**

Determines whether the specified type is derived from or implements the specified base type.

```csharp
public static bool IsDerived(Type type, Type baseType)
```

#### Parameters

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The type to check

`baseType` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The base type or interface to check against

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the type derives from or implements the base type; otherwise, false

### **TryGetClosedGenericType(Type, Type, Type&)**

Provide with a class type and a generic type\interface definition, this methods will return true if the class derives\implements the generic type\interface. It will
 also output the generic type.
 
 Example:
 public class Test: IEnumerable&lt;int&gt; { }
 
 var result = typeof(Test).TryGetClosedGenericTypes(typeof(IEnumerable&lt;&gt;), out Type type);
 Assert.True(result);
 Assert.AreSame(typeof(IEnumerable&lt;int&gt;), type);

```csharp
public static bool TryGetClosedGenericType(Type type, Type genericDefinition, Type& genericType)
```

#### Parameters

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
Input class type

`genericDefinition` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The definition of a generic type. For example: typeof(IEnumerable&lt;&gt;)

`genericType` [Type&](https://docs.microsoft.com/en-us/dotnet/api/system.type&)<br>
If the class extends\implements the generic type\interface, its type will be set in this output parameter

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Return true if the class implements the generic interface

### **GetRequiredType(String)**

Gets a Type by its name, throwing an ArgumentException if the type is not found.
 Unlike Type.GetType, this method provides better error handling for missing types.

```csharp
public static Type GetRequiredType(string className)
```

#### Parameters

`className` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The fully qualified name of the type to retrieve

#### Returns

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The Type object for the specified class name

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown when the class name is null, empty, or the type cannot be found

### **IsNullableValueType(Type)**

Determines whether the specified type is a nullable value type (Nullable&lt;T&gt;).

```csharp
public static bool IsNullableValueType(Type type)
```

#### Parameters

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The type to check

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the type is Nullable&lt;T&gt;; otherwise, false

### **IsNumericType(Type)**

Determines whether the specified type represents a numeric type.

```csharp
public static bool IsNumericType(Type type)
```

#### Parameters

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The type to check

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the type is a numeric type (byte, int, float, decimal, etc.); otherwise, false

### **GetTypeNameWithoutAssemblyVersion(Type)**

Gets the type name with assembly name but without version information.

```csharp
public static string GetTypeNameWithoutAssemblyVersion(Type type)
```

#### Parameters

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The type to get the name for

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A string in the format "FullName, AssemblyName" without version details

### **GetAssemblyLocation(Assembly, String)**

Gets the file system location of an assembly combined with the specified path.

```csharp
public static string GetAssemblyLocation(Assembly asm, string path)
```

#### Parameters

`asm` [Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly)<br>
The assembly to get the location for

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The relative path to combine with the assembly location

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The combined path of the assembly location and the specified path

#### Exceptions

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>
Thrown when the assembly location cannot be determined

### **GetAssemblyDirectoryLocation(Assembly, String)**

Gets a DirectoryInfo object representing the assembly location combined with the specified path.

```csharp
public static DirectoryInfo GetAssemblyDirectoryLocation(Assembly asm, string path)
```

#### Parameters

`asm` [Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly)<br>
The assembly to get the location for

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The relative path to combine with the assembly location

#### Returns

[DirectoryInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.directoryinfo)<br>
A DirectoryInfo object for the combined path

### **GetAssemblyFileLocation(Assembly, String)**

Gets a FileInfo object representing the assembly location combined with the specified path.

```csharp
public static FileInfo GetAssemblyFileLocation(Assembly asm, string path)
```

#### Parameters

`asm` [Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly)<br>
The assembly to get the location for

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The relative path to combine with the assembly location

#### Returns

[FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>
A FileInfo object for the combined path

### **GetPropertyValue(Type, Object, String, Boolean)**

Gets the value of a property from an object using reflection.
 Supports nested property access using dot notation (e.g., "Property.SubProperty").
 Supports indexed property access using square bracket notation (e.g., "Property[index]" or "Property[key]").

```csharp
public static object GetPropertyValue(Type type, object data, string name, bool ignoreCase)
```

#### Parameters

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The type of the object

`data` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The object to get the property value from

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The property name, which can include nested properties separated by dots and indexed properties with square brackets

`ignoreCase` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether to ignore case when matching property names

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The value of the property, or null if the object or any nested property is null

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown when the specified property is not found

### **GetPropertyType(Type, String, Boolean)**

Gets the Type of a property using reflection.
 Supports nested property access using dot notation (e.g., "Property.SubProperty").

```csharp
public static Type GetPropertyType(Type type, string name, bool ignoreCase)
```

#### Parameters

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The type containing the property

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The property name, which can include nested properties separated by dots

`ignoreCase` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether to ignore case when matching property names

#### Returns

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The Type of the specified property

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown when the specified property is not found

### **GetPropertyValue(Type, Object, String, Boolean, Type&)**

Gets the value of a property from an object using reflection and returns the property type.
 Supports nested property access using dot notation (e.g., "Property.SubProperty").
 Supports indexed property access using square bracket notation (e.g., "Property[index]" or "Property[key]").

```csharp
public static object GetPropertyValue(Type type, object data, string name, bool ignoreCase, Type& propertyType)
```

#### Parameters

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The type of the object

`data` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The object to get the property value from

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The property name, which can include nested properties separated by dots and indexed properties with square brackets

`ignoreCase` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether to ignore case when matching property names

`propertyType` [Type&](https://docs.microsoft.com/en-us/dotnet/api/system.type&)<br>
When this method returns, contains the type of the final property accessed

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The value of the property, or null if the object or any nested property is null

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown when the specified property is not found

### **SetPropertyValue(Type, Object, String, Object, Boolean)**

Sets the value of a property on an object using reflection.
 Supports nested property access using dot notation (e.g., "Property.SubProperty").

```csharp
public static void SetPropertyValue(Type type, object data, string propertyName, object value, bool ignoreCase)
```

#### Parameters

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The type of the object

`data` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The object to set the property value on

`propertyName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The property name, which can include nested properties separated by dots

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The value to set

`ignoreCase` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether to ignore case when matching property names

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown when the specified property is not found or doesn't have appropriate getter/setter

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when attempting to set a property on a null object

---

[`< Back`](../../)
