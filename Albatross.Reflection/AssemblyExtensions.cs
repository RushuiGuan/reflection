using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Albatross.Reflection {
	/// <summary>
	/// Provides extension methods for Assembly objects to facilitate type discovery and resource management.
	/// </summary>
	public static class AssemblyExtensions {
		/// <summary>
		/// Return all concrete classes that derive from base class T in an assembly
		/// </summary>
		/// <typeparam name="T">The base type to filter by</typeparam>
		/// <param name="assembly">The assembly to search</param>
		/// <returns>An enumerable of concrete types that derive from T</returns>
		public static IEnumerable<Type> GetConcreteClasses<T>(this Assembly assembly) {
			return assembly.GetTypes().Where(args => args.IsConcreteType() && typeof(T).IsAssignableFrom(args));
		}

		/// <summary>
		/// Return all Concrete classes in an assembly
		/// </summary>
		/// <param name="assembly">The assembly to search</param>
		/// <returns>An enumerable of all concrete types in the assembly</returns>
		public static IEnumerable<Type> GetConcreteClasses(this Assembly assembly) {
			return assembly.GetTypes().Where(args => args.IsConcreteType());
		}

		/// <summary>
		/// Retrieves the content of an embedded resource file from the assembly.
		/// Uses the DefaultNamespaceAttribute if available, otherwise falls back to assembly name.
		/// </summary>
		/// <param name="type">A type from the assembly containing the embedded resource</param>
		/// <param name="name">The name of the embedded file</param>
		/// <param name="folder">The folder path within the assembly resources (default: "Embedded")</param>
		/// <returns>The content of the embedded file as a string</returns>
		/// <exception cref="ArgumentException">Thrown when the specified resource doesn't exist</exception>
		/// <remarks>
		/// This method constructs the resource name as: {namespace}.{folder}.{name}
		/// If the assembly name doesn't match the default namespace, use DefaultNamespaceAttribute.
		/// </remarks>
		public static string GetEmbeddedFile(this Type type, string name, string folder = "Embedded") {
			var attribute = type.Assembly.GetCustomAttribute<DefaultNamespaceAttribute>();
			string resourceName = $"{attribute?.DefaultNamespace ?? type.Assembly.GetName().Name}.{folder}.{name}";
			using var stream = type.Assembly.GetManifestResourceStream(resourceName);
			if (stream == null) throw new ArgumentException($"Assembly resource {resourceName} doesn't exist");
			return new StreamReader(stream).ReadToEnd();
		}
	}
}