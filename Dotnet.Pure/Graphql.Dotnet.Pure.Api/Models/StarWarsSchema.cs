using System;
using GraphQL;
using GraphQL.Types;

namespace graphql.api.Models
{
    public class StarWarsSchema : Schema
    {
        public StarWarsSchema(Func<Type, GraphType> resolveType)
            : base(new FuncDependencyResolver(resolveType))
        {
            Query = (StarWarsQuery) resolveType(typeof(StarWarsQuery));
        }
    }
}