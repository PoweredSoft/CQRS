# CQRS

Our implementation of query and command responsability segregation (CQRS).

## Getting Started

> Install nuget package to your awesome project.

| Full Version                 | NuGet                                                                                                                                                                                                                                                              |                                          NuGet Install |
| ---------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | -----------------------------------------------------: |
| PoweredSoft.CQRS.Abstractions     | <a href="https://www.nuget.org/packages/PoweredSoft.CQRS.Abstractions/" target="_blank">[![NuGet](https://img.shields.io/nuget/v/PoweredSoft.CQRS.Abstractions.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/PoweredSoft.CQRS.Asbtractions/)</a>             |     ```PM> Install-Package PoweredSoft.CQRS.Abstractions ``` |
| PoweredSoft.CQRS     | <a href="https://www.nuget.org/packages/PoweredSoft.CQRS/" target="_blank">[![NuGet](https://img.shields.io/nuget/v/PoweredSoft.CQRS.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/PoweredSoft.CQRS/)</a>             |     ```PM> Install-Package PoweredSoft.CQRS ``` |
| PoweredSoft.CQRS.FluentValidation     | <a href="https://www.nuget.org/packages/PoweredSoft.CQRS.FluentValidation/" target="_blank">[![NuGet](https://img.shields.io/nuget/v/PoweredSoft.CQRS.FluentValidation.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/PoweredSoft.CQRS.FluentValidation/)</a>             |     ```PM> Install-Package PoweredSoft.CQRS.FluentValidation ``` |
| PoweredSoft.CQRS.AspNetCore.Abstractions     | <a href="https://www.nuget.org/packages/PoweredSoft.CQRS.AspNetCore.Abstractions/" target="_blank">[![NuGet](https://img.shields.io/nuget/v/PoweredSoft.CQRS.AspNetCore.Abstractions.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/PoweredSoft.CQRS.AspNetCore.Abstractions/)</a>             |     ```PM> Install-Package PoweredSoft.CQRS.AspNetCore.Abstractions ``` |
| PoweredSoft.CQRS.AspNetCore     | <a href="https://www.nuget.org/packages/PoweredSoft.CQRS.AspNetCore/" target="_blank">[![NuGet](https://img.shields.io/nuget/v/PoweredSoft.CQRS.AspNetCore.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/PoweredSoft.CQRS.AspNetCore/)</a>             |     ```PM> Install-Package PoweredSoft.CQRS.AspNetCore ``` |
| PoweredSoft.CQRS.GraphQL.HotChocolate     | <a href="https://www.nuget.org/packages/PoweredSoft.CQRS.GraphQL.HotChocolate/" target="_blank">[![NuGet](https://img.shields.io/nuget/v/PoweredSoft.CQRS.GraphQL.HotChocolate.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/PoweredSoft.CQRS.GraphQL.HotChocolate/)</a>             |     ```PM> Install-Package PoweredSoft.CQRS.GraphQL.HotChocolate ``` |


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

    // enabling gql.
    services
        .AddGraphQLServer()
        .AddProjections()
        .AddQueryType(d => d.Name("Query"))
        .AddPoweredSoftQueries()
        .AddPoweredSoftDynamicQueries()
        .AddMutationType(d => d.Name("Mutation"))
        .AddPoweredSoftMutations();

    services.AddSwaggerGen();
}
```
> Example how to add your queries and commands.

```csharp
private void AddCommands(IServiceCollection services)
{
    services.AddCommand<CreatePersonCommand, CreatePersonCommandHandler>();
    services.AddTransient<IValidator<CreatePersonCommand>, CreatePersonCommandValidator>();

    services.AddCommand<EchoCommand, string, EchoCommandHandler>();
    services.AddTransient<IValidator<EchoCommand>, EchoCommandValidator>();
}

private void AddQueries(IServiceCollection services)
{
    services.AddQuery<PersonQuery, IQueryable<Person>, PersonQueryHandler>();
}
```
# Fluent Validation

We use fluent validation in all of our projects, but we don't want it to be enforced.

If you install. ```PoweredSoft.CQRS.FluentValidation``` you can use this way of registrating your commands.

```chsarp
public void ConfigureServices(IServiceCollection services) 
{
    // without Package.
    services.AddCommand<EchoCommand, string, EchoCommandHandler>();
    services.AddTransient<IValidator<EchoCommand>, EchoCommandValidator>();
}

public void ConfigureServices(IServiceCollection services) 
{
    // with PoweredSoft.CQRS.FluentValidation package.
    services.AddCommandWithValidator<EchoCommand, string, EchoCommandHandler, EchoCommandValidator>();
}
```
