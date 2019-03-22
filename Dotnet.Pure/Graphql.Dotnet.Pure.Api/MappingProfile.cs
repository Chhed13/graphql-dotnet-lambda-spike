using AutoMapper;
using graphql.api.Models;

namespace graphql.api
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Character
            CreateMap<core.Models.Character, Character>(MemberList.Destination)
                .ForMember(dest => dest.Friends, opt => opt.Ignore())
                .ForMember(dest => dest.AppearsIn, opt => opt.Ignore()
                );

            // Droid
            CreateMap<core.Models.Droid, Droid>(MemberList.Destination).IncludeBase<core.Models.Character, Character>();

            // Human
            CreateMap<core.Models.Human, Human>(MemberList.Destination)
                .IncludeBase<core.Models.Character, Character>()
                .ForMember(
                    dest => dest.HomePlanet,
                    opt => opt.MapFrom(src => src.HomePlanet.Name)
                );
        }
    }
}