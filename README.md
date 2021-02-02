# CQRS

Our implementation of query and command responsability segregation (CQRS).

## Getting Started

> Install nuget package to your awesome project.

| Full Version                 | NuGet                                                                                                                                                                                                                                                              |                                          NuGet Install |
| ---------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | -----------------------------------------------------: |
| PoweredSoft.CQRS.Abstractions     | <a href="https://www.nuget.org/packages/PoweredSoft.CQRS.Abstractions/" target="_blank">[![NuGet](https://img.shields.io/nuget/v/PoweredSoft.CQRS.Abstractions.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/PoweredSoft.CQRS.Asbtractions/)</a>             |     ```PM> Install-Package PoweredSoft.CQRS.Abstractions ``` |
| PoweredSoft.CQRS     | <a href="https://www.nuget.org/packages/PoweredSoft.CQRS/" target="_blank">[![NuGet](https://img.shields.io/nuget/v/PoweredSoft.CQRS.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/PoweredSoft.CQRS/)</a>             |     ```PM> Install-Package PoweredSoft.CQRS ``` |
| PoweredSoft.CQRS.AspNetCore.Abstractions     | <a href="https://www.nuget.org/packages/PoweredSoft.CQRS.AspNetCore.Abstractions/" target="_blank">[![NuGet](https://img.shields.io/nuget/v/PoweredSoft.CQRS.AspNetCore.Abstractions.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/PoweredSoft.CQRS.AspNetCore.Abstractions/)</a>             |     ```PM> Install-Package PoweredSoft.CQRS.AspNetCore.Abstractions ``` |
| PoweredSoft.CQRS.AspNetCore     | <a href="https://www.nuget.org/packages/PoweredSoft.CQRS.AspNetCore/" target="_blank">[![NuGet](https://img.shields.io/nuget/v/PoweredSoft.CQRS.AspNetCore.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/PoweredSoft.CQRS.AspNetCore/)</a>             |     ```PM> Install-Package PoweredSoft.CQRS.AspNetCore ``` |


## Sample of startup code for aspnetcore MVC

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // make sure to add your queries and commands before the .AddPoweredSoftQueries and .AddPoweredSoftCommands
    AddQueries(services);
    AddCommands(services);

    // adds the non related to aspnet core features.
    services.AddPoweredSoftCQRS();
    
    services
        .AddControllers()
        .AddPoweredSoftQueries() // adds queries to aspnetcore mvc.(you can make it configurable to load balance only commands on a instance)
        .AddPoweredSoftCommands() // adds commands to aspnetcore mvc. (you can make it configurable to load balance only commands on a instance)
        .AddFluentValidation();

    services.AddSwaggerGen();
}
```

