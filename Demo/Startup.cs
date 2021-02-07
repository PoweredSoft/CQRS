using Demo.AsyncProvider;
using Demo.Commands;
using Demo.DynamicQueries;
using Demo.Queries;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PoweredSoft.CQRS;
using PoweredSoft.CQRS.Abstractions;
using PoweredSoft.CQRS.AspNetCore.Mvc;
using PoweredSoft.CQRS.DynamicQuery;
using PoweredSoft.CQRS.DynamicQuery.Abstractions;
using PoweredSoft.CQRS.DynamicQuery.AspNetCore;
using PoweredSoft.CQRS.GraphQL.FluentValidation;
using PoweredSoft.CQRS.GraphQL.HotChocolate;
using PoweredSoft.Data;
using PoweredSoft.Data.Core;
using PoweredSoft.DynamicQuery;
using System.Linq;
using PoweredSoft.CQRS.GraphQL.HotChocolate.DynamicQuery;
using PoweredSoft.CQRS.Abstractions.Security;
using Demo.Security;
using Microsoft.AspNet.OData.Extensions;

namespace Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddQueries(services);
            AddDynamicQueries(services);
            AddCommands(services);

            services.AddHttpContextAccessor();
            services.AddTransient<IQueryAuthorizationService, CommandAndQueryAuthorizationService>();
            services.AddTransient<ICommandAuthorizationService, CommandAndQueryAuthorizationService>();
            services.AddTransient<IAsyncQueryableHandlerService, InMemoryQueryableHandler>();
            services.AddPoweredSoftDataServices();
            services.AddPoweredSoftDynamicQuery();

            services.AddPoweredSoftCQRS();
            services.AddOData();

            services
                .AddControllers()
                .AddPoweredSoftQueries()
                .AddPoweredSoftCommands()
                .AddPoweredSoftDynamicQueries()
                .AddPoweredSoftODataQueries()
                .AddFluentValidation();

            services
                .AddGraphQLServer()
                .AddProjections()
                .AddQueryType(d => d.Name("Query"))
                .AddPoweredSoftQueries()
                .AddPoweredSoftDynamicQueries()
                .AddMutationType(d => d.Name("Mutation"))
                .AddPoweredSoftMutations();

            services.AddPoweredSoftGraphQLFluentValidation();

            services.AddSwaggerGen();
            services.AddCors();
        }

        private void AddDynamicQueries(IServiceCollection services)
        {
            services.AddTransient<IQueryableProvider<Contact>, ContactQueryableProvider>();
            services.AddDynamicQuery<Contact>();
            services.AddDynamicQueryWithParams<Contact, SearchContactParams>(name: "SearchContacts")
                .AddAlterQueryableWithParams<Contact, SearchContactParams, SearchContactParamsService>();

            services
                .AddTransient<IQueryableProvider<Person>, PersonQueryableProvider>()
                .AddDynamicQuery<Person, PersonModel>(name: "People")
                .AddDynamicQueryInterceptors<Person, PersonModel, PersonConvertInterceptor, PersonOptimizationInterceptor>();
        }

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors(o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGraphQL();

                endpoints.Select().Filter().OrderBy().Count().MaxTop(10);
               
                endpoints.MapODataRoute("odata", "odata", endpoints.GetPoweredSoftODataEdmModel());
            });
        }
    }
}
