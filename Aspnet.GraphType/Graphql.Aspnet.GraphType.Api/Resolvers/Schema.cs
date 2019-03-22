using System;
using GraphQL;
using GraphQL.Types;

namespace Graphql.Aspnet.GraphType.Api.Resolvers
{
    public class StarWarsSchema : Schema
    {
        public StarWarsSchema(Func<Type, GraphQL.Types.GraphType> resolveType)
            : base(new FuncDependencyResolver(resolveType))
        {
            Query = (StarWarsQuery) resolveType(typeof(StarWarsQuery));
        }
    }
}