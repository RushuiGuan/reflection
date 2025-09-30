# Albatross.Reflection

[![NuGet Version](https://img.shields.io/nuget/v/Albatross.Reflection)](https://www.nuget.org/packages/Albatross.Reflection)

A powerful .NET Standard 2.1 utility library that simplifies reflection operations with a focus on type-safe, expression-based property access and manipulation. This library provides a comprehensive set of extension methods for working with types, assemblies, and object introspection in .NET applications.

## Features

- **Enhanced Property Access with Indexer Support**: Access properties with dot notation AND indexer support for arrays, dictionaries, and collections
- **Expression-based Property Access**: Compile-time safe property operations using strongly-typed lambda expressions
- **Type Inspection Utilities**: Comprehensive type checking for nullable types, collections, tasks, and generics
- **Assembly Type Discovery**: Find and filter types within assemblies with powerful search capabilities
- **Object Property Enumeration**: Flatten complex objects and collections into key-value dictionaries
- **Embedded Resource Management**: Easy access to embedded resources with namespace resolution
- **Nullable Type Handling**: Specialized support for nullable value types and reference types
- **Generic Type Operations**: Utilities for working with generic types and their constraints
- **Predicate Generation**: Create LINQ predicates from property names and values

## Prerequisites

- **.NET Standard 2.1** or higher
- **.NET SDK 6.0** or later (for building from source)
- **C# 9.0** or later (supports nullable reference types)

## Installation

### Package Manager Console
```powershell
Install-Package Albatross.Reflection
```

### .NET CLI
```bash
dotnet add package Albatross.Reflection
```

### PackageReference
```xml
<PackageReference Include="Albatross.Reflection" Version="7.5.10" />
```

## Example Usage

### Expression-based Property Access

```csharp
using Albatross.Reflection;

public class Person
{
    public string Name { get; set; }
    public int? Age { get; set; }
    public DateTime CreatedDate { get; set; }
    public Address[] Addresses { get; set; }
    public Dictionary<string, string> Properties { get; set; }
}

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public int ZipCode { get; set; }
}

// Get PropertyInfo using expression
var nameProperty = ExpressionExtensions.GetPropertyInfo<Person>(p => p.Name);
Console.WriteLine(nameProperty.Name); // Output: Name

// Get property value using reflection utilities
var person = new Person { 
    Name = "John", 
    Age = 25,
    Addresses = new[] { 
        new Address { Street = "123 Main St", City = "New York", ZipCode = 10001 },
        new Address { Street = "456 Oak Ave", City = "Boston", ZipCode = 02101 }
    },
    Properties = new Dictionary<string, string> { ["Role"] = "Developer", ["Department"] = "IT" }
};

// Basic property access
var nameValue = typeof(Person).GetPropertyValue(person, "Name", false);
Console.WriteLine(nameValue); // Output: John

// Array indexer access
var firstAddress = typeof(Person).GetPropertyValue(person, "Addresses[0].Street", false);
Console.WriteLine(firstAddress); // Output: 123 Main St

// Dictionary indexer access
var role = typeof(Person).GetPropertyValue(person, "Properties[Role]", false);
Console.WriteLine(role); // Output: Developer

// Complex nested access
var firstZipCode = typeof(Person).GetPropertyValue(person, "Addresses[0].ZipCode", false);
Console.WriteLine(firstZipCode); // Output: 10001

// Create predicate expressions
var predicate = ExpressionExtensions.GetPredicate<Person>("Name", "John");
// Results in: person => person.Name == "John"
```

### Advanced Property Access with Indexers

The `GetPropertyValue` method now supports comprehensive indexer notation for accessing elements in arrays, dictionaries, lists, and other indexed collections:

```csharp
using Albatross.Reflection;

public class DataContainer
{
    public string[] Names { get; set; } = new[] { "Alice", "Bob", "Charlie" };
    public Dictionary<string, int> Scores { get; set; } = new() { ["Alice"] = 95, ["Bob"] = 87 };
    public List<Person> People { get; set; } = new();
    public Dictionary<string, Person[]> Teams { get; set; } = new();
}

var container = new DataContainer();
var type = typeof(DataContainer);

// Array access
var firstName = type.GetPropertyValue(container, "Names[0]", false);
Console.WriteLine(firstName); // Output: Alice

// Dictionary access
var aliceScore = type.GetPropertyValue(container, "Scores[Alice]", false);
Console.WriteLine(aliceScore); // Output: 95

// Nested object property access through indexer
var personName = type.GetPropertyValue(container, "People[0].Name", false);

// Complex nested indexer chains
var teamLeaderCity = type.GetPropertyValue(container, "Teams[Development][0].Addresses[0].City", false);

// Direct indexer access (when working with indexed objects directly)
var arrayElement = type.GetPropertyValue(someArray, "[2]", false);

// String property access through indexer
var nameLength = type.GetPropertyValue(container, "Names[0].Length", false);
Console.WriteLine(nameLength); // Output: 5 (length of "Alice")

// Get both value and type information
var cityValue = type.GetPropertyValue(container, "People[0].Addresses[0].City", false, out Type cityType);
Console.WriteLine($"Value: {cityValue}, Type: {cityType.Name}"); // Type: String
```

**Supported Indexer Patterns:**
- `Property[index]` - Array/List access with integer index
- `Property[key]` - Dictionary access with string or typed keys
- `Property[index].SubProperty` - Property access on indexed elements
- `Property[key].SubProperty.NestedProperty` - Deep nested access
- `Property1[key1][index2].Property2` - Consecutive indexers
- `[index]` - Direct indexer access on the root object

### Type Inspection and Utilities

```csharp
using Albatross.Reflection;

// Check nullable types
if (typeof(int?).GetNullableValueType(out Type valueType))
{
    Console.WriteLine(valueType); // Output: System.Int32
}

// Check collection types
if (typeof(List<string>).TryGetGenericCollectionElementType(out Type itemType))
{
    Console.WriteLine(itemType); // Output: System.String
}

// Type compatibility checks
bool isDerived = typeof(string).IsDerived<object>(); // true
bool isNullable = typeof(int?).IsNullableValueType(); // true
```

### Assembly Type Discovery

```csharp
using Albatross.Reflection;

// Find all concrete classes implementing an interface
var services = Assembly.GetExecutingAssembly()
    .GetConcreteClasses<IMyService>();

// Get embedded resource content
string content = typeof(MyClass).GetEmbeddedFile("config.json", "Settings");
```

### Object Property Enumeration

```csharp
using Albatross.Reflection;

var person = new Person 
{ 
    Name = "John", 
    Age = 30,
    Addresses = new[] { 
        new Address { Street = "123 Main St", City = "New York" },
        new Address { Street = "456 Oak Ave", City = "Boston" }
    },
    Properties = new Dictionary<string, string> { ["Role"] = "Developer" }
};

var properties = new Dictionary<string, object>();
person.ToDictionary(properties);

// Results in flattened structure that can be accessed using GetPropertyValue:
// "Name" => "John"
// "Age" => 30
// "Addresses[0].Street" => "123 Main St"
// "Addresses[0].City" => "New York"
// "Addresses[1].Street" => "456 Oak Ave"
// "Addresses[1].City" => "Boston"
// "Properties[Role]" => "Developer"

// You can then use these keys with GetPropertyValue:
foreach (var key in properties.Keys)
{
    var value = typeof(Person).GetPropertyValue(person, key, false);
    Console.WriteLine($"{key} = {value}");
}
```

## Project Structure

```
├── Albatross.Reflection/           # Main library source code
│   ├── AssemblyExtensions.cs       # Assembly type discovery utilities
│   ├── ExpressionExtensions.cs     # Expression-based property operations
│   ├── TypeExtensions.cs           # Type inspection and manipulation
│   ├── Extensions.cs               # Object property enumeration
│   ├── DefaultNamespaceAttribute.cs # Custom attribute for resources
│   └── README.md                   # Package documentation
├── Albatross.Reflection.Test/      # Unit tests
│   ├── ExpressionTest.cs           # Expression extension tests
│   ├── Tests.cs                    # Core functionality tests
│   ├── TestExtensions.cs           # Property enumeration tests
│   └── ...                        # Additional test files
├── docs/                          # Generated API documentation
├── README.md                      # This file
└── reflection.sln                 # Solution file
```

## Building from Source

1. **Clone the repository**
   ```bash
   git clone https://github.com/RushuiGuan/reflection.git
   cd reflection
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the solution**
   ```bash
   dotnet build
   ```

## Running Tests

Execute all unit tests:
```bash
dotnet test
```

Run tests with detailed output:
```bash
dotnet test --verbosity normal
```

Run tests for specific project:
```bash
dotnet test Albatross.Reflection.Test/Albatross.Reflection.Test.csproj
```

## Contributing

We welcome contributions! Please follow these steps:

1. **Fork the repository** on GitHub
2. **Create a feature branch** from `main`
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. **Make your changes** and add tests if applicable
4. **Ensure all tests pass**
   ```bash
   dotnet test
   ```
5. **Commit your changes** with a clear commit message
   ```bash
   git commit -m "Add feature: your feature description"
   ```
6. **Push to your fork** and **create a pull request**

### Code Style Guidelines

- Follow existing code formatting and naming conventions
- Add XML documentation comments for public APIs
- Include unit tests for new functionality
- Ensure backward compatibility when possible

## License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

```
MIT License

Copyright (c) 2019 Rushui Guan

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

## Recent Updates

### ✨ Enhanced Indexer Support (Latest Version)

**New in this version**: The `GetPropertyValue` method now supports comprehensive indexer notation:

```csharp
// 🆕 Array indexer support
var item = type.GetPropertyValue(obj, "Items[0]", false);

// 🆕 Dictionary indexer support  
var value = type.GetPropertyValue(obj, "Dictionary[key]", false);

// 🆕 Property access on indexed elements
var street = type.GetPropertyValue(obj, "Addresses[0].Street", false);

// 🆕 Consecutive indexers
var leader = type.GetPropertyValue(obj, "Teams[Development][0].Name", false);

// 🆕 Direct indexer access
var element = type.GetPropertyValue(array, "[2]", false);
```

**Key improvements:**
- ✅ Support for arrays, dictionaries, lists, and custom indexers
- ✅ Nested property access through indexed elements  
- ✅ Consecutive indexer chains (e.g., `[key1][index2]`)
- ✅ Type-safe index parameter conversion
- ✅ Comprehensive error handling with clear messages
- ✅ Full backward compatibility maintained

## Support

- 📖 [API Documentation](https://rushuiguan.github.io/reflection/)
- 🐛 [Report Issues](https://github.com/RushuiGuan/reflection/issues)
- 💡 [Request Features](https://github.com/RushuiGuan/reflection/issues/new)
- 📦 [NuGet Package](https://www.nuget.org/packages/Albatross.Reflection)
