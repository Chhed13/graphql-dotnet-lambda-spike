using System.Linq;
using System.Net.Http;
using System.Text;
using Graphql.Aspnet.GraphType.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Graphql.Aspnet.GraphType.Tests.Integration.Api
{
    public class GraphQlControllerShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public GraphQlControllerShould()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Test")
                .UseStartup<Startup>());

            _client = _server.CreateClient();
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ReturnR2D2()
        {
            var query = @"{
                ""query"": ""query { hero { id name } }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/graphql", content);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("R2-D2", responseString);
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteHeroNameQuery()
        {
            // Given
            const string query = @"{
                ""query"":
                    ""query HeroNameQuery {
                        hero {
                            name
                        }
                    }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(responseString);

            var result = JObject.Parse(responseString);

            Assert.NotNull(result);
            Assert.True(
                null == result["errors"],
                result["errors"] != null ? result["errors"].ToString() : string.Empty);

            Assert.NotNull(result["data"]);
            Assert.NotNull(result["data"]["hero"]);
            Assert.Equal("R2-D2", (string)result["data"]["hero"]["name"]);
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteHeroNameAndFriendsQuery()
        {
            // Given
            const string query = @"{
                ""query"":
                    ""query HeroNameAndFriendsQuery {
                        hero {
                            id
                            name
                            friends {
                                id
                                name
                            }
                        }
                    }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(responseString);

            var result = JObject.Parse(responseString);

            Assert.NotNull(result);
            Assert.True(
                null == result["errors"],
                result["errors"] != null ? result["errors"].ToString() : string.Empty);

            Assert.NotNull(result["data"]["hero"]);
            Assert.NotNull(result["data"]["hero"]["friends"]);
            Assert.Equal(3, ((JArray)result["data"]["hero"]["friends"]).Count);
            Assert.Equal("Luke Skywalker", (string)result["data"]["hero"]["friends"][0]["name"]);
            Assert.Equal("Han Solo", (string)result["data"]["hero"]["friends"][1]["name"]);
            Assert.Equal("Leia Organa", (string)result["data"]["hero"]["friends"][2]["name"]);
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteNestedQuery()
        {
            // Given
            const string query = @"{
                ""query"":
                    ""query NestedQuery {
                        hero {
                            name
                            friends {
                                name
                                appearsIn
                                friends {
                                    name
                                }
                            }
                        }
                    }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(responseString);

            var result = JObject.Parse(responseString);

            Assert.NotNull(result);
            Assert.True(
                null == result["errors"],
                result["errors"] != null ? result["errors"].ToString() : string.Empty);

            Assert.NotNull(result["data"]);
            Assert.NotNull(result["data"]["hero"]);
            Assert.NotNull(result["data"]["hero"]["friends"]);
            var luke = result["data"]["hero"]["friends"][0];
            var episodes = ((JArray) luke["appearsIn"]).Select(e => (string)e).ToArray();
            Assert.Equal(new[] { "NEWHOPE", "EMPIRE", "JEDI" }, episodes);
            Assert.Equal(4, ((JArray)luke["friends"]).Count);
            Assert.Equal("Han Solo", (string)luke["friends"][0]["name"]);
            Assert.Equal("Leia Organa", (string)luke["friends"][1]["name"]);
            Assert.Equal("C-3PO", (string)luke["friends"][2]["name"]);
            Assert.Equal("R2-D2", (string)luke["friends"][3]["name"]);
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteFetchLukeQuery()
        {
            // Given
            const string query = @"{
                ""query"":
                    ""query FetchLukeQuery {
                        human(id: ""1000"") {
                            name
                        }
                    }
                }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(responseString);

            var result = JObject.Parse(responseString);

            Assert.NotNull(result);
            Assert.True(
                null == result["errors"],
                result["errors"] != null ? result["errors"].ToString() : string.Empty);

            Assert.NotNull(result["data"]);
            Assert.NotNull(result["data"]["human"]);
            Assert.Equal("Luke Skywalker", (string)result["data"]["human"]["name"]);
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteFetchLukeAliased()
        {
            // Given
            const string query = @"{
                ""query"":
                    ""query FetchLukeAliased {
                        luke: human(id: ""1000"") {
                            name
                        }
                    }
                }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(responseString);

            var result = JObject.Parse(responseString);

            Assert.NotNull(result);
            Assert.True(
                null == result["errors"],
                result["errors"] != null ? result["errors"].ToString() : string.Empty);

            Assert.NotNull(result["data"]);
            Assert.NotNull(result["data"]["human"]);
            Assert.Equal("Luke Skywalker", (string)result["data"]["human"]["name"]);
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteFetchLukeAndLeiaAliased()
        {
            // Given
            const string query = @"{
                ""query"":
                    ""query FetchLukeAliased {
                        luke: human(id: ""1000"") {
                            name
                        }
                        leia: human(id: ""1003"") {
                            name
                        }
                    }
                }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(responseString);

            var result = JObject.Parse(responseString);

            Assert.NotNull(result);
            Assert.True(
                null == result["errors"],
                result["errors"] != null ? result["errors"].ToString() : string.Empty);

            Assert.NotNull(result["data"]);
            Assert.NotNull(result["data"]["luke"]);
            Assert.NotNull(result["data"]["leia"]);
            Assert.Equal("Luke Skywalker", (string)result["data"]["luke"]["name"]);
            Assert.Equal("Leia Organa", (string)result["data"]["leia"]["name"]);
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteDuplicateFields()
        {
            // Given
            const string query = @"{
                ""query"":
                    ""query DuplicateFields {
                        luke: human(id: ""1000"") {
                            name
                            homePlanet
                        }
                        leia: human(id: ""1003"") {
                            name
                            homePlanet
                        }
                    }
                }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(responseString);

            var result = JObject.Parse(responseString);

            Assert.NotNull(result);
            Assert.True(
                null == result["errors"],
                result["errors"] != null ? result["errors"].ToString() : string.Empty);

            Assert.NotNull(result["data"]);
            Assert.NotNull(result["data"]["luke"]);
            Assert.NotNull(result["data"]["leia"]);
            Assert.Equal("Luke Skywalker", (string)result["data"]["luke"]["name"]);
            Assert.Equal("Tatooine", (string)result["data"]["luke"]["homePlanet"]);
            Assert.Equal("Leia Organa", (string)result["data"]["leia"]["name"]);
            Assert.Equal("Alderaan", (string)result["data"]["leia"]["homePlanet"]);
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteUseFragment()
        {
            // Given
            const string query = @"{
                ""query"":
                    ""query UseFragment {
                        luke: human(id: ""1000"") {
                            ...HumanFragment
                        }
                        leia: human(id: ""1003"") {
                            ...HumanFragment
                        }
                    }

                    fragment HumanFragment on Human {
                        name
                        homePlanet
                    }
                }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(responseString);

            var result = JObject.Parse(responseString);

            Assert.NotNull(result);
            Assert.True(
                null == result["errors"],
                result["errors"] != null ? result["errors"].ToString() : string.Empty);

            Assert.NotNull(result["data"]);
            Assert.NotNull(result["data"]["luke"]);
            Assert.NotNull(result["data"]["leia"]);
            Assert.Equal("Luke Skywalker", (string)result["data"]["luke"]["name"]);
            Assert.Equal("Tatooine", (string)result["data"]["luke"]["homePlanet"]);
            Assert.Equal("Leia Organa", (string)result["data"]["leia"]["name"]);
            Assert.Equal("Alderaan", (string)result["data"]["leia"]["homePlanet"]);
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteCheckTypeOfR2()
        {
            // Given
            const string query = @"{
                ""query"":
                    ""query CheckTypeOfR2 {
                        hero {
                            __typename
                            name
                        }
                    }
                }""
            }";
            var content = new StringContent(query, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(responseString);

            var result = JObject.Parse(responseString);

            Assert.NotNull(result);
            Assert.True(
                null == result["errors"],
                result["errors"] != null ? result["errors"].ToString() : string.Empty);

            Assert.NotNull(result["data"]);
            Assert.Equal("Droid", (string)result["data"]["hero"]["__typename"]);
            Assert.Equal("R2-D2", (string)result["data"]["hero"]["name"]);
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteCheckTypeOfLuke()
        {
            // Given
            const string query = @"{
                ""query"":
                    ""query CheckTypeOfLuke {
                       hero(episode: EMPIRE) {
                            __typename
                            name
                       }
                    }
                }""
            }";

            var content = new StringContent(query, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(responseString);

            var result = JObject.Parse(responseString);
            
            Assert.NotNull(result);
            Assert.True(
                null == result["errors"],
                result["errors"] != null ? result["errors"].ToString() : string.Empty);

            Assert.NotNull(result["data"]);
            Assert.NotNull(result["data"]["hero"]);
            Assert.Equal("Human", (string)result["data"]["hero"]["__typename"]);
            Assert.Equal("Luke Skywalker", (string)result["data"]["hero"]["name"]);
        }
    }
}