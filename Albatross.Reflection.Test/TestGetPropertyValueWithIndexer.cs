using System;
using System.Collections.Generic;
using Xunit;

namespace Albatross.Reflection.Test {
    public class TestGetPropertyValueWithIndexer {
        public class IndexedClass {
            public string[] Items { get; set; } = new[] { "first", "second", "third" };
            public Dictionary<string, int> Dict { get; set; } = new Dictionary<string, int> { ["key1"] = 10, ["key2"] = 20 };
            public List<string> List { get; set; } = new List<string> { "a", "b", "c" };
            public NestedClass Nested { get; set; } = new NestedClass();
        }

        public class NestedClass {
            public int[] Numbers { get; set; } = new[] { 100, 200, 300 };
            public Dictionary<string, string> Props { get; set; } = new Dictionary<string, string> { ["name"] = "test", ["color"] = "blue" };
        }

        public class PersonClass {
            public string Name { get; set; }
            public int Age { get; set; }
            public Address Address { get; set; }

            public PersonClass(string name, int age, Address address) {
                Name = name;
                Age = age;
                Address = address;
            }
        }

        public class Address {
            public string Street { get; set; }
            public string City { get; set; }
            public int ZipCode { get; set; }

            public Address(string street, string city, int zipCode) {
                Street = street;
                City = city;
                ZipCode = zipCode;
            }
        }

        public class ComplexIndexedClass {
            public PersonClass?[] People { get; set; }
            public Dictionary<string, PersonClass> PersonDict { get; set; }
            public List<PersonClass> PersonList { get; set; }
            public string[] StringArray { get; set; } = new[] { "Hello", "World", "Test" };

            public ComplexIndexedClass() {
                var person1 = new PersonClass("Alice", 30, new Address("123 Main St", "New York", 10001));
                var person2 = new PersonClass("Bob", 25, new Address("456 Oak Ave", "Boston", 02101));
                var person3 = new PersonClass("Charlie", 35, new Address("789 Pine Rd", "Chicago", 60601));

                People = new[] { person1, person2, person3 };
                PersonDict = new Dictionary<string, PersonClass> {
                    ["alice"] = person1,
                    ["bob"] = person2,
                    ["charlie"] = person3
                };
                PersonList = new List<PersonClass> { person1, person2, person3 };
            }
        }

