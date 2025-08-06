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
	Run as administrator: PowerShell
	``` 
	New-SelfSignedCertificate -DnsName "dro.local" -CertStoreLocation "cert:\LocalMachine\My"

	New-SelfSignedCertificate -DnsName "dcrs.local" -CertStoreLocation "cert:\LocalMachine\My"
 	
3. **Export Certificates**  
	Open the Certificates MMC snap-in:  
	- Run `certlm.msc`  
	- Navigate to Personal > Certificates  
	- Find the newly created certificates, right-click each, and select Export.  
	- Export as a `.pfx` file (e.g. dcrs.locaL.pfx) with a password using AES256 encryption.

4. **Trust the Certificates**  
	Double-click the exported .pfx files and follow the wizard to import them into Local Machine\Trusted Root Certification Authorities.

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
