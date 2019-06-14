# DuplicatePackageFinder
Finds duplicate packages references in all *.*proj (C# and F# project files) in a given folder.



# RabbitMQPurger
Remove all messages in all [Rabbit MQ](https://www.rabbitmq.com/) Queues

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
	{ Element =     <PackageReference Include="Microsoft.VisualStudio.Web.BrowserLink" Version=".." />, Counter = 2 }
	{ Element =     <PackageReference Include="Polly" Version=".." />, Counter = 2 }

	Done


