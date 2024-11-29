using BoletoBusApp.Api.Dtos.Configuration.Asiento;
using BoletoBusApp.Api.Dtos.Configuration.Bus;
using BoletoBusApp.Data.Base;
using BoletoBusApp.Data.Entities.Configuration;
using BoletoBusApp.Data.Interfaces;
using BoletoBusApp.Data.Models;
using BoletoBusApp.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BoletoBusApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsientoController : ControllerBase
    {
        private readonly IAsientoRepository _asientoRepository;

        public AsientoController(IAsientoRepository asientoRepository)
        {
            _asientoRepository = asientoRepository;
        }

        
        [HttpGet("GetAsientos")]
        public async Task<IActionResult> Get()
        {
            OperationResult<List<AsientoBusModel>> result = await _asientoRepository.GetAll();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        
        [HttpGet("GetAsientoById")]
        public async Task<IActionResult> Get(int id)
        {
            OperationResult<AsientoBusModel> result = new OperationResult<AsientoBusModel>();

            if (id <= 0)
            {
                result.Success = false;
                result.Message = "El id del asiento es inválido";
                return BadRequest(result);
            }

            result = await _asientoRepository.GetEntityBy(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        
        [HttpPost("SaveAsiento")]
        public async Task<IActionResult> Post([FromBody] AsientoSaveOrUpdateDto asientoSaveOrUpdateDto)
        {
            OperationResult<AsientoBusModel> result = null;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Asiento asientoToInsert = new Asiento()
            {
                IdBus = asientoSaveOrUpdateDto.BusId,
                NumeroPiso = asientoSaveOrUpdateDto.NumeroPiso,
                NumeroAsiento = asientoSaveOrUpdateDto.NumeroAsiento,
                FechaCreacion = asientoSaveOrUpdateDto.FechaCambio,
                UsuarioModificacion = asientoSaveOrUpdateDto.UsuarioId
            };

            result = await _asientoRepository.Save(asientoToInsert);

            if (!result.Success)
                return BadRequest(result);


            return Ok(result);
        }

        
        [HttpPut("UpdateAsiento")]
        public async Task<IActionResult> Put(int id, [FromBody] AsientoSaveOrUpdateDto asientoSaveOrUpdateDto)
        {
            OperationResult<AsientoBusModel> result = null;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Asiento asientoToUpdate = new Asiento()
            {
                Id = asientoSaveOrUpdateDto.AsientoId,
                IdBus = asientoSaveOrUpdateDto.BusId,
                NumeroPiso = asientoSaveOrUpdateDto.NumeroPiso,
                NumeroAsiento = asientoSaveOrUpdateDto.NumeroAsiento,
                FechaModificacion = asientoSaveOrUpdateDto.FechaCambio,
                UsuarioModificacion = asientoSaveOrUpdateDto.UsuarioId
            };

            result = await _asientoRepository.Update(asientoToUpdate);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

       
        [HttpPut("ActiveAsiento")]
        public async Task<IActionResult> Delete([FromBody] AsientoDisableOrEnableDto asientoDisableOrEnable)
        {
            OperationResult<AsientoBusModel> result = null;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Asiento asientoToUpdate = new Asiento()
            {
                Id = asientoDisableOrEnable.AsientoId,
                UsuarioModificacion = asientoDisableOrEnable.UsuarioId,
                FechaModificacion = asientoDisableOrEnable.FechaCambio,
                Estatus = asientoDisableOrEnable.Status
            };

            result = await _asientoRepository.Remove(asientoToUpdate);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
