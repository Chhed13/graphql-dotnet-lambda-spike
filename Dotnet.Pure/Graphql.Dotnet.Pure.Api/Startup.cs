using System;
using System.Data;
using AutoMapper;
using graphql.api.Models;
using graphql.core.Data;
using graphql.core.Logic;
using graphql.data.EntityFramework;
using graphql.data.InMemory;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace graphql.api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddAutoMapper();

            //AWS_LAMBDA_FUNCTION_NAME - stub to run in Lambda with no extenal DB
            if (Environment.GetEnvironmentVariable("AWS_LAMBDA_FUNCTION_NAME") != "")
            {
                services.AddSingleton<IDroidRepository, DroidRepository>();
                services.AddSingleton<IHumanRepository, HumanRepository>();
                services.AddSingleton<IPlanetRepository, PlanetRepository>();
                services.AddSingleton<IEpisodeRepository, EpisodeRepository>();
                services.AddSingleton<ICharacterRepository, CharacterRepository>();

                services.AddDbContext<StarWarsContext>(options => options.UseInMemoryDatabase("StarWars"));
            }
            else
            {
                string connString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
                if (string.IsNullOrEmpty(connString))
                {
                    throw new InvalidConstraintException("Connection string is null or empty");
                }
                services.AddDbContext<StarWarsContext>(options => options.UseNpgsql(connString)).AddEntityFrameworkNpgsql();

                services.AddSingleton<IDroidRepository, data.EntityFramework.Repositories.DroidRepository>();
                services.AddSingleton<IHumanRepository, data.EntityFramework.Repositories.HumanRepository>();
                services.AddSingleton<IPlanetRepository, data.EntityFramework.Repositories.PlanetRepository>();
                services.AddSingleton<IEpisodeRepository, data.EntityFramework.Repositories.EpisodeRepository>();
                services.AddSingleton<ICharacterRepository, data.EntityFramework.Repositories.CharacterRepository>();
            }

            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<ITrilogyHeroes, TrilogyHeroes>();
            services.AddSingleton<DroidType>();
            services.AddSingleton<HumanType>();
            services.AddSingleton<CharacterInterface>();
            services.AddSingleton<EpisodeEnum>();
            services.AddSingleton<StarWarsQuery>();

            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(_ => new StarWarsSchema(type => (GraphType) sp.GetService(type))
                {Query = sp.GetService<StarWarsQuery>()});
        }
    }
}