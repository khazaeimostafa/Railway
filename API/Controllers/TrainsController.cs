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
    public class TrainsController : ControllerBase
    {
        private readonly IGenericRepository<Train> repository;

        private readonly StoreContext context;

        private readonly IMapper mapper;

        public TrainsController(
            IGenericRepository<Train> repository,
            StoreContext context,
            IMapper mapper
        )
        {
            this.repository = repository;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> CreateTrain(TrainRequestDto trainDto)
        {
            var train = mapper.Map<Train>(trainDto);
            repository.Add (train);
            await context.SaveChangesAsync();
            return Ok(train);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<Pagination<TrainDto>>>
        GetTrains([FromQuery] TrainSpecParams trainSpecParams)
        {
            var spec = new TrainsSpecification(trainSpecParams);

            var totalItems = await repository.CountAsync(spec);

            var trains = repository.ListAsync(spec).Result;

            var data = mapper.Map<IReadOnlyList<TrainDto>>(trains);

            return Ok(new Pagination<TrainDto>(trainSpecParams.PageIndex,
                trainSpecParams.PageSize,
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
        public async Task<ActionResult<TrainDto>> GetTrain(int id)
        {
            var spec = new TrainsSpecification(id);

            var train = await repository.GetEntityWithSpec(spec);

            if (train == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return mapper.Map<TrainDto>(train);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [
            ProducesResponseType(
                typeof (ApiResponse),
                StatusCodes.Status404NotFound)
        ]
        public async Task<ActionResult> DeleteTrain(int id)
        {
            var spec = new TrainsSpecification(id);

            var train = await repository.GetEntityWithSpec(spec);
            repository.Delete (train);

            return Ok(train);
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [
            ProducesResponseType(
                typeof (ApiResponse),
                StatusCodes.Status400BadRequest)
        ]
        public ActionResult<TrainRequestDto>
        Updatetrain(TrainDto trainDto, int Id)
        {
            var train = mapper.Map<Train>(trainDto);
            train.Id = Id;

            try
            {
                repository.Update (train);
                return Ok(train);
            }
            catch
            {
                return BadRequest(new ApiResponse(400));
            }
        }
    }
}
