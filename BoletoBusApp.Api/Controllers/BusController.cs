﻿using BoletoBusApp.Api.Dtos.Configuration.Bus;
using BoletoBusApp.Data.Base;
using BoletoBusApp.Data.Entities.Configuration;
using BoletoBusApp.Data.Interfaces;
using BoletoBusApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoletoBusApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusController : ControllerBase
    {
        private readonly IBusRepository _busRepository;

        public BusController(IBusRepository busRepository)
        {
            _busRepository = busRepository;
        }

        // GET: api/<BusController>
        [HttpGet("GetBuses")]
        public async Task<IActionResult> Get()
        {
            OperationResult<List<BusModel>> result = await _busRepository.GetAll();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // GET api/<BusController>/5
        [HttpGet("GetBusById")]
        public async Task<IActionResult> Get(int id)
        {

            OperationResult<BusModel> result = new OperationResult<BusModel>();

            if (id <= 0)
            {
                result.Success = false;
                result.Message = "El id del bus es inválido";
                return BadRequest(result);
            }


            result = await _busRepository.GetEntityBy(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // POST api/<BusController>
        [HttpPost("SaveBus")]
        public async Task<IActionResult> Post([FromBody] BusSaveOrUpdateDto busSaveOrUpdateDto)
        {
            OperationResult<BusModel> result = null;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Bus busToInsert = new Bus()
            {
                CapacidadPiso1 = busSaveOrUpdateDto.CapacidadPiso1,
                CapacidadPiso2 = busSaveOrUpdateDto.CapacidadPiso2,
                Disponible = busSaveOrUpdateDto.Disponible,
                FechaCreacion = busSaveOrUpdateDto.FechaCambio,
                Nombre = busSaveOrUpdateDto.Nombre,
                NumeroPlaca = busSaveOrUpdateDto.NumeroPlaca,
                UsuarioModificacion = busSaveOrUpdateDto.UsuarioId
            };

            result = await _busRepository.Save(busToInsert);

            if (!result.Success)
                return BadRequest(result);


            return Ok(result);
        }

        // PUT api/<BusController>/5
        [HttpPut("UpdateBus")]
        public async Task<IActionResult> Put(int id, [FromBody] BusSaveOrUpdateDto busSaveOrUpdateDto)
        {
            OperationResult<BusModel> result = null;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            Bus busToUpdate = new Bus()
            {
                Id = busSaveOrUpdateDto.BusId,
                CapacidadPiso1 = busSaveOrUpdateDto.CapacidadPiso1,
                CapacidadPiso2 = busSaveOrUpdateDto.CapacidadPiso2,
                Disponible = busSaveOrUpdateDto.Disponible,
                Nombre = busSaveOrUpdateDto.Nombre,
                NumeroPlaca = busSaveOrUpdateDto.NumeroPlaca,
                UsuarioModificacion = busSaveOrUpdateDto.UsuarioId,
                FechaModificacion= busSaveOrUpdateDto.FechaCambio
            };

            result = await _busRepository.Update(busToUpdate);

            if (!result.Success)
                return BadRequest(result);


            return Ok(result);
        }

        // DELETE api/<BusController>/5
        [HttpPut("ActiveBus")]
        public async Task<IActionResult> Delete([FromBody] BusDisableOrEnableDto busDisableOrEnableDto)
        {
            OperationResult<BusModel> result = null;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Bus busToUpdate = new Bus()
            {
                Id = busDisableOrEnableDto.BusId,
                UsuarioModificacion = busDisableOrEnableDto.UsuarioId,
                FechaModificacion = busDisableOrEnableDto.FechaCambio,
                Estatus = busDisableOrEnableDto.Status
            };

            result = await _busRepository.Remove(busToUpdate);

            if (!result.Success)
                return BadRequest(result);


            return Ok(result);
        }
    }
}
