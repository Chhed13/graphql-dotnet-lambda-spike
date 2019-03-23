# Spike of how to build Graphql service with lambda and .Net

- first version based on usage of `AspNet` technology
- second - pure .Net (not implemented yet) 

Feature - look inside of each implementation

To run with InMemoryMod
`export ASPNETCORE_ENVIRONMENT=Test && dotnet run -p Aspnet/Graphql.Aspnet.Api.GraphType`

To run with real Postgres
`dotnet run -p Aspnet/Graphql.Aspnet.Api.GraphType`