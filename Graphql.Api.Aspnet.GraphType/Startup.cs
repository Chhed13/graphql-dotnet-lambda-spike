﻿using System;
using Graphql.Api.Aspnet.GraphType.Models;
using Graphql.Api.Aspnet.GraphType.Resolvers;
using Graphql.Core.Data;
using Graphql.Core.Logic;
using Graphql.Data.EntityFramework;
using Graphql.Data.EntityFramework.Seed;
using Graphql.Data.InMemory;
using Graphql.Data.InMemory.Repositories;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Schema = Graphql.Api.Aspnet.GraphType.Resolvers.Schema;

namespace Graphql.Api.Aspnet.GraphType
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Env = env;
        }

        public IConfigurationRoot Configuration;
        public readonly IHostingEnvironment Env;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            //AWS_LAMBDA_FUNCTION_NAME - stub to run in Lambda with no extenal DB
            if (Env.IsEnvironment("Test") ||
                !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("AWS_LAMBDA_FUNCTION_NAME")))
            {
                services.AddSingleton<IDroidRepository, DroidRepository>();
                services.AddSingleton<IHumanRepository, HumanRepository>();
                services.AddSingleton<IPlanetRepository, PlanetRepository>();
                services.AddSingleton<IEpisodeRepository, EpisodeRepository>();
                services.AddSingleton<ICharacterRepository, CharacterRepository>();

                services.AddDbContext<Context>(options =>
                    options.UseInMemoryDatabase("StarWars"));
                services.AddSingleton<InMemoryContext>();
            }
            else
            {
                services.AddSingleton<IDroidRepository, Data.EntityFramework.Repositories.DroidRepository>();
                services.AddSingleton<IHumanRepository, Data.EntityFramework.Repositories.HumanRepository>();
                services.AddSingleton<IPlanetRepository, Data.EntityFramework.Repositories.PlanetRepository>();
                services.AddSingleton<IEpisodeRepository, Data.EntityFramework.Repositories.EpisodeRepository>();
                services.AddSingleton<ICharacterRepository, Data.EntityFramework.Repositories.CharacterRepository>();

                services.AddDbContext<Context>(options =>
                        options.UseNpgsql(Configuration["ConnectionStrings:DatabaseConnection"]))
                    .AddEntityFrameworkNpgsql();
            }

            services.AddSingleton<ITrilogyHeroes, TrilogyHeroes>();
            services.AddSingleton<DroidType>();
            services.AddSingleton<DroidTypeInput>();
            services.AddSingleton<HumanType>();
            services.AddSingleton<CharacterInterface>();
            services.AddSingleton<EpisodeEnum>();
            services.AddSingleton<Query>();
            services.AddSingleton<Mutation>();

            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(_ => new Schema(type => (GraphQL.Types.GraphType) sp.GetService(type)));

            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
                options.ExposeExceptions = true;
            }).AddDataLoader();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            Context db, ISchema schema)
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
            schema.Initialize();

            if (!Env.IsEnvironment("Test") && Environment.GetEnvironmentVariable("AWS_LAMBDA_FUNCTION_NAME") == "")
            {
                db.EnsureSeedData();
            }
        }
    }
}