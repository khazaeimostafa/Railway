using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Passengersontroller : ControllerBase
    {
        private readonly IGenericRepository<Passenger> repository;

        private readonly StoreContext context;

        private readonly IMapper mapper;

        public Passengersontroller(
            IGenericRepository<Passenger> repository,
            StoreContext context,
            IMapper mapper
        )
        {
            this.repository = repository;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult>
        CreatePassenger(PassengerRequestDto passengerDto)
        {
            var passenger = mapper.Map<Passenger>(passengerDto);
            repository.Add (passenger);
            await context.SaveChangesAsync();
            return Ok(passenger);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<Pagination<PassengerDto>>>
        GetPassengers([FromQuery] PassengerSpecParams passengerSpecParams)
        {
            var spec = new PassengersSpecification(passengerSpecParams);

            var totalItems = await repository.CountAsync(spec);

            var passengers =
                repository
                    .ListAsync(spec)
                    .Result
                    .Select(x =>
                        new Passenger {
                            Address = x.Address,
                            Gender = x.Gender,
                            Id = x.Id,
                            Name = x.Name,
                            Nat = x.Nat,
                            Phone = x.Phone,
                            Reservation = x.Reservation
                        });

            var data = mapper.Map<IReadOnlyList<PassengerDto>>(passengers);

            return Ok(new Pagination<PassengerDto>(passengerSpecParams
                    .PageIndex,
                passengerSpecParams.PageSize,
                10,
                data));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [
            ProducesResponseType(
                typeof (ApiResponse),
                StatusCodes.Status404NotFound)
        ]
        public async Task<ActionResult<PassengerDto>> GetPassenger(int id)
        {
            var spec = new PassengersSpecification(id);

            var passenger = await repository.GetEntityWithSpec(spec);

            if (passenger == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return mapper.Map<PassengerDto>(passenger);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [
            ProducesResponseType(
                typeof (ApiResponse),
                StatusCodes.Status404NotFound)
        ]
        public async Task<ActionResult> DeletePassenger(int id)
        {
            var spec = new PassengersSpecification(id);

            var passenger = await repository.GetEntityWithSpec(spec);
            repository.Delete (passenger);

            return Ok(passenger);
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [
            ProducesResponseType(
                typeof (ApiResponse),
                StatusCodes.Status400BadRequest)
        ]
        public ActionResult<PassengerDto>
        Updatepassenger(PassengerRequestDto passengerDto, int Id)
        {
            var passenger = mapper.Map<Passenger>(passengerDto);
            passenger.Id = Id;

            try
            {
                repository.Update (passenger);
                return Ok(passenger);
            }
            catch
            {
                return BadRequest(new ApiResponse(400));
            }
        }
    }
}
