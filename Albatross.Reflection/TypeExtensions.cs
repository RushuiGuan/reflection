using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Albatross.Reflection {
	/// <summary>
	/// Provides extension methods for Type objects to facilitate type inspection, property manipulation, and generic type operations.
	/// Includes utilities for nullable types, collections, reflection-based property access, and type compatibility checks.
	/// </summary>
	public static class TypeExtensions {
		/// <summary>
		/// Determines if the specified type is a nullable value type and extracts the underlying value type.
		/// </summary>
		/// <param name="nullableType">The type to check</param>
		/// <param name="valueType">When this method returns true, contains the underlying value type of the nullable type</param>
		/// <returns>True if the type is Nullable&lt;T&gt;; otherwise, false</returns>
		public static bool GetNullableValueType(this Type nullableType, [NotNullWhen(true)] out Type? valueType) {
			if (nullableType.IsGenericType && nullableType.GetGenericTypeDefinition() == typeof(Nullable<>)) {
				valueType = nullableType.GetGenericArguments()[0];
				return true;
			}
			valueType = null;
			return false;
		}

		/// <summary>
		/// Determines if the specified type is a Task&lt;T&gt; and extracts the result type.
		/// </summary>
		/// <param name="taskType">The type to check</param>
		/// <param name="resultType">When this method returns true, contains the result type of the Task&lt;T&gt;</param>
		/// <returns>True if the type is Task&lt;T&gt;; otherwise, false</returns>
		public static bool GetTaskResultType(this Type taskType, [NotNullWhen(true)] out Type? resultType) {
			if (taskType.IsGenericType && taskType.GetGenericTypeDefinition() == typeof(Task<>)) {
				resultType = taskType.GetGenericArguments()[0];
				return true;
			}
			resultType = null;
			return false;
		}

		/// <summary>
		/// Determines if the specified type is a collection and extracts the element type.
		/// Supports arrays, generic collections implementing IEnumerable&lt;T&gt;, and non-generic enumerables.
		/// </summary>
		/// <param name="collectionType">The type to check</param>
		/// <param name="elementType">When this method returns true, contains the element type of the collection</param>
		/// <returns>True if the type is a collection type; otherwise, false</returns>
		/// <remarks>String types are not considered collections and will return false.</remarks>
		[Obsolete("Use TryGetCollectionElementType instead.")]
		public static bool GetCollectionElementType(this Type collectionType, [NotNullWhen(true)] out Type? elementType) {
			return TryGetCollectionElementType(collectionType, out elementType);
		}

		/// <summary>
		/// Determines if the specified type is a collection and extracts the element type.
		/// Supports arrays, generic collections implementing IEnumerable&lt;T&gt;, and non-generic enumerables.
		/// </summary>
		/// <param name="collectionType">The type to check</param>
		/// <param name="elementType">When this method returns true, contains the element type of the collection</param>
		/// <returns>True if the type is a collection type; otherwise, false</returns>
		/// <remarks>String types are not considered collections and will return false.</remarks>
		[Obsolete("Use TryGetGenericCollectionElementType instead.")]
		public static bool TryGetCollectionElementType(this Type collectionType, [NotNullWhen(true)] out Type? elementType) {
			elementType = null;

			if (collectionType == typeof(string)) {
				return false;
			} else if (collectionType == typeof(Array) || collectionType.IsArray) {
				// this code path is created for performance reason.  Type.IsArray is faster than checking all interfaces
				elementType = collectionType.GetElementType() ?? typeof(object);
			} else if (collectionType.IsGenericType && collectionType.GetGenericTypeDefinition() == typeof(IEnumerable<>)) {
				// this code path is already created for performance reason.
				elementType = collectionType.GetGenericArguments().First();
			} else {
				var enumerableInterface = collectionType.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));
				if (enumerableInterface != null) {
					elementType = enumerableInterface.GetGenericArguments().First();
				} else if (typeof(IEnumerable).IsAssignableFrom(collectionType)) {
					elementType = typeof(object);
				} else {
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Determines whether the specified type represents a collection type.
		/// </summary>
		/// <param name="type">The type to check</param>
		/// <returns>True if the type implements IEnumerable and is not a string; otherwise, false</returns>
		/// <remarks>String types are not considered collections and will return false.</remarks>
		public static bool IsCollectionType(this Type type)
			=> type != typeof(string) && typeof(System.Collections.IEnumerable).IsAssignableFrom(type);

		/// <summary>
		/// Determines if the specified type is a generic collection and extracts the element type.
		/// Supports arrays and generic collections implementing IEnumerable&lt;T&gt;.
		/// </summary>
		/// <param name="collectionType">The type to check</param>
		/// <param name="elementType">When this method returns true, contains the element type of the collection</param>
		/// <returns>True if the type is a generic collection type; otherwise, false</returns>
		/// <remarks>String types and non-generic enumerables are not supported and will return false.</remarks>
		public static bool TryGetGenericCollectionElementType(this Type collectionType, [NotNullWhen(true)] out Type? elementType) {
			elementType = null;

			if (collectionType == typeof(string)) {
				return false;
			}
			if (collectionType.IsArray) {
				elementType = collectionType.GetElementType() ?? typeof(object);
				return true;
			}
			if (collectionType.IsGenericType && collectionType.GetGenericTypeDefinition() == typeof(IEnumerable<>)) {
				elementType = collectionType.GetGenericArguments()[0];
				return true;
			}
			var enumerableInterface = collectionType.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));
			if (enumerableInterface != null) {
				elementType = enumerableInterface.GetGenericArguments()[0];
				return true;
			}
			return false;
		}

		/// <summary>
		/// Extracts the generic type name by removing the backtick and type parameter count.
		/// </summary>
		/// <param name="name">The full generic type name (e.g., "List`1")</param>
		/// <returns>The generic type name without the parameter count (e.g., "List")</returns>
		public static string GetGenericTypeName(this string name) {
			return name.Substring(0, name.LastIndexOf('`'));
		}

		/// <summary>
		/// Creates a neat class name combining the full type name with the assembly name.
		/// </summary>
		/// <param name="type">The type to get the neat name for</param>
		/// <returns>A string in the format "FullName,AssemblyName"</returns>
		public static string GetClassNameNeat(this Type type) => $"{type.FullName},{type.Assembly.GetName().Name}";

		/// <summary>
		/// Check if a type is anoymous
		/// </summary>
		public static Boolean IsAnonymousType(this Type type) {
			Boolean hasCompilerGeneratedAttribute = type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Count() > 0;
			Boolean nameContainsAnonymousType = type.FullName?.Contains("AnonymousType") == true;
			Boolean isAnonymousType = hasCompilerGeneratedAttribute && nameContainsAnonymousType;
			return isAnonymousType;
		}

		/// <summary>
		/// Determines whether the specified type is a concrete class (not abstract, not interface, not generic type definition).
		/// </summary>
		/// <param name="type">The type to check</param>
		/// <returns>True if the type is a concrete class; otherwise, false</returns>
		public static bool IsConcreteType(this Type type) => !type.IsAbstract && !type.IsInterface && type.IsClass && !type.IsGenericTypeDefinition;

		/// <summary>
		/// Determines whether the specified type is derived from or implements the generic type T.
		/// </summary>
		/// <typeparam name="T">The base type or interface to check against</typeparam>
		/// <param name="type">The type to check</param>
		/// <returns>True if the type derives from or implements type T; otherwise, false</returns>
		public static bool IsDerived<T>(this Type type) => typeof(T).IsAssignableFrom(type);

		/// <summary>
		/// Determines whether the specified type is derived from or implements the specified base type.
		/// </summary>
		/// <param name="type">The type to check</param>
		/// <param name="baseType">The base type or interface to check against</param>
		/// <returns>True if the type derives from or implements the base type; otherwise, false</returns>
		public static bool IsDerived(this Type type, Type baseType) => baseType.IsAssignableFrom(type);


		/// <summary>
		/// Provide with a class type and a generic type\interface definition, this methods will return true if the class derives\implements the generic type\interface.  It will
		/// also output the generic type.
		/// 
		/// Example:
		/// public class Test: IEnumerable&lt;int&gt; { }
		/// 
		/// var result = typeof(Test).TryGetClosedGenericTypes(typeof(IEnumerable&lt;&gt;), out Type type);
		/// Assert.True(result);
		/// Assert.AreSame(typeof(IEnumerable&lt;int&gt;), type);
		/// </summary>
		/// <param name="type">Input class type</param>
		/// <param name="genericDefinition">The definition of a generic type.  For example: typeof(IEnumerable&lt;&gt;)</param>
		/// <param name="genericType">If the class extends\implements the generic type\interface, its type will be set in this output parameter</param>
		/// <returns>Return true if the class implements the generic interface</returns>
		public static bool TryGetClosedGenericType(this Type type, Type genericDefinition, [NotNullWhen(true)] out Type? genericType) {
			genericType = null;
			if (!type.IsAbstract && type.IsClass && !type.IsGenericTypeDefinition) {
				if (genericDefinition.IsInterface) {
					genericType = type.GetInterfaces().FirstOrDefault(args => args.IsGenericType && args.GetGenericTypeDefinition() == genericDefinition);
				} else {
					while (type != typeof(object)) {
						if (type.IsGenericType && type.GetGenericTypeDefinition() == genericDefinition) {
							genericType = type;
							break;
						}
						type = type.BaseType ?? typeof(object);
					}
				}
			}
			return genericType != null;
		}

		/// <summary>
		/// Gets a Type by its name, throwing an ArgumentException if the type is not found.
		/// Unlike Type.GetType, this method provides better error handling for missing types.
		/// </summary>
		/// <param name="className">The fully qualified name of the type to retrieve</param>
		/// <returns>The Type object for the specified class name</returns>
		/// <exception cref="ArgumentException">Thrown when the class name is null, empty, or the type cannot be found</exception>
		public static Type GetRequiredType(this string? className) {
			if (string.IsNullOrEmpty(className)) {
				throw new ArgumentException("Type not found: empty class name");
			} else {
				Type? type = Type.GetType(className);
				if (type == null) {
					throw new ArgumentException($"Type not found: {className}");
				}
				return type;
			}
		}


		/// <summary>
		/// Determines whether the specified type is a nullable value type (Nullable&lt;T&gt;).
		/// </summary>
		/// <param name="type">The type to check</param>
		/// <returns>True if the type is Nullable&lt;T&gt;; otherwise, false</returns>
		public static bool IsNullableValueType(this Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

		/// <summary>
		/// Determines whether the specified type represents a numeric type.
		/// </summary>
		/// <param name="type">The type to check</param>
		/// <returns>True if the type is a numeric type (byte, int, float, decimal, etc.); otherwise, false</returns>
		public static bool IsNumericType(this Type type) {
			switch (Type.GetTypeCode(type)) {
				case TypeCode.Byte:
				case TypeCode.SByte:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Decimal:
				case TypeCode.Double:
				case TypeCode.Single:
					return true;
				default:
					return false;
			}
		}

		/// <summary>
		/// Gets the type name with assembly name but without version information.
		/// </summary>
		/// <param name="type">The type to get the name for</param>
		/// <returns>A string in the format "FullName, AssemblyName" without version details</returns>
		public static string GetTypeNameWithoutAssemblyVersion(this Type type) => $"{type.FullName}, {type.Assembly.GetName().Name}";

		/// <summary>
		/// Gets the file system location of an assembly combined with the specified path.
		/// </summary>
		/// <param name="asm">The assembly to get the location for</param>
		/// <param name="path">The relative path to combine with the assembly location</param>
		/// <returns>The combined path of the assembly location and the specified path</returns>
		/// <exception cref="Exception">Thrown when the assembly location cannot be determined</exception>
		public static string GetAssemblyLocation(this Assembly asm, string path) {
			string location = System.IO.Path.GetDirectoryName(asm.Location) ?? throw new Exception($"Cannot find the location of assembly {asm.FullName}");
			return System.IO.Path.Combine(location, path);
		}

		/// <summary>
		/// Gets a DirectoryInfo object representing the assembly location combined with the specified path.
		/// </summary>
		/// <param name="asm">The assembly to get the location for</param>
		/// <param name="path">The relative path to combine with the assembly location</param>
		/// <returns>A DirectoryInfo object for the combined path</returns>
		public static DirectoryInfo GetAssemblyDirectoryLocation(this Assembly asm, string path) {
			string location = GetAssemblyLocation(asm, path);
			return new DirectoryInfo(location);
		}

		/// <summary>
		/// Gets a FileInfo object representing the assembly location combined with the specified path.
		/// </summary>
		/// <param name="asm">The assembly to get the location for</param>
		/// <param name="path">The relative path to combine with the assembly location</param>
		/// <returns>A FileInfo object for the combined path</returns>
		public static FileInfo GetAssemblyFileLocation(this Assembly asm, string path) {
			string location = GetAssemblyLocation(asm, path);
			return new FileInfo(location);
		}

		/// <summary>
		/// Gets the value of a property from an object using reflection.
		/// Supports nested property access using dot notation (e.g., "Property.SubProperty").
		/// </summary>
		/// <param name="type">The type of the object</param>
		/// <param name="data">The object to get the property value from</param>
		/// <param name="name">The property name, which can include nested properties separated by dots</param>
		/// <param name="ignoreCase">Whether to ignore case when matching property names</param>
		/// <returns>The value of the property, or null if the object or any nested property is null</returns>
		/// <exception cref="ArgumentException">Thrown when the specified property is not found</exception>
		public static object? GetPropertyValue(this Type type, object? data, string name, bool ignoreCase) {
			if (data == null) { return null; }
			var bindingFlag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty;
			if (ignoreCase) {
				bindingFlag = bindingFlag | BindingFlags.IgnoreCase;
			}
			var index = name.IndexOf('.');
			if (index == -1) {
				var property = type.GetProperty(name, bindingFlag) ?? throw new ArgumentException($"Property {name} is not found in type {type.Name}");
				return property.GetValue(data);
			} else {
				var firstProperty = name.Substring(0, index);
				var property = type.GetProperty(firstProperty, bindingFlag) ?? throw new ArgumentException($"Property {name} is not found in type {type.Name}");
				var value = property.GetValue(data);
				if (value != null) {
					var remainingProperty = name.Substring(index + 1);
					// use value.GetType() instead of property.PropertyType because property.PropertyType may be a base class of value
					return GetPropertyValue(value.GetType(), value, remainingProperty, ignoreCase);
				} else {
					return null;
				}
			}
		}

		/// <summary>
		/// Gets the Type of a property using reflection.
		/// Supports nested property access using dot notation (e.g., "Property.SubProperty").
		/// </summary>
		/// <param name="type">The type containing the property</param>
		/// <param name="name">The property name, which can include nested properties separated by dots</param>
		/// <param name="ignoreCase">Whether to ignore case when matching property names</param>
		/// <returns>The Type of the specified property</returns>
		/// <exception cref="ArgumentException">Thrown when the specified property is not found</exception>
		public static Type GetPropertyType(this Type type, string name, bool ignoreCase) {
			var bindingFlag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty;
			if (ignoreCase) {
				bindingFlag = bindingFlag | BindingFlags.IgnoreCase;
			}
			var index = name.IndexOf('.');
			if (index == -1) {
				var property = type.GetProperty(name, bindingFlag) ?? throw new ArgumentException($"Property {name} is not found in type {type.Name}");
				return property.PropertyType;
			} else {
				var firstProperty = name.Substring(0, index);
				var property = type.GetProperty(firstProperty, bindingFlag) ?? throw new ArgumentException($"Property {name} is not found in type {type.Name}");
				var remainingProperty = name.Substring(index + 1);
				return property.PropertyType.GetPropertyType(remainingProperty, ignoreCase);
			}
		}

		/// <summary>
		/// Sets the value of a property on an object using reflection.
		/// Supports nested property access using dot notation (e.g., "Property.SubProperty").
		/// </summary>
		/// <param name="type">The type of the object</param>
		/// <param name="data">The object to set the property value on</param>
		/// <param name="propertyName">The property name, which can include nested properties separated by dots</param>
		/// <param name="value">The value to set</param>
		/// <param name="ignoreCase">Whether to ignore case when matching property names</param>
		/// <exception cref="ArgumentException">Thrown when the specified property is not found or doesn't have appropriate getter/setter</exception>
		/// <exception cref="InvalidOperationException">Thrown when attempting to set a property on a null object</exception>
		public static void SetPropertyValue(this Type type, object? data, string propertyName, object? value, bool ignoreCase) {
			if (data == null) {
				throw new InvalidOperationException($"cannot set property {propertyName} of a null value");
			}
			var bindingFlag = BindingFlags.Instance | BindingFlags.Public;
			if (ignoreCase) {
				bindingFlag = bindingFlag | BindingFlags.IgnoreCase;
			}
			var index = propertyName.IndexOf('.');
			if (index == -1) {
				bindingFlag = bindingFlag | BindingFlags.SetProperty;
				var property = type.GetProperty(propertyName, bindingFlag) ?? throw new ArgumentException($"Property {propertyName} is not found or doesn't have a setter in type {type.Name}");
				property.SetValue(data, value);
			} else {
				bindingFlag = bindingFlag | BindingFlags.GetProperty;
				var firstProperty = propertyName.Substring(0, index);
				var property = type.GetProperty(firstProperty, bindingFlag) ?? throw new ArgumentException($"Property {propertyName} is not found or doesn't have a getter in type {type.Name}");
				var propertyValue = property.GetValue(data);
				var remainingProperty = propertyName.Substring(index + 1);
				if (propertyValue != null) {
					SetPropertyValue(propertyValue.GetType(), propertyValue, remainingProperty, value, ignoreCase);
				} else {
					throw new InvalidOperationException($"cannot set property {remainingProperty} of a null value");
				}
			}
		}
	}
}