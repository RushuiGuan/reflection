# Albatross.Reflection

[![NuGet Version](https://img.shields.io/nuget/v/Albatross.Reflection)](https://www.nuget.org/packages/Albatross.Reflection)

A powerful .NET Standard 2.1 utility library that simplifies reflection operations with a focus on type-safe, expression-based property access and manipulation. This library provides a comprehensive set of extension methods for working with types, assemblies, and object introspection in .NET applications.

## Features

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
- **.NET Framework 4.7.2+**, **.NET Core 2.1+**, or **.NET 5+**
- **Visual Studio 2019** or **Visual Studio Code** with C# extension

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
}

// Get PropertyInfo using expression
var nameProperty = ExpressionExtensions.GetPropertyInfo<Person>(p => p.Name);
Console.WriteLine(nameProperty.Name); // Output: Name

// Set value only if not null
var person = new Person();
person.SetValueIfNotNull(p => p.Age, 25);
person.SetTextIfNotEmpty(p => p.Name, "John Doe");

// Create predicate expressions
var predicate = ExpressionExtensions.GetPredicate<Person>("Name", "John");
// Results in: person => person.Name == "John"
```

### Type Inspection and Utilities

```csharp
using Albatross.Reflection;

// Check nullable types
if (typeof(int?).GetNullableValueType(out Type valueType))
{
    Console.WriteLine(valueType); // Output: System.Int32
}

// Check collection types
if (typeof(List<string>).GetCollectionItemType(out Type itemType))
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
    Addresses = new[] { "123 Main St", "456 Oak Ave" }
};

var properties = new Dictionary<string, object>();
Enumerations.Property(person, null, null, properties);

// Results in flattened structure:
// "Name" => "John"
// "Age" => 30
// "Addresses[0]" => "123 Main St"
// "Addresses[1]" => "456 Oak Ave"
```

## Project Structure

```
├── Albatross.Reflection/           # Main library source code
│   ├── AssemblyExtensions.cs       # Assembly type discovery utilities
│   ├── ExpressionExtensions.cs     # Expression-based property operations
│   ├── TypeExtensions.cs           # Type inspection and manipulation
│   ├── Enumerations.cs             # Object property enumeration
│   ├── DefaultNamespaceAttribute.cs # Custom attribute for resources
│   └── README.md                   # Package documentation
├── Albatross.Reflection.Test/      # Unit tests
│   ├── ExpressionTest.cs           # Expression extension tests
│   ├── Tests.cs                    # Core functionality tests
│   ├── TestEnumerations.cs         # Property enumeration tests
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

4. **Run the application** (if applicable)
   ```bash
   dotnet run --project Albatross.Reflection
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

## Support

- 📖 [API Documentation](https://rushuiguan.github.io/reflection/)
- 🐛 [Report Issues](https://github.com/RushuiGuan/reflection/issues)
- 💡 [Request Features](https://github.com/RushuiGuan/reflection/issues/new)
- 📦 [NuGet Package](https://www.nuget.org/packages/Albatross.Reflection)
