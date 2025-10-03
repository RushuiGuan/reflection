using System;
using System.Collections.Generic;
using Xunit;

namespace Albatross.Reflection.Test {
    public class TestModifiedGetPropertyValue {
        public class TestClass {
            public string[] Items { get; set; } = new[] { "test1", "test2", "test3" };
            public Dictionary<string, PersonClass> People { get; set; } = new Dictionary<string, PersonClass> {
                ["john"] = new PersonClass { Name = "John", Age = 30 },
                ["jane"] = new PersonClass { Name = "Jane", Age = 25 }
            };

            public Color? Color { get; set; }
        }

        public record class Red : Color {
	        public Red() :base("red") { }
        }

        public record class Color {
	        public Color(string name) { Name = name; }
	        public  string Name { get; } 
        }

        public class PersonClass {
            public string Name { get; set; } = "";
            public int Age { get; set; }
        }

        [Fact]
        public void TestDirectIndexerAccess() {
            var obj = new TestClass();
            var type = typeof(TestClass);

            // Test direct indexer access (starting with bracket)
            var result = type.GetPropertyValue(obj.Items, "[1]", false);
            Assert.Equal("test2", result);

            var result2 = type.GetPropertyValue(obj.People, "[john]", false);
            Assert.Equal("John", ((PersonClass)result2!).Name);
        }

        [Fact]
        public void TestIndexerWithProperties() {
            var obj = new TestClass();
            var type = typeof(TestClass);

            // Test indexer followed by property access
            var result = type.GetPropertyValue(obj, "People[jane].Name", false);
            Assert.Equal("Jane", result);

            var result2 = type.GetPropertyValue(obj, "People[john].Age", false);
            Assert.Equal(30, result2);
        }

        [Fact]
        public void TestPropertyThenIndexer() {
            var obj = new TestClass();
            var type = typeof(TestClass);

            // Test property access followed by indexer
            var result = type.GetPropertyValue(obj, "Items[0]", false);
            Assert.Equal("test1", result);

            var result2 = type.GetPropertyValue(obj, "Items[2].Length", false);
            Assert.Equal(5, result2); // "test3".Length
        }

        [Theory]
        [InlineData(".InvalidStart")]
        [InlineData("Items[")]
        [InlineData("Items[0")]
        public void TestInvalidFormats(string propertyName) {
            var obj = new TestClass();
            var type = typeof(TestClass);

            Assert.ThrowsAny<ArgumentException>(() =>
                type.GetPropertyValue(obj, propertyName, false));
        }

        [Fact]
        public void TestOutParameterType() {
	        var obj = new TestClass() {
		        Color = new Red(),
	        };
            var type = typeof(TestClass);

            // Test that the out parameter correctly returns the type
            var result = type.GetPropertyValue(obj, "People[john].Age", false, out Type resultType);
            Assert.Equal(30, result);
            Assert.Equal(typeof(int), resultType);

            var result2 = type.GetPropertyValue(obj, "Items[0]", false, out Type resultType2);
            Assert.Equal("test1", result2);
            Assert.Equal(typeof(string), resultType2);
            
            var result3 = type.GetPropertyValue(obj, "Color", false, out Type resultType3);
            Assert.Equal(new Red(), result3);
            Assert.Equal(typeof(Color), resultType3);
            
        }
    }
}
