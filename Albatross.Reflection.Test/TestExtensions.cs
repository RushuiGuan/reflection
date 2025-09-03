using Albatross.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Albatross.Reflection.Test {
	public class ClassB {
		public ClassB(string name) {
			Name = name;
		}
		public string Name { get; set; }
		public int[] Array { get; set; } = new int[0];
	}
	public class ClassA {
		public string? TextA { get; set; }
		public string? TextB { get; set; }
		public int IntValue { get; set; }
		public int? NullableIntValue { get; set; }
		public ClassB B { get; set; } = new ClassB("class b");
		public List<string> ListA { get; set; } = new List<string>();
		public string[] ArrayB { get; set; } = Array.Empty<string>();
	}
	public class TestExtensions {
		[Fact]
		public void TestSimpleCase() {
			var dict = new Dictionary<string, object>();
			int intValue = 1;
			string textValue = "test";
			Extensions.RecursivelyAddProperties(intValue, "a", null, dict);
			Extensions.RecursivelyAddProperties(intValue, "a", 1, dict);
			Extensions.RecursivelyAddProperties(textValue, "t", null, dict);
			Assert.Collection(dict,
				x => {
					Assert.Equal("a", x.Key);
					Assert.Equal(1, x.Value);
				}, x => {
					Assert.Equal("a[1]", x.Key);
					Assert.Equal(1, x.Value);
				}, x => {
					Assert.Equal("t", x.Key);
					Assert.Equal("test", x.Value);
				});
		}
		[Fact]
		public void TestPropertyEnumeration() {
			var dict = new Dictionary<string, object>();
			ClassA a = new ClassA() {
				TextB = "a",
				IntValue = 100,
			};
			Extensions.ToDictionary(a, dict);
			Assert.Collection(dict,
				x => {
					Assert.Equal("TextB", x.Key);
					Assert.Equal("a", x.Value);
				}, x => {
					Assert.Equal("IntValue", x.Key);
					Assert.Equal(100, x.Value);
				}, x => {
					Assert.Equal("B.Name", x.Key);
					Assert.Equal("class b", x.Value);
				});
		}
		[Theory]
		[InlineData(null, 1, null, "[1]")]
		[InlineData(null, null, "name", "name")]
		[InlineData("test", null, null, "test")]
		[InlineData("test", 1, null, "test[1]")]
		[InlineData(null, 1, "name", "[1].name")]
		[InlineData("test", null, "name", "test.name")]
		[InlineData("test", 1, "name", "test[1].name")]
		public void TestBuildPropertyPath(string? path, int? index, string? name, string expected) {
			var result = Extensions.BuildPropertyPath(path, index, name);
			Assert.Equal(expected, result);
		}

		[Fact]
		public void TestArrayEnumeration() {
			var dict = new Dictionary<string, object>();
			ClassA a = new ClassA() {
				ListA = new List<string> { "a", "b", "c" },
				ArrayB = new string[] { "x", "y", "z", },
			};
			a.B.Array = new int[] { 1, 2, };
			Extensions.ToDictionary(a, dict);
			Assert.Collection(dict,
				x => {
					Assert.Equal("IntValue", x.Key);
					Assert.Equal(0, x.Value);
				}, x => {
					Assert.Equal("B.Name", x.Key);
					Assert.Equal("class b", x.Value);
				}, x => {
					Assert.Equal("B.Array[0]", x.Key);
					Assert.Equal(1, x.Value);
				}, x => {
					Assert.Equal("B.Array[1]", x.Key);
					Assert.Equal(2, x.Value);
				}, x => {
					Assert.Equal("ListA[0]", x.Key);
					Assert.Equal("a", x.Value);
				}, x => {
					Assert.Equal("ListA[1]", x.Key);
					Assert.Equal("b", x.Value);
				}, x => {
					Assert.Equal("ListA[2]", x.Key);
					Assert.Equal("c", x.Value);
				}, x => {
					Assert.Equal("ArrayB[0]", x.Key);
					Assert.Equal("x", x.Value);
				}, x => {
					Assert.Equal("ArrayB[1]", x.Key);
					Assert.Equal("y", x.Value);
				}, x => {
					Assert.Equal("ArrayB[2]", x.Key);
					Assert.Equal("z", x.Value);
				});
		}

		[Fact]
		public void TestRootArrayEnumeration() {
			var dict = new Dictionary<string, object>();
			var array = new string[] { 
				"a", "b", "c",
			};
			Extensions.ToDictionary(array, dict);
			Assert.Collection(dict,
				x => {
					Assert.Equal("[0]", x.Key);
					Assert.Equal("a", x.Value);
				}, x => {
					Assert.Equal("[1]", x.Key);
					Assert.Equal("b", x.Value);
				}, x => {
					Assert.Equal("[2]", x.Key);
					Assert.Equal("c", x.Value);
				});
		}
	}
}