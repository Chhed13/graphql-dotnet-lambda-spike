using System;
using Graphql.Aspnet.GraphType.Api.Models;
using Graphql.Aspnet.GraphType.Core.Data;
using Graphql.Aspnet.GraphType.Core.Logic;
using Graphql.Aspnet.GraphType.Data.EntityFramework;
using Graphql.Aspnet.GraphType.Data.EntityFramework.Seed;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Graphql.Aspnet.GraphType.Api.Resolvers;
using Graphql.Aspnet.GraphType.Data.InMemory;
using Graphql.Aspnet.GraphType.Data.InMemory.Repositories;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.Extensions.Logging;

namespace Graphql.Aspnet.GraphType.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Env = env;
        }

        public IConfigurationRoot Configuration;
        public readonly IHostingEnvironment Env;

        public void ConfigureServices(IServiceCollection services)
        {
            //AWS_LAMBDA_FUNCTION_NAME - stub to run in Lambda with no extenal DB
            if (Env.IsEnvironment("Test") ||
                !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("AWS_LAMBDA_FUNCTION_NAME")))
            {
                services.AddSingleton<IDroidRepository, DroidRepository>();
                services.AddSingleton<IHumanRepository, HumanRepository>();
                services.AddSingleton<IPlanetRepository, PlanetRepository>();
                services.AddSingleton<IEpisodeRepository, EpisodeRepository>();
                services.AddSingleton<ICharacterRepository, CharacterRepository>();

                services.AddDbContext<StarWarsContext>(options =>
                    options.UseInMemoryDatabase("StarWars"));
                services.AddSingleton<StarWarsInMemoryContext>();
            }
            else
            {
                services.AddSingleton<IDroidRepository, Graphql.Aspnet.GraphType.Data.EntityFramework.Repositories.DroidRepository>();
                services.AddSingleton<IHumanRepository, Graphql.Aspnet.GraphType.Data.EntityFramework.Repositories.HumanRepository>();
                services.AddSingleton<IPlanetRepository, Graphql.Aspnet.GraphType.Data.EntityFramework.Repositories.PlanetRepository>();
                services.AddSingleton<IEpisodeRepository, Graphql.Aspnet.GraphType.Data.EntityFramework.Repositories.EpisodeRepository>();
                services.AddSingleton<ICharacterRepository, Graphql.Aspnet.GraphType.Data.EntityFramework.Repositories.CharacterRepository>();

                services.AddDbContext<StarWarsContext>(options =>
                        options.UseNpgsql(Configuration["ConnectionStrings:DatabaseConnection"]))
                    .AddEntityFrameworkNpgsql();
            }

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<ITrilogyHeroes, TrilogyHeroes>();
            services.AddSingleton<DroidType>();
            services.AddSingleton<DroidTypeInput>();
            services.AddSingleton<HumanType>();
            services.AddSingleton<CharacterInterface>();
            services.AddSingleton<EpisodeEnum>();
            services.AddSingleton<StarWarsQuery>();
            services.AddSingleton<Mutation>();

            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(_ =>
                new StarWarsSchema(type => (GraphQL.Types.GraphType) sp.GetService(type))
                {
                    Query = sp.GetService<StarWarsQuery>(),
                    Mutation = sp.GetService<Mutation>()
                }
            );

            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
                options.ExposeExceptions = true;
            }).AddDataLoader();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            StarWarsContext db)
        {
            // https://github.com/aws/aws-lambda-dotnet/tree/master/Libraries/src/Amazon.Lambda.Logging.AspNetCore
            var loggerOptions = new LambdaLoggerOptions(Configuration.GetSection("Lambda.Logging"));

            loggerFactory.AddLambdaLogger(loggerOptions);

            if (true) //(Environment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
            {
                Path = "/playground"
            });
            app.UseGraphQL<ISchema>("");

            if (!Env.IsEnvironment("Test") && Environment.GetEnvironmentVariable("AWS_LAMBDA_FUNCTION_NAME") == "")
            {
                db.EnsureSeedData();
            }
        }
    }
}