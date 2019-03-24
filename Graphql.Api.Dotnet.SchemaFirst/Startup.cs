using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using Graphql.Api.Dotnet.SchemaFirst.Resolvers;
using Graphql.Core.Data;
using Graphql.Core.Logic;
using Graphql.Core.Models;
using Graphql.Data.EntityFramework;
using Graphql.Data.InMemory.Repositories;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Graphql.Api.Dotnet.SchemaFirst
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            //AWS_LAMBDA_FUNCTION_NAME - stub to run in Lambda with no extenal DB
            if (Environment.GetEnvironmentVariable("AWS_LAMBDA_FUNCTION_NAME") != "")
            {
                services.AddSingleton<IDroidRepository, DroidRepository>();
                services.AddSingleton<IHumanRepository, HumanRepository>();
                services.AddSingleton<IPlanetRepository, PlanetRepository>();
                services.AddSingleton<IEpisodeRepository, EpisodeRepository>();
                services.AddSingleton<ICharacterRepository, CharacterRepository>();

                services.AddDbContext<Context>(options => options.UseInMemoryDatabase("StarWars"));
            }
            else
            {
                services.AddSingleton<IDroidRepository, Data.EntityFramework.Repositories.DroidRepository>();
                services.AddSingleton<IHumanRepository, Data.EntityFramework.Repositories.HumanRepository>();
                services.AddSingleton<IPlanetRepository, Data.EntityFramework.Repositories.PlanetRepository>();
                services.AddSingleton<IEpisodeRepository, Data.EntityFramework.Repositories.EpisodeRepository>();
                services.AddSingleton<ICharacterRepository, Data.EntityFramework.Repositories.CharacterRepository>();

                string connString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
                if (string.IsNullOrEmpty(connString))
                {
                    throw new InvalidConstraintException("Connection string is null or empty");
                }

                services.AddDbContext<Context>(options =>
                    options.UseNpgsql(connString)).AddEntityFrameworkNpgsql();
            }

            services.AddSingleton<ITrilogyHeroes, TrilogyHeroes>();

            var assembly = Assembly.GetExecutingAssembly();
            var resourceStream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.SDL.schema.graphqls");

            if (resourceStream == null)
            {
                throw new Exception("Schema content is not provided");
            }

            string schema;
            using (var sr = new StreamReader(resourceStream, Encoding.UTF8))
            {
                schema = sr.ReadToEnd();
            }

            services.AddSingleton<Query>();
            services.AddSingleton<Mutation>();
            services.AddSingleton<CharacterResolver<Droid>>();
            services.AddSingleton<DroidResolver>();
            services.AddSingleton<CharacterResolver<Human>>();
            services.AddSingleton<HumanResolver>();

            services.AddSingleton(s => Schema.For(
                schema,
                _ =>
                {
                    _.DependencyResolver = new FuncDependencyResolver(s.GetRequiredService);
                    _.Types.Include<Query>();
                    _.Types.Include<Mutation>();
                    _.Types.Include<DroidResolver>();
                    _.Types.Include<HumanResolver>();
                }));

//            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
        }
    }
}