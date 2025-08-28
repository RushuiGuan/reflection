[`< Back`](../../)

---

# ExpressionExtensions

Namespace: Albatross.Reflection

Provides extension methods for working with LINQ expressions, particularly for property access and manipulation.
 Enables compile-time safe property operations using strongly-typed lambda expressions.

```csharp
public static class ExpressionExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ExpressionExtensions](./albatross/reflection/expressionextensions)<br>
Attributes [NullableContextAttribute](./system/runtime/compilerservices/nullablecontextattribute), [NullableAttribute](./system/runtime/compilerservices/nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **GetPropertyInfo&lt;T&gt;(Expression&lt;Func&lt;T, Object&gt;&gt;)**

Extracts PropertyInfo from a lambda expression that accesses a property.

```csharp
public static PropertyInfo GetPropertyInfo<T>(Expression<Func<T, object>> lambda)
```

#### Type Parameters

`T`<br>
The type containing the property

#### Parameters

`lambda` Expression&lt;Func&lt;T, Object&gt;&gt;<br>
Lambda expression that accesses a property

#### Returns

[PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo)<br>
The PropertyInfo for the accessed property

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown when the expression doesn't refer to a property

### **GetPropertyInfo&lt;T, V&gt;(Expression&lt;Func&lt;T, V&gt;&gt;)**

Extracts PropertyInfo from a strongly-typed lambda expression that accesses a property.

```csharp
public static PropertyInfo GetPropertyInfo<T, V>(Expression<Func<T, V>> lambda)
```

#### Type Parameters

`T`<br>
The type containing the property

`V`<br>
The type of the property value

#### Parameters

`lambda` Expression&lt;Func&lt;T, V&gt;&gt;<br>
Lambda expression that accesses a property

#### Returns

[PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo)<br>
The PropertyInfo for the accessed property

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown when the expression doesn't refer to a property

### **GetPredicate&lt;T&gt;(String, Object)**

Creates a predicate expression that checks equality between a member and a value.
 The member can be a property or field of the specified type.

```csharp
public static Expression<Func<T, bool>> GetPredicate<T>(string propertyOrFieldName, object value)
```

#### Type Parameters

`T`<br>
The type containing the member

#### Parameters

`propertyOrFieldName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the property or field to compare

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The value to compare against

#### Returns

Expression&lt;Func&lt;T, Boolean&gt;&gt;<br>
A predicate expression that evaluates to true when the member equals the value

### **SetValueIfNotNull&lt;T, V&gt;(T, Expression&lt;Func&lt;T, V&gt;&gt;, Nullable&lt;V&gt;)**

Sets a property value only if the provided nullable struct value has a value.
 Uses lambda expression for compile-time safety and property access.

```csharp
public static T SetValueIfNotNull<T, V>(T ob, Expression<Func<T, V>> lambda, Nullable<V> value)
```

#### Type Parameters

`T`<br>
The type of the object

`V`<br>
The value type of the property

#### Parameters

`ob` T<br>
The object to modify

`lambda` Expression&lt;Func&lt;T, V&gt;&gt;<br>
Lambda expression that identifies the property to set

`value` Nullable&lt;V&gt;<br>
The nullable value to set if it has a value

#### Returns

T<br>
The original object for method chaining

### **SetValueIfNotNull&lt;T, V&gt;(T, Expression&lt;Func&lt;T, Nullable&lt;V&gt;&gt;&gt;, Nullable&lt;V&gt;)**

Sets a nullable property value only if the provided nullable struct value has a value.
 Uses lambda expression for compile-time safety and property access.

```csharp
public static T SetValueIfNotNull<T, V>(T ob, Expression<Func<T, Nullable<V>>> lambda, Nullable<V> value)
```

#### Type Parameters

`T`<br>
The type of the object

`V`<br>
The value type of the nullable property

#### Parameters

`ob` T<br>
The object to modify

`lambda` Expression&lt;Func&lt;T, Nullable&lt;V&gt;&gt;&gt;<br>
Lambda expression that identifies the nullable property to set

`value` Nullable&lt;V&gt;<br>
The nullable value to set if it has a value

#### Returns

T<br>
The original object for method chaining

### **SetTextIfNotEmpty&lt;T&gt;(T, Expression&lt;Func&lt;T, String&gt;&gt;, String)**

Sets a string property value only if the provided string is not null or empty.
 Uses lambda expression for compile-time safety and property access.

```csharp
public static T SetTextIfNotEmpty<T>(T obj, Expression<Func<T, string>> lambda, string value)
```

#### Type Parameters

`T`<br>
The type of the object

#### Parameters

`obj` T<br>
The object to modify

`lambda` Expression&lt;Func&lt;T, String&gt;&gt;<br>
Lambda expression that identifies the string property to set

`value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The string value to set if it's not null or empty

#### Returns

T<br>
The original object for method chaining

### **SetValueIfHasKey&lt;T, V, K&gt;(T, Expression&lt;Func&lt;T, V&gt;&gt;, Nullable&lt;K&gt;, Func&lt;K, Task&lt;V&gt;&gt;)**

Asynchronously sets a property value if the provided key has a value, using a function to retrieve the value.
 Uses lambda expression for compile-time safety and property access.

```csharp
public static Task SetValueIfHasKey<T, V, K>(T ob, Expression<Func<T, V>> lambda, Nullable<K> key, Func<K, Task<V>> func)
```

#### Type Parameters

`T`<br>
The type of the object

`V`<br>
The type of the property value

`K`<br>
The type of the key parameter

#### Parameters

`ob` T<br>
The object to modify

`lambda` Expression&lt;Func&lt;T, V&gt;&gt;<br>
Lambda expression that identifies the property to set

`key` Nullable&lt;K&gt;<br>
The nullable key value

`func` Func&lt;K, Task&lt;V&gt;&gt;<br>
Async function that retrieves the value using the key

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
A task representing the async operation

### **SetValueIfHasKey&lt;T, V&gt;(T, Expression&lt;Func&lt;T, V&gt;&gt;, String, Func&lt;String, Task&lt;V&gt;&gt;)**

Asynchronously sets a property value if the provided string value is not null or empty, using a function to retrieve the value.
 Uses lambda expression for compile-time safety and property access.

```csharp
public static Task SetValueIfHasKey<T, V>(T ob, Expression<Func<T, V>> lambda, string value, Func<string, Task<V>> func)
```

#### Type Parameters

`T`<br>
The type of the object

`V`<br>
The type of the property value

#### Parameters

`ob` T<br>
The object to modify

`lambda` Expression&lt;Func&lt;T, V&gt;&gt;<br>
Lambda expression that identifies the property to set

`value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The string value to use as input

`func` Func&lt;String, Task&lt;V&gt;&gt;<br>
Async function that retrieves the value using the string

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
A task representing the async operation

---

[`< Back`](../../)
