# Blazor WASM Application (with server-side prerendering) .Net 5.0

The default Blazor .net 5 demo project updated to use prerendering.

Uses MySQL server - Configure connection string in the appsettings.config.

Creates tenants with connection strings.

Creates defauld user: Admin P@ssword123.

Navigate to localhost:5001 or pig.localhost:5001 or dog.localhost:5001.

Show Employees tab will show different data depending on the route

## Pre-Requisites

Install .Net Core 5.0

Update Visual Studio to version 16.8 or greater

MySQL Server

##Code Sources

[Jon Hilton - Blazor WASM Project](https://jonhilton.net/blazor-wasm-prerendering/)

[Chis Sainty - JWT Auth](https://chrissainty.com/securing-your-blazor-apps-authentication-with-clientside-blazor-using-webapi-aspnet-core-identity/)

## Usage

``` powershell
cd Server
dotnet run
```

or 

``` powershell
cd Server
dotnet watch run
```

Debug Web Assembly in Chrome
```
Shift+alt+D
Follow the instructions
```

If you want changes automatically reflected in the browser...

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
