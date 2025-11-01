using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Albatross.Reflection {
	/// <summary>
	/// Provides utility methods for enumerating and flattening object properties into dictionaries.
	/// Supports nested objects, arrays, and collections with hierarchical path-based keys.
	/// </summary>
	public static class Extensions {
		internal static string BuildPropertyPath(string? path, int? index, string? name) {
			var sb = new StringBuilder(path);
			if (index.HasValue) {
				sb.Append("[").Append(index.Value).Append("]");
			}
			if (!string.IsNullOrEmpty(name)) {
				if (sb.Length > 0) {
					sb.Append(".");
				}
				sb.Append(name);
			}
			return sb.ToString();
		}

		internal static void RecursivelyAddProperties(object? value, string? path, int? index, Dictionary<string, object> result, HashSet<object> objects) {
			if (value == null) {
				return;
			} else if (value is string) {
				result.Add(BuildPropertyPath(path, index, null), value);
			} else if (value.GetType().IsValueType) {
				result.Add(BuildPropertyPath(path, index, null), value);
			} else {
				var type = value.GetType();
				// circular reference check
				if (objects.Contains(value)) {
					return;
				} else {
					objects.Add(value);
				}
				if (type.IsArray || typeof(IEnumerable).IsAssignableFrom(type)) {
					var newPath = BuildPropertyPath(path, index, null);
					int newIndex = 0;
					foreach (var item in (IEnumerable)value) {
						RecursivelyAddProperties(item, newPath, newIndex, result, objects);
						newIndex++;
					}
				} else {
					foreach (var property in value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
						var propertyValue = property.GetValue(value);
						RecursivelyAddProperties(propertyValue, BuildPropertyPath(path, index, property.Name), null, result, objects);
					}
				}
			}
		}

		/// <summary>
		/// Flatten an object into a dictionary.  The key is the path to the property.  The value is the value of the property.
		/// If a property is an array, then the index with square brackets is appended to the path.
		/// </summary>
		/// <param name="value">The object to flatten. Can be a primitive type, string, complex object, array, or collection</param>
		/// <param name="result">The dictionary to store the flattened key-value pairs</param>
		/// <remarks>
		/// Null values are skipped. Arrays and collections are recursively processed with indexed paths.
		/// Complex objects have their public instance properties enumerated recursively.
		/// </remarks>
		public static void ToDictionary(this object? value, Dictionary<string, object> result) {
			HashSet<object> visited = new HashSet<object>();
			RecursivelyAddProperties(value, null, null, result, visited);
		}
	}
}