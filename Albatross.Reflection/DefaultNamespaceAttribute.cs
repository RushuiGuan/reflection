using System;

namespace Albatross.Reflection {
	/// <summary>
	/// Use this attribute to specify the default namespace for embedded resources.  
	/// Useful when the assembly name doesn't match the default namespace name.
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly)]
	public class DefaultNamespaceAttribute : Attribute {
		/// <summary>
		/// Gets the default namespace for embedded resources.
		/// </summary>
		public string DefaultNamespace { get; }
		
		/// <summary>
		/// Initializes a new instance of the DefaultNamespaceAttribute class.
		/// </summary>
		/// <param name="defaultNamespace">The default namespace to use for embedded resources</param>
		public DefaultNamespaceAttribute(string defaultNamespace) {
			this.DefaultNamespace = defaultNamespace;
		}
	}
}