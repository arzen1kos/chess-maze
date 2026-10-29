<div align="center">
   <img title="Chess Maze Game" width="100" height="100" alt="chess-maze-logo" src="https://github.com/user-attachments/assets/bb8ca55f-4764-463c-93a3-1990c91b8486" />
  
# ♞ Knight’s Tale — Chess Maze Solver
[![C#](https://custom-icon-badges.demolab.com/badge/C%23-%23662D91.svg?style=for-the-badge&logo=cshrp&logoColor=white)](#)
![.NET MAUI](https://img.shields.io/badge/.NET_MAUI-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

~✦~

> BCDE222 Best Programming Practices (C#.NET)
>
> A C#/.NET implementation of the classic Knight’s Tale problem, inspired by maze-based traversal challenges. As part of the **BCDE222 Best Programming Practices (C#.NET)** course, this project explores how a knight can navigate a constrained board (maze) while visiting valid positions under chess movement rules. It demonstrates algorithmic problem-solving, backtracking strategies, and clean state management in .NET.

</div>

## Branch notes: `dev`

**Purpose**: Primary integration branch for active development. Features, fixes and refactors are merged here before being promoted to main.

**Notes**

- Keep builds passing (npm run lint and npm run test:examples).
- Merge tested work only.
- Sync regularly with main.
- May be unstable; do not demo from here.

Status: Building the pipeline, invariants and property tests; planted-defect demo in progress (In Progress)

## Installation & Setup

### 1. Requirements

| Requirement | Version | Required? |
|---|---|---|
| [.NET SDK](https://dotnet.microsoft.com/download) | **8.0 or newer** (8, 9 and 10 all work) | Yes |
| .NET Runtime (`Microsoft.NETCore.App`) | **8.0.x** | Yes (included with the .NET 8 SDK) |
| [Git](https://git-scm.com/downloads) | Any recent version | Only to clone the repo |
| [VS Code](https://code.visualstudio.com/) | Any recent version | Optional (or use Visual Studio) |

The project targets `net8.0`. A newer SDK (e.g. 10) can build it, but the **.NET 8 runtime must be installed to run it**. Installing the .NET 8 SDK covers both.

You do **not** need .NET Framework, the .NET Framework 4.8.1 targeting pack, or Visual Studio.

---

### 2. Check what you already have

Open PowerShell, Command Prompt or the VS Code terminal and run:

```powershell
dotnet --version
```

Shows the SDK version currently in use (e.g. `10.0.403`).

```powershell
dotnet --list-sdks
```

Lists every installed SDK. You want at least one line starting with `8.` or higher.

```powershell
dotnet --list-runtimes
```

Lists every installed runtime. You need this line to be present:

```powershell
Microsoft.NETCore.App 8.0.x [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
```

`Microsoft.AspNetCore.App` and `Microsoft.WindowsDesktop.App` are **not** needed for this project.


> If something is missing
> Install the .NET 8 SDK from https://dotnet.microsoft.com/download/dotnet/8.0, or with winget:

```powershell
winget install Microsoft.DotNet.SDK.8
```

Then close and reopen your terminal and re-run the checks above.

---

### 3. Get the code

```powershell
git clone https://github.com/arzen1kos/chess-maze.git
cd chess-maze
```

---

### 4. Build and run

From the repository root:

```powershell
dotnet run --project ChessMazeConsoleApp\ChessMazeConsoleApp.csproj
```

Or move into the project folder first:

```powershell
cd ChessMazeConsoleApp\ChessMazeConsoleApp
dotnet run
```

Build only (no run):

```powershell
dotnet build
```

---

### 5. VS Code setup (optional)

Open the repository folder in VS Code (`File > Open Folder`).

#### Required extension

| Extension | ID | Purpose |
|---|---|---|
| **C# Dev Kit** | `ms-dotnettools.csdevkit` | C# language support, IntelliSense, debugging, Solution Explorer. Installs the base **C#** extension (`ms-dotnettools.csharp`) and **.NET Install Tool** automatically. |

Install from the terminal:

```powershell
code --install-extension ms-dotnettools.csdevkit
```

#### Suggested extensions

| Extension | ID | Why |
|---|---|---|
| **IntelliCode for C# Dev Kit** | `ms-dotnettools.vscodeintellicode-csharp` | AI-assisted completions |
| **EditorConfig for VS Code** | `editorconfig.editorconfig` | Consistent formatting across editors |
| **Error Lens** | `usernamehw.errorlens` | Shows errors and warnings inline |
| **GitLens** | `eamodio.gitlens` | Git history and blame in the editor |
| **Markdown Preview Mermaid Support** | `bierner.markdown-mermaid` | Preview diagrams in Markdown files |

Install all suggested extensions at once:

```powershell
code --install-extension ms-dotnettools.vscodeintellicode-csharp
code --install-extension editorconfig.editorconfig
code --install-extension usernamehw.errorlens
code --install-extension eamodio.gitlens
code --install-extension bierner.markdown-mermaid
```

#### Running and debugging in VS Code

1. Open the Command Palette (`Ctrl+Shift+P`).
2. Run **.NET: Generate Assets for Build and Debug** if prompted.
3. Press `F5` to debug, or `Ctrl+F5` to run without debugging.
> Console input is more reliable in the integrated terminal. If input doesn't work while debugging, set `"console": "integratedTerminal"` in `.vscode/launch.json`.

---

### 6. Troubleshooting

| Error | Cause | Fix |
|---|---|---|
| `MSB4184 ... VersionGreaterThanOrEquals(net8.0, 4.0)` | The `.csproj` still has the old (non-SDK) format | Replace the whole file with the SDK-style version below |
| `You must install or update .NET to run this application` | .NET 8 runtime is missing | Install the .NET 8 SDK or runtime |
| `CS2001 Source file '...' could not be found` | A file listed in the `.csproj` doesn't exist | Use the SDK-style `.csproj` (auto-includes files) |
| `CS0101` / `CS0111` duplicate definitions | Leftover duplicate `.cs` files in the folder | Exclude them with `<Compile Remove="..." />` or delete them |
| `CS0579` duplicate attribute | `GenerateAssemblyInfo` is `true` while `Properties\AssemblyInfo.cs` exists | Set `<GenerateAssemblyInfo>false</GenerateAssemblyInfo>` |
| `'dotnet' is not recognized` | .NET is not on your PATH | Reinstall the SDK, then restart the terminal |

After changing the project format, delete the `bin` and `obj` folders and run again.

### Expected `.csproj`

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <RootNamespace>ChessMazeConsoleApp</RootNamespace>
    <AssemblyName>ChessMazeConsoleApp</AssemblyName>
    <GenerateAssemblyInfo>false</GenerateAssemblyInfo>
  </PropertyGroup>
</Project>
```

---

Created by Arsenie — 2024
