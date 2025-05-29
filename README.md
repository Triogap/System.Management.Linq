# System.Management.Linq

Exposes WMI and system/network management objects as strongly-typed C# classes, enabling LINQ-to-WQL queries for expressive, type-safe access to management data.

## Features

- Strongly-typed C# classes for WMI and system/network management objects
- LINQ-to-WQL support for expressive and type-safe queries
- Easy access to management data for .NET developers

## Getting Started

### Prerequisites

- .NET 8.0, or .NET 9.0 SDK
- Windows OS with Windows Management Instrumentation (WMI) enabled

### Installation

Clone the repository:

```shell
git clone https://github.com/Triogap/System.Management.Linq.git
```

## Usage

**Note:** Running WMI queries may require elevated permissions.

Here’s an example of how to check if an explorer process is running on the system:

```csharp
using System.Management.Linq;
using System.Management.Types.Win32;
using System.Management.Types;

bool explorerRunning = ManagementObjects.Get<Process>()
    .Any(p => p.Name.StartsWith("Explorer"));

if (explorerRunning)
{
    Console.WriteLine("Explorer process is running.");
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