        [Theory]
        [InlineData("Items[0]", "first")]
        [InlineData("Items[1]", "second")]
        [InlineData("Items[2]", "third")]
        [InlineData("Dict[key1]", 10)]
        [InlineData("Dict[key2]", 20)]
        [InlineData("List[0]", "a")]
        [InlineData("List[1]", "b")]
        [InlineData("List[2]", "c")]
        public void TestGetPropertyValueIndexerAccess(string propertyName, object? expected) {
            var obj = new IndexedClass();
            var actual = typeof(IndexedClass).GetPropertyValue(obj, propertyName, false);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Items[0]", typeof(string))]
        [InlineData("Dict[key1]", typeof(int))]
        [InlineData("List[0]", typeof(string))]
        public void TestGetPropertyValueWithIndexerAndType(string propertyName, Type expectedType) {
            var obj = new IndexedClass();
            var actual = typeof(IndexedClass).GetPropertyValue(obj, propertyName, false, out Type actualType);
            Assert.Equal(expectedType, actualType);
        }

        [Theory]
        [InlineData("Nested.Numbers[0]", 100)]
        [InlineData("Nested.Numbers[1]", 200)]
        [InlineData("Nested.Props[name]", "test")]
        [InlineData("Nested.Props[color]", "blue")]
        public void TestGetPropertyValueWithNestedIndexer(string propertyName, object? expected) {
            var obj = new IndexedClass();
            var actual = typeof(IndexedClass).GetPropertyValue(obj, propertyName, false);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Items[99]")] // Index out of range
        [InlineData("Dict[nonexistent]")] // Key not found  
        [InlineData("List[99]")] // Index out of range
        public void TestGetPropertyValueWithIndexerError(string propertyName) {
            var obj = new IndexedClass();
            // These should throw exceptions when accessing invalid indices/keys
            Assert.ThrowsAny<Exception>(() => typeof(IndexedClass).GetPropertyValue(obj, propertyName, false));
        }

        [Fact]
        public void TestGetPropertyValueWithInvalidIndexerSyntax() {
            var obj = new IndexedClass();
            // Missing closing bracket should throw ArgumentException
            Assert.Throws<ArgumentException>(() => typeof(IndexedClass).GetPropertyValue(obj, "Items[0", false));
        }

        [Fact]
        public void TestGetPropertyValueWithNonIndexableProperty() {
            var obj = new { Name = "test" };
            // Trying to index a non-indexable property should throw ArgumentException
            Assert.Throws<ArgumentException>(() => typeof(object).GetPropertyValue(obj, "Name[0]", false));
        }

        // Tests for accessing properties of indexed elements
        [Theory]
        [InlineData("StringArray[0].Length", 5)] // "Hello".Length
        [InlineData("StringArray[1].Length", 5)] // "World".Length
        [InlineData("StringArray[2].Length", 4)] // "Test".Length
        public void TestGetPropertyOfIndexedString(string propertyName, object? expected) {
            var obj = new ComplexIndexedClass();
            var actual = typeof(ComplexIndexedClass).GetPropertyValue(obj, propertyName, false);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("People[0].Name", "Alice")]
        [InlineData("People[0].Age", 30)]
        [InlineData("People[1].Name", "Bob")]
        [InlineData("People[1].Age", 25)]
        [InlineData("People[2].Name", "Charlie")]
        [InlineData("People[2].Age", 35)]
        public void TestGetPropertyOfIndexedArrayElement(string propertyName, object? expected) {
            var obj = new ComplexIndexedClass();
            var actual = typeof(ComplexIndexedClass).GetPropertyValue(obj, propertyName, false);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("PersonDict[alice].Name", "Alice")]
        [InlineData("PersonDict[alice].Age", 30)]
        [InlineData("PersonDict[bob].Name", "Bob")]
        [InlineData("PersonDict[bob].Age", 25)]
        [InlineData("PersonDict[charlie].Name", "Charlie")]
        [InlineData("PersonDict[charlie].Age", 35)]
        public void TestGetPropertyOfIndexedDictionaryElement(string propertyName, object? expected) {
            var obj = new ComplexIndexedClass();
            var actual = typeof(ComplexIndexedClass).GetPropertyValue(obj, propertyName, false);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("PersonList[0].Name", "Alice")]
        [InlineData("PersonList[0].Age", 30)]
        [InlineData("PersonList[1].Name", "Bob")]
        [InlineData("PersonList[1].Age", 25)]
        [InlineData("PersonList[2].Name", "Charlie")]
        [InlineData("PersonList[2].Age", 35)]
        public void TestGetPropertyOfIndexedListElement(string propertyName, object? expected) {
            var obj = new ComplexIndexedClass();
            var actual = typeof(ComplexIndexedClass).GetPropertyValue(obj, propertyName, false);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("People[0].Address.Street", "123 Main St")]
        [InlineData("People[0].Address.City", "New York")]
        [InlineData("People[0].Address.ZipCode", 10001)]
        [InlineData("People[1].Address.Street", "456 Oak Ave")]
        [InlineData("People[1].Address.City", "Boston")]
        [InlineData("People[1].Address.ZipCode", 02101)]
        [InlineData("PersonDict[charlie].Address.Street", "789 Pine Rd")]
        [InlineData("PersonDict[charlie].Address.City", "Chicago")]
        [InlineData("PersonDict[charlie].Address.ZipCode", 60601)]
        public void TestGetDeepPropertyOfIndexedElement(string propertyName, object? expected) {
            var obj = new ComplexIndexedClass();
            var actual = typeof(ComplexIndexedClass).GetPropertyValue(obj, propertyName, false);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("People[0]", typeof(PersonClass))]
        [InlineData("People[0].Name", typeof(string))]
        [InlineData("People[0].Age", typeof(int))]
        [InlineData("People[0].Address.ZipCode", typeof(int))]
        [InlineData("StringArray[0].Length", typeof(int))]
        [InlineData("PersonDict[alice].Address.Street", typeof(string))]
        public void TestGetPropertyTypeOfIndexedElement(string propertyName, Type expectedType) {
            var obj = new ComplexIndexedClass();
            var actual = typeof(ComplexIndexedClass).GetPropertyValue(obj, propertyName, false, out Type actualType);
            Assert.Equal(expectedType, actualType);
        }

        [Theory]
        [InlineData("People[99].Name")] // Index out of range
        [InlineData("PersonDict[nonexistent].Name")] // Key not found
        [InlineData("People[0].NonExistentProperty")] // Property doesn't exist
        [InlineData("People[0].Address.NonExistentField")] // Nested property doesn't exist
        public void TestGetPropertyOfIndexedElementError(string propertyName) {
            var obj = new ComplexIndexedClass();
            // These should throw exceptions
            Assert.ThrowsAny<Exception>(() => typeof(ComplexIndexedClass).GetPropertyValue(obj, propertyName, false));
        }

        [Fact]
        public void TestCaseInsensitivePropertyAccessOfIndexedElement() {
            var obj = new ComplexIndexedClass();

            // Test case insensitive access
            var result1 = typeof(ComplexIndexedClass).GetPropertyValue(obj, "people[0].name", true);
            var result2 = typeof(ComplexIndexedClass).GetPropertyValue(obj, "PEOPLE[0].NAME", true);
            var result3 = typeof(ComplexIndexedClass).GetPropertyValue(obj, "People[0].Name", false);

            Assert.Equal("Alice", result1);
            Assert.Equal("Alice", result2);
            Assert.Equal("Alice", result3);

            // Verify case sensitive throws exception for wrong case
            Assert.Throws<ArgumentException>(() =>
                typeof(ComplexIndexedClass).GetPropertyValue(obj, "people[0].name", false));
        }

        [Fact]
        public void TestComplexIndexerChaining() {
            // Let's test step by step to debug the issue
            var nestedData = new {
                Groups = new Dictionary<string, PersonClass[]> {
                    ["team1"] = new[] {
                        new PersonClass("Team Leader", 40, new Address("Leadership St", "Management City", 12345)),
                        new PersonClass("Team Member", 35, new Address("Collaboration Ave", "Teamwork City", 67890))
                    }
                }
            };

            var type = nestedData.GetType();

            // Test individual parts first
            var groups = type.GetPropertyValue(nestedData, "Groups", false);
            Assert.NotNull(groups);

            var team1Array = type.GetPropertyValue(nestedData, "Groups[team1]", false);
            Assert.NotNull(team1Array);
            Assert.IsType<PersonClass[]>(team1Array);

            var firstPerson = type.GetPropertyValue(nestedData, "Groups[team1][0]", false);
            Assert.NotNull(firstPerson);
            Assert.IsType<PersonClass>(firstPerson);

            // Now test the full chain - this should work if the previous steps work
            var result1 = type.GetPropertyValue(nestedData, "Groups[team1][0].Name", false);
            Assert.Equal("Team Leader", result1);
        }

        [Fact]
        public void TestNullHandlingInIndexedPropertyChain() {
            var data = new ComplexIndexedClass();
            // Set one person to null to test null handling
            data.People[1] = null;

            var type = typeof(ComplexIndexedClass);

            // This should return null without throwing
            var result = type.GetPropertyValue(data, "People[1].Name", false);
            Assert.Null(result);

            // But accessing properties of non-null elements should still work
            var validResult = type.GetPropertyValue(data, "People[0].Name", false);
            Assert.Equal("Alice", validResult);
        }
    }
}
