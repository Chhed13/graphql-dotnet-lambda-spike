using System.Linq;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Graphql.Tests.Integration.Api
{
    public abstract class QueryTestsBase
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        protected QueryTestsBase(IWebHostBuilder builder)
        {
            _server = new TestServer(builder);
            _client = _server.CreateClient();
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteHeroNameQuery()
        {
            const string query = @"{
                ""query"":
                    ""query HeroNameQuery {
                        hero(episode: NEWHOPE) {
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
            Assert.Equal("R2-D2", result["data"]["hero"]["name"].ToString());
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteHeroNameAndFriendsQuery()
        {
            const string query = @"{
                ""query"":
                    ""query HeroNameAndFriendsQuery {
                        heroes {
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

            Assert.NotNull(result["data"]["heroes"][0]);
            Assert.NotNull(result["data"]["heroes"][0]["friends"]);
            Assert.Equal(3, ((JArray) result["data"]["heroes"][0]["friends"]).Count);
            Assert.Equal("Luke Skywalker", result["data"]["heroes"][0]["friends"][0]["name"].ToString());
            Assert.Equal("Han Solo", result["data"]["heroes"][0]["friends"][1]["name"].ToString());
            Assert.Equal("Leia Organa", result["data"]["heroes"][0]["friends"][2]["name"].ToString());
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteNestedQuery()
        {
            const string query = @"{
                ""query"":
                    ""query NestedQuery {
                        hero(episode: NEWHOPE) {
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
            var episodes = ((JArray) luke["appearsIn"]).Select(e => e.ToString()).ToArray();
            Assert.Equal(new[] {"NEWHOPE", "EMPIRE", "JEDI"}, episodes);
            Assert.Equal(4, ((JArray) luke["friends"]).Count);
            Assert.Equal("Han Solo", luke["friends"][0]["name"].ToString());
            Assert.Equal("Leia Organa", luke["friends"][1]["name"].ToString());
            Assert.Equal("C-3PO", luke["friends"][2]["name"].ToString());
            Assert.Equal("R2-D2", luke["friends"][3]["name"].ToString());
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteFetchLukeQuery()
        {
            const string query = @"{
                ""query"":
                    ""query FetchLukeQuery {
                        human(id: 1000) {
                            id
                            name
                            homePlanet
                            appearsIn
                            friends {
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
            Assert.Equal("1000", result["data"]["human"]["id"].ToString());
            Assert.Equal("Luke Skywalker", result["data"]["human"]["name"].ToString());
            Assert.Equal("Tatooine", result["data"]["human"]["homePlanet"].ToString());
            Assert.Contains("NEWHOPE", result["data"]["human"]["appearsIn"].ToString());
            Assert.Contains("Han Solo", result["data"]["human"]["friends"].ToString());
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteFetchLukeAliased()
        {
            const string query = @"{
                ""query"":
                    ""query FetchLukeAliased {
                        luke: human(id: 1000) {
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
            Assert.NotNull(result["data"]["luke"]);
            Assert.Equal("Luke Skywalker", result["data"]["luke"]["name"].ToString());
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteFetchLukeAndLeiaAliased()
        {
            const string query = @"{
                ""query"":
                    ""query FetchLukeAliased {
                        luke: human(id: 1000) {
                            name
                        }
                        leia: human(id: 1003) {
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
            Assert.NotNull(result["data"]["luke"]);
            Assert.NotNull(result["data"]["leia"]);
            Assert.Equal("Luke Skywalker", result["data"]["luke"]["name"].ToString());
            Assert.Equal("Leia Organa", result["data"]["leia"]["name"].ToString());
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteDuplicateFields()
        {
            const string query = @"{
                ""query"":
                    ""query DuplicateFields {
                        luke: human(id: 1000) {
                            name
                            homePlanet
                        }
                        leia: human(id: 1003) {
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
            Assert.Equal("Luke Skywalker", result["data"]["luke"]["name"].ToString());
            Assert.Equal("Tatooine", result["data"]["luke"]["homePlanet"].ToString());
            Assert.Equal("Leia Organa", result["data"]["leia"]["name"].ToString());
            Assert.Equal("Alderaan", result["data"]["leia"]["homePlanet"].ToString());
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteUseFragment()
        {
            const string query = @"{
                ""query"":
                    ""query UseFragment {
                        luke: human(id: 1000) {
                            ...HumanFragment
                        }
                        leia: human(id: 1003) {
                            ...HumanFragment
                        }
                    }

                    fragment HumanFragment on Human {
                        name
                        homePlanet
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
            Assert.Equal("Luke Skywalker", result["data"]["luke"]["name"].ToString());
            Assert.Equal("Tatooine", result["data"]["luke"]["homePlanet"].ToString());
            Assert.Equal("Leia Organa", result["data"]["leia"]["name"].ToString());
            Assert.Equal("Alderaan", result["data"]["leia"]["homePlanet"].ToString());
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteCheckType()
        {
            const string query = @"{
                ""query"":
                    ""query CheckType {
                        hero(episode: NEWHOPE) {
                            __typename
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
            Assert.Equal("Droid", result["data"]["hero"]["__typename"].ToString());
            Assert.Equal("R2-D2", result["data"]["hero"]["name"].ToString());
        }

        [Fact]
        [Trait("test", "integration")]
        public async void ExecuteCheckTypeOfLuke()
        {
            const string query = @"{
                ""query"":
                    ""query CheckTypeOfLuke {
                       hero(episode: EMPIRE) {
                            __typename
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
            Assert.Equal("Human", result["data"]["hero"]["__typename"].ToString());
            Assert.Equal("Luke Skywalker", result["data"]["hero"]["name"].ToString());
        }
    }
}