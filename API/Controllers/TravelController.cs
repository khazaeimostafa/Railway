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
    public class TravelController : ControllerBase
    {
        private readonly IGenericRepository<Travel> repository;

        private readonly StoreContext context;

        private readonly IMapper mapper;

        public TravelController(
            IGenericRepository<Travel> repository,
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
        public async Task<ActionResult> CreateTravel(TravelRequestDto travelDto)
        {
            var travel = mapper.Map<Travel>(travelDto);
            repository.Add (travel);
            await context.SaveChangesAsync();
            return Ok(travel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<Pagination<TravelDto>>>
        GetTravels([FromQuery] TravelSpecParams travelSpecParams)
        {
            var spec = new TravelsSpecification(travelSpecParams);

            var totalItems = await repository.CountAsync(spec);

            var travels =
                repository
                    .ListAsync(spec)
                    .Result
                    .Select(x =>
                        new Travel {
                            Cost = x.Cost,
                            Date = x.Date,
                            Dest = x.Dest,
                            Reservation = null,
                            TrainId = x.TrainId,
                            Train = null
                        });

            var data = mapper.Map<IReadOnlyList<TravelDto>>(travels);

            return Ok(new Pagination<TravelDto>(travelSpecParams.PageIndex,
                travelSpecParams.PageSize,
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
        public async Task<ActionResult<TravelDto>> GetTravel(int id)
        {
            var spec = new TravelsSpecification(id);

            var travel = await repository.GetEntityWithSpec(spec);

            if (travel == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return mapper.Map<TravelDto>(travel);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [
            ProducesResponseType(
                typeof (ApiResponse),
                StatusCodes.Status404NotFound)
        ]
        public async Task<ActionResult> DeleteTravel(int id)
        {
            var spec = new TravelsSpecification(id);

            var travel = await repository.GetEntityWithSpec(spec);
            repository.Delete (travel);

            return Ok(travel);
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [
            ProducesResponseType(
                typeof (ApiResponse),
                StatusCodes.Status400BadRequest)
        ]
        public ActionResult<TravelDto>
        Updatetravel(TravelRequestDto travelDto, int Id)
        {
            var travel = mapper.Map<Travel>(travelDto);
            travel.Id = Id;

            try
            {
                repository.Update (travel);
                return Ok(travel);
            }
            catch
            {
                return BadRequest(new ApiResponse(400));
            }
        }
    }
}
