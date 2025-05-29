# System.Management.Types

Provides strongly-typed C# representations of WMI and system/network management objects for use in .NET applications. This project supplies type definitions and code generation utilities to enable type-safe access to management data.

## Features

- Strongly-typed C# classes for WMI and system/network management objects
- Auto-generated types for common WMI classes (e.g., Win32_Process, Win32_OperatingSystem)
- Designed for use with management APIs and LINQ providers
- Simplifies working with management data by providing compile-time type checking

## Getting Started

### Prerequisites

- .NET 8.0 or .NET 9.0 SDK
- Windows OS with Windows Management Instrumentation (WMI) enabled

## Usage

**Note:** Running WMI queries may require elevated permissions.

Here's an example of how to check if an explorer process is running on the system:

```csharp
using System.Management;
using System.Management.Types.Win32;
using System.Management.Types;

var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Process WHERE Name LIKE 'Explorer%'");
var process = InstanceFactory.CreateInstances<Process>(searcher.Get()).FirstOrDefault();

if (process != null)
{
    Console.WriteLine($"Explorer process is running with ProcessId: {process.ProcessId}");
}
else
{
    Console.WriteLine("Explorer process is not running.");
}
```

## Contributing

Contributions are welcome! Please open issues and submit PRs for improvements or bug fixes.

## License

This project is licensed under the MIT License.

---

> Maintained by [Triogap](https://github.com/Triogap)