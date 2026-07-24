# TestBench

A .NET console application that provides an interactive command-line environment for running and testing code snippets. Commands are discovered automatically via reflection and can be invoked interactively or passed as arguments.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Build & Run

```bash
dotnet build
dotnet run
```

To run a command directly without entering the interactive loop:

```bash
dotnet run -- <command> [args...]
```

## Usage

Once running, the application presents a `:>` prompt. Type a command name to execute it, or use `help` to list all available commands.

### Built-in Commands

| Command | Description |
|---------|-------------|
| `help` | Shows all available commands with parameters |
| `clear` | Clears the console |
| `localdrives` | Lists local drives |
| `openlogfolder` | Opens the application log folder |
| `samplemenu` | Demonstrates the interactive menu system |
| `quit` / `exit` | Exits the application |

### Adding Commands

Add a new `static` method to the `Program` class with a `[Description]` attribute. It will be automatically discovered and available from the prompt:

```csharp
[Description("Does something useful")]
static void MyCommand(string arg1)
{
    Console.WriteLine(arg1);
}
```

## Project Structure

- **Program.cs** - Entry point, command loop, reflection-based command dispatch, and utility methods
- **Menu.cs** - Interactive console menu with keyboard navigation and multi-select
- **EnumExtensions.cs** - Helper extensions for working with enum flags

## License

Private project.
