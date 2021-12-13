---
title: Demo Application to try out some Analyzer stuff
---

## .NET Analyzers

<br/><br/>
Guy Van den Nieuwenhof
<br/>
<a href="https://github.com/guyvdn">github.com/guyvdn</a>

---

## Overview

<ul>
  <li class="fragment fade-in-then-semi-out">.NET compiler platform (Roslyn) analyzers inspect your code quality and style issues</li>
  <li class="fragment fade-in-then-semi-out">Starting in .NET 5, these analyzers are included with the .NET SDK and you don't need to install them separately</li>
  <li class="fragment fade-in-then-semi-out">If your project targets .NET 5 or later, code analysis is enabled by default</li>
  <li class="fragment fade-in-then-semi-out">For .NET Core, .NET Standard, or .NET Framework, you must manually enable code analysis by setting the EnableNETAnalyzers property to true</li>
</ul>

---

## Installing additional analyzers

<ul>
  <li class="fragment fade-in-then-semi-out">Visual Studio Extension (only run in IDE)</li>
  <li class="fragment fade-in-then-semi-out">NuGet Package (can run on build and CI/CD)</li>
</ul>
---

## Directory.Build.props

```xml
<ItemGroup Label="Code Analyzers">
    <PackageReference Include="StyleCop.Analyzers" Version="*" PrivateAssets="All" />
    <PackageReference Include="AsyncFixer" Version="*" PrivateAssets="All" />
    <PackageReference Include="Microsoft.VisualStudio.Threading.Analyzers" Version="*" PrivateAssets="All" />
    <PackageReference Include="Meziantou.Analyzer" Version="*" PrivateAssets="All" />
    <PackageReference Include="SonarAnalyzer.CSharp" Version="*" PrivateAssets="All" />
    <PackageReference Include="Roslynator.Analyzers" Version="*" PrivateAssets="All" />
    <PackageReference Include="Roslynator.CodeAnalysis.Analyzers" Version="*" PrivateAssets="All" />
    <PackageReference Include="Roslynator.Formatting.Analyzers" Version="*" PrivateAssets="All" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Analyzers" Version="*" PrivateAssets="All" />
</ItemGroup>

<ItemGroup Label="Test Project Code Analyzers" Condition="$(MSBuildProjectName.EndsWith('Tests'))">
    <PackageReference Include="FluentAssertions.Analyzers" Version="*" PrivateAssets="All" />
</ItemGroup>
```

---


## Analyzer settings

```xml
<PropertyGroup Label="Analyzer settings">
    <!-- Run code analysis during build -->
    <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
    <!-- Specify a set of code analyzers to run according to a .NET release --> 
    <AnalysisLevel>latest</AnalysisLevel>
    <!-- Customize the set of rules that are enabled by default -->
    <AnalysisMode>All</AnalysisMode>
    <!-- Instruct the different language Compilers to treat warnings as errors -->
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    <!-- Instruct Code Analysis to treat warnings as errors -->
    <CodeAnalysisTreatWarningsAsErrors>false</CodeAnalysisTreatWarningsAsErrors>
    <!-- To measure the impact of each rule -->
    <ReportAnalyzer>false</ReportAnalyzer>
    <!-- For projects that are not .NET 5 or later --> 
    <EnableNETAnalyzers>true</EnableNETAnalyzers>
    <RunAnalyzersDuringBuild>true</RunAnalyzersDuringBuild>
    <RunAnalyzersDuringLiveAnalysis>true</RunAnalyzersDuringLiveAnalysis>
</PropertyGroup>	
```

---

## Severity levels of analyzers

<ul>
  <li class="fragment fade-in-then-semi-out">Error: appear in error list and command line output, cause builds to fail</li>
  <li class="fragment fade-in-then-semi-out">Warning: appear in error list and command line output, do not cause builds to fail</li>
  <li class="fragment fade-in-then-semi-out">Info: appear in error list but not in command line output</li>
  <li class="fragment fade-in-then-semi-out">Hidden: not visbile to the user (reported to IDE)</li>
  <li class="fragment fade-in-then-semi-out">None: suprressed completely</li>
  <li class="fragment fade-in-then-semi-out">Default: default severity of the rule.</li>
</ul>

----

## Change Severity level

----

### .editorconfig file

```csharp
[*.cs]

# SA1600: Elements should be documented
dotnet_diagnostic.SA1600.severity = error

# S1186: Methods should not be empty
dotnet_diagnostic.S1186.severity = warning
```

----

### Directory.Build.props

```xml
<PropertyGroup>
    <WarningsAsErrors>$(WarningsAsErrors);SA1200</WarningsAsErrors>
    <WarningsNotAsErrors>$(WarningsNotAsErrors);MA0004</WarningsNotAsErrors>
</PropertyGroup>	
```
---

## Suppress code analysis warnings

----

### .editorconfig file

```csharp
[*.cs]
# SA1600: Elements should be documented
dotnet_diagnostic.SA1600.severity = error

[*.MyGenerated.cs]
generated_code = true
```
----

### inline 

```csharp
#pragma warning disable CA2200 // Rethrow to preserve stack details
    throw e;
#pragma warning restore CA2200 // Rethrow to preserve stack details
```

----

### attribute 

```csharp
[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2200:Rethrow to preserve stack details", Justification = "¯\_(ツ)_/¯")]
private static void IngorableCharacters()
{ ... }
```

----

### Directory.Build.props

```xml
<PropertyGroup>
    <NoWarn>$(NoWarn);SA1600</NoWarn>
</PropertyGroup>	
```

---

### Further reading

<div style="font-size:0.6em">

Overview of source code analysis<br/>
https://docs.microsoft.com/en-us/visualstudio/code-quality/roslyn-analyzers-overview

AnalysisLevel<br/>
https://docs.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props#analysislevel

AnalysisMode<br/>
https://docs.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props#analysismode

Understanding the impact of Roslyn Analyzers on build time<br/>
https://www.meziantou.net/understanding-the-impact-of-roslyn-analyzers-on-the-build-time.htm
