using AutoMapper;
using SportPlanner.ModelsDto;
using SportPlanner.Repository.Models;
using SportPlanner.Repository.Models.Extensions;
using SportPlanner.Repository.Models.Static;
using System.Text.Json;

namespace SportPlanner.Repository.Profiles
{
    public class SportPlannerProfile : Profile
    {
        public SportPlannerProfile()
        {
            #region DtoToModel

            CreateMap<EventDto, Event>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => JsonSerializer.Serialize(src.Address, new JsonSerializerOptions())))
                .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.PartitionKey, opt => opt.MapFrom( src => CloudTableConstants.PartitionKeyEvent));

            CreateMap<CreateEventDto, Event>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => JsonSerializer.Serialize(src.Address, new JsonSerializerOptions())))
                .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.PartitionKey, opt => opt.MapFrom(src => CloudTableConstants.PartitionKeyEvent));

            CreateMap<CreateEventDto, EventDto>();

            CreateMap<EventUserDto, EventUser>()
                .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.UserId));

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PartitionKey, opt => opt.MapFrom( src => CloudTableConstants.PartitionKeyEvent));

            CreateMap<UserDto, EventUser>()
                .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PartitionKey, opt => opt.MapFrom(src => CloudTableConstants.PartitionKeyEvent));

            #endregion DtoToModel

            #region ModelToDto

            CreateMap<Event, EventDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RowKey))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => JsonSerializer.Deserialize<AddressDto>(src.Address ?? string.Empty, new JsonSerializerOptions())));

            CreateMap<EventUser, EventUserDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.RowKey));

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RowKey));

            #endregion ModelToDto
        }
    }
}
