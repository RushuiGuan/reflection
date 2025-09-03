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
	}
}