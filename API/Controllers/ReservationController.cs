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
using Core.Interfaces.Bhrugen;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IUnitOfWorkBhrugen uow;

        private readonly IMapper mapper;

        public ReservationController(IUnitOfWorkBhrugen Uow, IMapper mapper)
        {
            uow = Uow;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<IReadOnlyList<Reservation>>>
        CreateReservation(ReservationRequestDto model)
        {
            Reservation reservation =
                new Reservation {
                    PassengerId = model.PassengerId,
                    TravelId = model.TravelId
                };

            uow.ReserveRepository.Add (reservation); //CreateReservation(reservation);

            return Ok(reservation);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ReservationReturnDto>>>
        GetReserves([FromQuery] ReservationSpecParams reserveParams)
        {
            var spec = new ReservationSpecification(reserveParams);
            var totalItems = await uow.ReserveRepository.CountAsync(spec);

            var reserves = await uow.ReserveRepository.GetEntityWithSpec(spec);

            var Data =
                mapper.Map<IReadOnlyList<ReservationReturnDto>>(reserves);

            return Ok(new Pagination<ReservationReturnDto>(reserveParams
                    .PageIndex,
                reserveParams.PageSize,
                totalItems,
                Data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationReturnDto>> GetReserve(int id)
        {
            var spec = new ReservationSpecification(id);

            var reserve = await uow.ReserveRepository.GetFirstOrDefault(spec);

            var Data = mapper.Map<ReservationReturnDto>(reserve);

            return Ok(Data);
        }

        
    }
}
