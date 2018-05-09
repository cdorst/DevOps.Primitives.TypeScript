# DevOps.Primitives.TypeScript

Represents TypeScript-language concepts as C# entity types

## Available on NuGet

[https://www.nuget.org/packages/CDorst.DevOps.Primitives.TypeScript](https://www.nuget.org/packages/CDorst.DevOps.Primitives.TypeScript/)

## Use Cases

This library enables meta-programming tasks for TypeScript type declarations:
- Code generation
- Database storage (w/ EntityFrameworkCore)

## Usage Notes

Compose `TypeDeclaration` graphs using the following types:
- `ClassDeclaration`
- `EnumDeclaration`
- `InterfaceDeclaration`

Invoke `ToString()` to generate a source-code document as `string`.

```csharp
var classDeclaration = new ClassDeclaration("TypeName", "Namespace");

var path = Path.Combine(Environment.CurrentDirectory, "index.ts");
File.WriteAllText(path, classDeclartion.ToString());
```

