# INSS.Forms.Launcher

This project is a Blazor application targeting .NET 8.

## Configure Host Headers for Development Mode
1.	**Configure hosts file**  
	Run as administrator: Notepad.exe and open:  
	C:\Windows\System32\drivers\etc\hosts

	Add the following followed by Save:

	```
	# INSS.Forms.Launcher			
	127.0.0.1 dro.local
	127.0.0.1 dcrs.local

2.  **Generate Self-Signed Certificates**  
	**Note:** Only generate certificates if new ones are needed for development. There are already certificates within the SelfSignedCerts folder.
	
	Run as administrator: PowerShell
	``` 
	New-SelfSignedCertificate -DnsName "dro.local" -CertStoreLocation "cert:\LocalMachine\My"

	New-SelfSignedCertificate -DnsName "dcrs.local" -CertStoreLocation "cert:\LocalMachine\My"
 	
3. **Export Certificates**  
	**Note:** Only export the certificates if new ones are needed for development. There are already certificates within the SelfSignedCerts folder.

	Open the Certificates MMC snap-in:  
	- Run `certlm.msc`  
	- Navigate to Personal > Certificates  
	- Find the newly created certificates, right-click each, and select Export.  
	- Export as a `.pfx` file (e.g. dcrs.locaL.pfx) with a password using AES256 encryption.

4. **Trust the Certificates**  
	- Using File Explorer, navigate to the exported `.pfx` files in the `SelfSignedCerts` folder.
	- Double-click each of the exported .pfx files
		- Import to Local Machine (not current user)
		- Supply the password, this can be found in appSettings.Development.json under Certificates:SelfSignedPassword
		- Check the boxes for 'Mark the key as exportable' and 'Include all extended properties'
		- Place all certificates in the following store: Trusted Route Certification Authorities


## Dependecies
1. **Cosmos DB Instance**
   
   The forms data is stored in a Cosmos DB instance. 
   - Database Name: `Forms`
   - Container Name: `FormInstance`
   - Partition Key: `/formMetadata/formSetInstanceId`
   
	The connection string can be found in the (INSS.Forms.Application.Services.Api) appsettings.json file under `ConnectionString:CosmosDb` or set the environment variable (ConnectionStrings__CosmosDb)

2. **SQL Server Instance**

   The launcher configuration data is stored in a SQL Server instance.
   
   The database can be deployed by publishing this solutions SqlDatabase project: INSS.Forms.Infrastructure.Persistence.Database
   
   The connection string can be found in the (INSS.Forms.Application.Services.Api) appsettings.json file under `ConnectionString:SqlServer` or set the environment variable (ConnectionStrings__SqlServer)
   	

## Build and Run Instructions

1. **Restore dependencies**  
   Run `dotnet restore` in the project directory.

2. **Build the project**  
   Run `dotnet build`.

3. **Run the application**  
   Run `dotnet run` or use Visual Studio's Start Debugging.

## Project Structure

- `Pages/` - Blazor components and pages.
- `wwwroot/` - Static files (CSS, JS, images).
- `Program.cs` - Application entry point.

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 or later (recommended)

## Useful Commands

- `dotnet clean` - Cleans the build outputs.
- `dotnet test` - Runs unit tests.

## Additional Resources

- [Blazor Documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [.NET Documentation](https://learn.microsoft.com/en-us/dotnet/)
