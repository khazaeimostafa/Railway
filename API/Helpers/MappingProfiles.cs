using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Train, TrainDto>()
                .ForMember(d => d.TravelId, o => o.MapFrom(s => s.Travel.Id));

            CreateMap<TrainDto, Train>()
                .ForMember(d => d.Travel, o => o.Ignore());
            CreateMap<TrainRequestDto, Train>();

            CreateMap<TravelRequestDto, Travel>();

            CreateMap<Travel, TravelDto>();

            CreateMap<Travel, TravelDto>()
                .ForMember(s => s.Train,
                ops => ops.MapFrom<MapTravelDtoResolver>());
            CreateMap<PassengerRequestDto, Passenger>();

            CreateMap<Passenger, PassengerDto>();
            CreateMap<Reservation, ReservationReturnDto>()
                .ForMember(d => d.Passenger,
                o => o.MapFrom(s => s.Passenger.Name));
        }
    }
}
