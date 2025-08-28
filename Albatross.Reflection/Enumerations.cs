using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Albatross.Reflection {
	/// <summary>
	/// Provides utility methods for enumerating and flattening object properties into dictionaries.
	/// Supports nested objects, arrays, and collections with hierarchical path-based keys.
	/// </summary>
	public static class Enumerations {
		static string GetPropertyKey(string? path, int? index, string? name) {
			string key;
			if (string.IsNullOrEmpty(path)) {
				return name ?? string.Empty;
			} else {
				if (!string.IsNullOrEmpty(name)) {
					name = $".{name}";
				}
				if (index.HasValue) {
					key = $"{path}[{index}]{name}";
				} else {
					key = $"{path}{name}";
				}
			}
			return key;
		}
		/// <summary>
		/// Flatten an object into a dictionary.  The key is the path to the property.  The value is the value of the property.
		/// If a property is an array, then the index with square brackets is appended to the path.
		/// </summary>
		/// <param name="value">The object to flatten. Can be a primitive type, string, complex object, array, or collection</param>
		/// <param name="path">The current property path. Used for nested objects and arrays</param>
		/// <param name="index">The array index when processing array elements. Used to generate indexed keys like "path[0]"</param>
		/// <param name="result">The dictionary to store the flattened key-value pairs</param>
		/// <remarks>
		/// Null values are skipped. Arrays and collections are recursively processed with indexed paths.
		/// Complex objects have their public instance properties enumerated recursively.
		/// </remarks>
		public static void Property(object? value, string? path, int? index, Dictionary<string, object> result) {
			if (value == null) {
				return;
			} else if (value is string) {
				result.Add(GetPropertyKey(path, index, null), value);
			} else if (value.GetType().IsValueType) {
				result.Add(GetPropertyKey(path, index, null), value);
			} else {
				var type = value.GetType();
				if (type.IsArray || typeof(IEnumerable).IsAssignableFrom(type)) {
					var newPath = GetPropertyKey(path, index, null);
					int newIndex = 0;
					foreach (var item in (IEnumerable)value) {
						Property(item, newPath, newIndex, result);
						newIndex++;
					}
				} else {
					foreach (var property in value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
						var propertyValue = property.GetValue(value);
						Property(propertyValue, GetPropertyKey(path, index, property.Name), null, result);
					}
				}
			}
		}
	}
}