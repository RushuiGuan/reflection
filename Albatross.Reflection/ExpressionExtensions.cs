using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Albatross.Reflection {
	/// <summary>
	/// Provides extension methods for working with LINQ expressions, particularly for property access and manipulation.
	/// Enables compile-time safe property operations using strongly-typed lambda expressions.
	/// </summary>
	public static class ExpressionExtensions {
		/// <summary>
		/// Extracts PropertyInfo from a lambda expression that accesses a property.
		/// </summary>
		/// <typeparam name="T">The type containing the property</typeparam>
		/// <param name="lambda">Lambda expression that accesses a property</param>
		/// <returns>The PropertyInfo for the accessed property</returns>
		/// <exception cref="ArgumentException">Thrown when the expression doesn't refer to a property</exception>
		public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, object?>> lambda) {
			MemberExpression? member = lambda.Body as MemberExpression;
			if (member == null) {
				throw new ArgumentException($"Expression '{lambda}' refers to a method.");
			}

			PropertyInfo? propInfo = member.Member as PropertyInfo;
			if (propInfo == null) {
				throw new ArgumentException($"Expression '{lambda}' refers to a field, not a property.");
			}
			return propInfo;
		}

		/// <summary>
		/// Extracts PropertyInfo from a strongly-typed lambda expression that accesses a property.
		/// </summary>
		/// <typeparam name="T">The type containing the property</typeparam>
		/// <typeparam name="V">The type of the property value</typeparam>
		/// <param name="lambda">Lambda expression that accesses a property</param>
		/// <returns>The PropertyInfo for the accessed property</returns>
		/// <exception cref="ArgumentException">Thrown when the expression doesn't refer to a property</exception>
		public static PropertyInfo GetPropertyInfo<T, V>(this Expression<Func<T, V>> lambda) {
			MemberExpression? member = lambda.Body as MemberExpression;
			if (member == null) {
				throw new ArgumentException($"Expression '{lambda}' refers to a method.");
			}

			PropertyInfo? propInfo = member.Member as PropertyInfo;
			if (propInfo == null) {
				throw new ArgumentException($"Expression '{lambda}' refers to a field, not a property.");
			}
			return propInfo;
		}
		/// <summary>
		/// Creates a predicate expression that checks equality between a member and a value.
		/// The member can be a property or field of the specified type.
		/// </summary>
		/// <typeparam name="T">The type containing the member</typeparam>
		/// <param name="propertyOrFieldName">The name of the property or field to compare</param>
		/// <param name="value">The value to compare against</param>
		/// <returns>A predicate expression that evaluates to true when the member equals the value</returns>
		/// <example>
		/// var predicate = ExpressionExtensions.GetPredicate&lt;Person&gt;("Name", "John");
		/// // Results in: person => person.Name == "John"
		/// </example>
		public static Expression<Func<T, bool>> GetPredicate<T>(string propertyOrFieldName, object? value) {
			ParameterExpression parameter = Expression.Parameter(typeof(T), "args");
			var body = Expression.Equal(Expression.PropertyOrField(parameter, propertyOrFieldName), Expression.Constant(value));
			return Expression.Lambda<Func<T, bool>>(body, parameter);
		}

		/// <summary>
		/// Sets a property value only if the provided nullable struct value has a value.
		/// Uses lambda expression for compile-time safety and property access.
		/// </summary>
		/// <typeparam name="T">The type of the object</typeparam>
		/// <typeparam name="V">The value type of the property</typeparam>
		/// <param name="ob">The object to modify</param>
		/// <param name="lambda">Lambda expression that identifies the property to set</param>
		/// <param name="value">The nullable value to set if it has a value</param>
		/// <returns>The original object for method chaining</returns>
		public static T SetValueIfNotNull<T, V>(this T ob, Expression<Func<T, V>> lambda, V? value) where V : struct {
			if (value.HasValue) {
				PropertyInfo prop = lambda.GetPropertyInfo();
				prop.SetValue(ob, value);
			}
			return ob;
		}

		/// <summary>
		/// Sets a nullable property value only if the provided nullable struct value has a value.
		/// Uses lambda expression for compile-time safety and property access.
		/// </summary>
		/// <typeparam name="T">The type of the object</typeparam>
		/// <typeparam name="V">The value type of the nullable property</typeparam>
		/// <param name="ob">The object to modify</param>
		/// <param name="lambda">Lambda expression that identifies the nullable property to set</param>
		/// <param name="value">The nullable value to set if it has a value</param>
		/// <returns>The original object for method chaining</returns>
		public static T SetValueIfNotNull<T, V>(this T ob, Expression<Func<T, V?>> lambda, V? value) where V : struct {
			if (value.HasValue) {
				PropertyInfo prop = lambda.GetPropertyInfo();
				prop.SetValue(ob, value);
			}
			return ob;
		}
		/// <summary>
		/// Sets a string property value only if the provided string is not null or empty.
		/// Uses lambda expression for compile-time safety and property access.
		/// </summary>
		/// <typeparam name="T">The type of the object</typeparam>
		/// <param name="obj">The object to modify</param>
		/// <param name="lambda">Lambda expression that identifies the string property to set</param>
		/// <param name="value">The string value to set if it's not null or empty</param>
		/// <returns>The original object for method chaining</returns>
		public static T SetTextIfNotEmpty<T>(this T obj, Expression<Func<T, string?>> lambda, string? value) {
			if (!string.IsNullOrEmpty(value)) {
				PropertyInfo prop = lambda.GetPropertyInfo();
				prop.SetValue(obj, value);
			}
			return obj;
		}

		/// <summary>
		/// Asynchronously sets a property value if the provided key has a value, using a function to retrieve the value.
		/// Uses lambda expression for compile-time safety and property access.
		/// </summary>
		/// <typeparam name="T">The type of the object</typeparam>
		/// <typeparam name="V">The type of the property value</typeparam>
		/// <typeparam name="K">The type of the key parameter</typeparam>
		/// <param name="ob">The object to modify</param>
		/// <param name="lambda">Lambda expression that identifies the property to set</param>
		/// <param name="key">The nullable key value</param>
		/// <param name="func">Async function that retrieves the value using the key</param>
		/// <returns>A task representing the async operation</returns>
		public static async Task SetValueIfHasKey<T, V, K>(this T ob, Expression<Func<T, V>> lambda, K? key, Func<K, Task<V>> func) where K : struct {
			if (key.HasValue) {
				var result = await func(key.Value);
				PropertyInfo prop = lambda.GetPropertyInfo();
				prop.SetValue(ob, result);
			}
		}
		/// <summary>
		/// Asynchronously sets a property value if the provided string value is not null or empty, using a function to retrieve the value.
		/// Uses lambda expression for compile-time safety and property access.
		/// </summary>
		/// <typeparam name="T">The type of the object</typeparam>
		/// <typeparam name="V">The type of the property value</typeparam>
		/// <param name="ob">The object to modify</param>
		/// <param name="lambda">Lambda expression that identifies the property to set</param>
		/// <param name="value">The string value to use as input</param>
		/// <param name="func">Async function that retrieves the value using the string</param>
		/// <returns>A task representing the async operation</returns>
		public static async Task SetValueIfHasKey<T, V>(this T ob, Expression<Func<T, V>> lambda, string? value, Func<string, Task<V>> func) {
			if (!string.IsNullOrEmpty(value)) {
				var result = await func(value);
				PropertyInfo prop = lambda.GetPropertyInfo();
				prop.SetValue(ob, result);
			}
		}
	}
}