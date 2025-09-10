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

---

[`< Back`](../../)
