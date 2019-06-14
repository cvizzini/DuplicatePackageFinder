# DuplicatePackageFinder
Finds duplicate packages references in all *proj (C# and F# project files) in a given folder.
DotNet Core 2.2 console application


----
## Example

> After a git-merge the following could happen with PackageReference
> This app will identify the duplicates in your *proj files

    <PackageReference Include="Microsoft.Extensions.Configuration" Version="2.2.0" />
	<PackageReference Include="Microsoft.Extensions.Configuration" Version="2.2.0" />
	<PackageReference Include="Microsoft.Extensions.Configuration" Version="2.1.0" />
	<PackageReference Include="Polly" Version="1.0.0" />
	<PackageReference Include="Polly" Version="2.0.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.FileExtensions" Version="2.2.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="2.2.0" />


----
## Usage

Set the connection settings in the appsettings.config

    {
		  "folderPath": "D:\\Source\\Test",
		  "searchFilePattern": "*sproj"
	}

Build and Run

## Example Expected Output

    Finding Duplicate Package References
	Looking for files with pattern: *sproj in path: D:\Source\Test
	D:\Source\Test\Api.Core\Api.Core.csproj
	{ Element =     <PackageReference Include="Microsoft.Extensions.Configuration" Version=".." />, Counter = 3 }
	{ Element =     <PackageReference Include="Polly" Version=".." />, Counter = 2 }

	Done


