# GetPropertyValue Enhancement Summary

## Overview
The `GetPropertyValue` method in `Albatross.Reflection.TypeExtensions` has been significantly enhanced to support comprehensive indexer notation for accessing elements in arrays, dictionaries, lists, and other indexed collections.

## New Features Added

### 1. Indexer Support
- **Array Access**: `"Items[0]"`, `"Items[1]"`
- **Dictionary Access**: `"Scores[PlayerName]"`, `"Settings[key]"`
- **List Access**: `"People[0]"`, `"Categories[2]"`
- **Custom Indexers**: Support for any type implementing indexer properties

### 2. Property Access on Indexed Elements
- **Basic**: `"People[0].Name"`, `"Items[1].Length"`
- **Nested**: `"People[0].Address.City"`, `"Orders[0].Customer.Name"`
- **Deep Nesting**: `"Companies[0].Departments[1].Manager.Contact.Email"`

### 3. Consecutive Indexers
- **Double Indexing**: `"Matrix[row][col]"`
- **Complex Chains**: `"Teams[Development][0].Members[1].Skills[C#]"`
- **Mixed Access**: `"Groups[team1][0].Projects[0].Tasks[1].Status"`

### 4. Direct Indexer Access
- **Root Object Indexing**: `"[0]"`, `"[key]"`
- **Starting with Bracket**: Allows indexing the root object directly

### 5. Enhanced Error Handling
- **Clear Error Messages**: Specific error types for different failure scenarios
- **Index Validation**: Range checking for arrays with descriptive messages
- **Type Conversion**: Automatic conversion of string indices to appropriate types
- **Syntax Validation**: Detection of malformed bracket syntax

## Implementation Details

### Method Signatures
```csharp
// Simple overload (existing signature enhanced)
public static object? GetPropertyValue(this Type type, object data, string name, bool ignoreCase)

// Overload with type information (existing signature enhanced)  
public static object? GetPropertyValue(this Type type, object data, string name, bool ignoreCase, out Type propertyType)
```

### Helper Method Added
```csharp
private static object? GetIndexValue(object obj, string indexString, BindingFlags bindingFlag, out Type resultType)
```

### Parsing Logic
The enhanced implementation now handles:
1. **Property-first scenarios**: `"Property.Nested"`
2. **Bracket-first scenarios**: `"Property[index]"` 
3. **Direct bracket access**: `"[index]"`
4. **Mixed combinations**: `"Property[index].SubProperty"`

## Backward Compatibility
✅ **Fully backward compatible** - all existing functionality preserved
✅ **Same method signatures** - no breaking changes
✅ **Enhanced capabilities** - existing patterns work better with improved error handling

## Test Coverage
- **91 comprehensive tests** covering all new functionality
- **Edge cases tested**: null handling, invalid syntax, type conversions
- **Complex scenarios**: nested indexers, consecutive brackets, mixed access patterns
- **Error conditions**: out-of-range indices, missing keys, invalid syntax

## Usage Examples

### Before Enhancement
```csharp
// Limited to dot notation only
var city = type.GetPropertyValue(person, "Address.City", false);
```

### After Enhancement  
```csharp
// All previous functionality plus indexer support
var city = type.GetPropertyValue(person, "Address.City", false);
var firstAddress = type.GetPropertyValue(person, "Addresses[0].City", false);
var score = type.GetPropertyValue(game, "PlayerScores[PlayerName]", false);
var teamLeader = type.GetPropertyValue(org, "Teams[Development][0].Name", false);
```

## Documentation Updates
- ✅ **README.md**: Updated with comprehensive examples and new feature descriptions
- ✅ **API Documentation**: Enhanced method documentation with indexer examples
- ✅ **Code Comments**: Detailed XML documentation for all new functionality
- ✅ **Usage Examples**: Real-world scenarios demonstrating the new capabilities

This enhancement significantly expands the utility of the `GetPropertyValue` method while maintaining full backward compatibility and providing robust error handling.
