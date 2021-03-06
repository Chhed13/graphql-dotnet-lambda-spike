using System;
using GraphQL;
using GraphQL.Types;

namespace Graphql.Api.Aspnet.GraphType.Resolvers
{
    public class Schema : GraphQL.Types.Schema
    {
        public Schema(Func<Type, GraphQL.Types.GraphType> resolveType)
            : base(new FuncDependencyResolver(resolveType))
        {
            Query = (Query) resolveType(typeof(Query));
            Mutation = (Mutation) resolveType(typeof(Mutation));
        }
    }
}