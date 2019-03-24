using System;
using System.Collections.Generic;
using Graphql.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Graphql.Data.InMemory
{
    public class InMemoryContext
    {
        private readonly ILogger _logger;

        private readonly List<Episode> _episodes = new List<Episode>();
        private readonly List<Planet> _planets = new List<Planet>();
        private readonly List<Character> _characters = new List<Character>();
        private readonly List<CharacterFriend> _characterFriends = new List<CharacterFriend>();
        private readonly List<CharacterEpisode> _characterEpisodes = new List<CharacterEpisode>();
        private readonly List<Droid> _droids = new List<Droid>();
        private readonly List<Human> _humans = new List<Human>();

        public IServiceProvider Provider;

        public InMemoryContext(ILogger<InMemoryContext> logger)
        {
            _logger = logger;

            var sp = new ServiceCollection();
            sp.AddSingleton(typeof(List<Episode>), _episodes);
            sp.AddSingleton(typeof(List<Planet>), _planets);
            sp.AddSingleton(typeof(List<Character>), _characters);
            sp.AddSingleton(typeof(List<CharacterFriend>), _characterFriends);
            sp.AddSingleton(typeof(List<CharacterEpisode>), _characterEpisodes);
            sp.AddSingleton(typeof(List<Droid>), _droids);
            sp.AddSingleton(typeof(List<Human>), _humans);

            Provider = sp.BuildServiceProvider();

            Initialize();
        }

        private void Initialize()
        {
            // episodes
            var newhope = new Episode {Id = 4, Title = "NEWHOPE"};
            var empire = new Episode {Id = 5, Title = "EMPIRE"};
            var jedi = new Episode {Id = 6, Title = "JEDI"};
            var episodes = new List<Episode>
            {
                newhope,
                empire,
                jedi,
            };
            _logger.LogInformation("Seeding episodes");
            _episodes.AddRange(episodes);

            // planets
            var tatooine = new Planet {Id = 1, Name = "Tatooine"};
            var alderaan = new Planet {Id = 2, Name = "Alderaan"};
            var planets = new List<Planet>
            {
                tatooine,
                alderaan
            };
            _logger.LogInformation("Seeding planets");
            _planets.AddRange(planets);

            // humans
            var luke = new Human
            {
                Id = 1000,
                Name = "Luke Skywalker",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode {Episode = newhope},
                    new CharacterEpisode {Episode = empire},
                    new CharacterEpisode {Episode = jedi}
                },
                HomePlanet = tatooine
            };
            var vader = new Human
            {
                Id = 1001,
                Name = "Darth Vader",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode {Episode = newhope},
                    new CharacterEpisode {Episode = empire},
                    new CharacterEpisode {Episode = jedi}
                },
                HomePlanet = tatooine
            };
            var han = new Human
            {
                Id = 1002,
                Name = "Han Solo",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode {Episode = newhope},
                    new CharacterEpisode {Episode = empire},
                    new CharacterEpisode {Episode = jedi}
                },
                HomePlanet = tatooine
            };
            var leia = new Human
            {
                Id = 1003,
                Name = "Leia Organa",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode {Episode = newhope},
                    new CharacterEpisode {Episode = empire},
                    new CharacterEpisode {Episode = jedi}
                },
                HomePlanet = alderaan
            };
            var tarkin = new Human
            {
                Id = 1004,
                Name = "Wilhuff Tarkin",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode {Episode = newhope}
                },
            };
            var humans = new List<Human>
            {
                luke,
                vader,
                han,
                leia,
                tarkin
            };
            _logger.LogInformation("Seeding humans");
            _humans.AddRange(humans);

            // droids
            var threepio = new Droid
            {
                Id = 2000,
                Name = "C-3PO",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode {Episode = newhope},
                    new CharacterEpisode {Episode = empire},
                    new CharacterEpisode {Episode = jedi}
                },
                PrimaryFunction = "Protocol"
            };
            var artoo = new Droid
            {
                Id = 2001,
                Name = "R2-D2",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode {Episode = newhope},
                    new CharacterEpisode {Episode = empire},
                    new CharacterEpisode {Episode = jedi}
                },
                PrimaryFunction = "Astromech"
            };
            var droids = new List<Droid>
            {
                threepio,
                artoo
            };
            _logger.LogInformation("Seeding droids");
            _droids.AddRange(droids);

            // update character's friends
            luke.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend {Friend = han},
                new CharacterFriend {Friend = leia},
                new CharacterFriend {Friend = threepio},
                new CharacterFriend {Friend = artoo}
            };
            vader.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend {Friend = tarkin}
            };
            han.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend {Friend = luke},
                new CharacterFriend {Friend = leia},
                new CharacterFriend {Friend = artoo}
            };
            leia.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend {Friend = luke},
                new CharacterFriend {Friend = han},
                new CharacterFriend {Friend = threepio},
                new CharacterFriend {Friend = artoo}
            };
            tarkin.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend {Friend = vader}
            };
            threepio.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend {Friend = luke},
                new CharacterFriend {Friend = han},
                new CharacterFriend {Friend = leia},
                new CharacterFriend {Friend = artoo}
            };
            artoo.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend {Friend = luke},
                new CharacterFriend {Friend = han},
                new CharacterFriend {Friend = leia}
            };
            var characters = new List<Character>
            {
                luke,
                vader,
                han,
                leia,
                tarkin,
                threepio,
                artoo
            };
            _logger.LogInformation("Seeding character's friends");
            _characters.AddRange(characters);

            newhope.Hero = artoo;
            empire.Hero = luke;
            jedi.Hero = luke;
        }
    }
}