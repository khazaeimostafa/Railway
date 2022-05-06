using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace API.Helpers
{
    public class MapTravelDtoResolver : IValueResolver<Travel, TravelDto, Train>
    {
        private readonly IServiceProvider service;

        public MapTravelDtoResolver(IServiceProvider service)
        {
            this.service = service;
        }

        public Train
        Resolve(
            Travel source,
            TravelDto destination,
            Train destMember,
            ResolutionContext context
        )
        {
            var ctx = service.GetRequiredService<StoreContext>();
            Train train =
                ctx.Trains.Where(x => x.Id == source.TrainId).FirstOrDefault();

            return train;
        }
    }
}
