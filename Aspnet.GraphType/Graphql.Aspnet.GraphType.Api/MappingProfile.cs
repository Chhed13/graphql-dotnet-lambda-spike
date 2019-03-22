//using AutoMapper;
//using Graphql.Aspnet.GraphType.Api.Models;
//
//namespace Graphql.Aspnet.GraphType.Api
//{
//    public class MappingProfile : Profile
//    {
//        public MappingProfile()
//        {
//            // Character
//            CreateMap<Graphql.Aspnet.GraphType.Core.Models.Character, Character>(MemberList.Destination)
//                .ForMember(dest => dest.Friends, opt => opt.Ignore())
//                .ForMember(dest => dest.AppearsIn, opt => opt.Ignore()
//                );
//
//            CreateMap<Character, Graphql.Aspnet.GraphType.Core.Models.Character>(MemberList.Destination)
//                .ForMember(dest => dest.CharacterFriends, opt => opt.Ignore())
//                .ForMember(dest => dest.CharacterEpisodes, opt => opt.Ignore()
//                );
//
//            // Droid
//            CreateMap<Graphql.Aspnet.GraphType.Core.Models.Droid, Droid>(MemberList.Destination).IncludeBase<Graphql.Aspnet.GraphType.Core.Models.Character, Character>();
//            CreateMap<Droid, Graphql.Aspnet.GraphType.Core.Models.Droid>(MemberList.Destination).IncludeBase<Character, Graphql.Aspnet.GraphType.Core.Models.Character>();
//
//            // Human
//            CreateMap<Graphql.Aspnet.GraphType.Core.Models.Human, Human>(MemberList.Destination)
//                .IncludeBase<Graphql.Aspnet.GraphType.Core.Models.Character, Character>()
//                .ForMember(
//                    dest => dest.HomePlanet,
//                    opt => opt.MapFrom(src => src.HomePlanet.Name)
//                );
//
//            CreateMap<Human, Graphql.Aspnet.GraphType.Core.Models.Human>(MemberList.Destination)
//                .IncludeBase<Character, Graphql.Aspnet.GraphType.Core.Models.Character>()
//                .ForMember(
//                    dest => dest.HomePlanet,
//                    opt => opt.MapFrom(src => src.HomePlanet)
//                );
//        }
//    }
//}