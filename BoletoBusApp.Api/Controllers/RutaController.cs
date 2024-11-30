using BoletoBusApp.Api.Dtos.Configuration.Bus;
using BoletoBusApp.Api.Dtos.Configuration.Ruta;
using BoletoBusApp.Data.Base;
using BoletoBusApp.Data.Entities.Configuration;
using BoletoBusApp.Data.Interfaces;
using BoletoBusApp.Data.Models;
using BoletoBusApp.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BoletoBusApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RutaController : ControllerBase
    {
        private readonly IRutaRepository _rutaRepository;

        public RutaController(IRutaRepository rutaRepository) 
        {
            _rutaRepository = rutaRepository;
        }

        // GET: api/<RutaController>
        [HttpGet("GetRutas")]
        public async Task<IActionResult> Get()
        {
            OperationResult<List<RutaModel>> result = await _rutaRepository.GetAll();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // GET api/<RutaController>/5
        [HttpGet("GetRutaById")]
        public async Task<IActionResult> Get(int id)
        {
            OperationResult<RutaModel> result = new OperationResult<RutaModel>();

            if (id <= 0)
            {
                result.Success = false;
                result.Message = "El id de la ruta es inválido";
                return BadRequest(result);
            }

            result = await _rutaRepository.GetEntityBy(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // POST api/<RutaController>
        [HttpPost("SaveRuta")]
        public async Task<IActionResult> Post([FromBody] RutaSaveOrUpdateDto rutaSaveOrUpdateDto)
        {
            OperationResult<RutaModel> result = null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Ruta ruta = new Ruta()
            {
                
                Origen = rutaSaveOrUpdateDto.Origen,
                Destino = rutaSaveOrUpdateDto.Destino,
                UsuarioModificacion = rutaSaveOrUpdateDto.UsuarioId,
                FechaCreacion = rutaSaveOrUpdateDto.FechaCambio
            };

            result = await _rutaRepository.Save(ruta);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // PUT api/<RutaController>/5
        [HttpPut("UpdateRuta")]
        public async Task<IActionResult> Put(int id, [FromBody] RutaSaveOrUpdateDto rutaSaveOrUpdateDto)
        {
            OperationResult<RutaModel> result = null;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            Ruta rutaToUpdate = new Ruta()
            {
                Id = rutaSaveOrUpdateDto.IdRuta,
                Origen = rutaSaveOrUpdateDto.Origen,
                Destino = rutaSaveOrUpdateDto.Destino,
                UsuarioModificacion = rutaSaveOrUpdateDto.UsuarioId,
                FechaModificacion= rutaSaveOrUpdateDto.FechaCambio
            };

            result = await _rutaRepository.Update(rutaToUpdate);

            if (!result.Success)
                return BadRequest(result);


            return Ok(result);
        }

        // DELETE api/<RutaController>/5
        [HttpPut("ActiveRuta")]
        public async Task<IActionResult> Delete(RutaDisableOrEnableDto rutaDisableOrEnableDto)
        {
            OperationResult<RutaModel> result = null;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Ruta rutaToUpdate = new Ruta()
            {
                Id = rutaDisableOrEnableDto.RutaId,
                UsuarioModificacion = rutaDisableOrEnableDto.UsuarioId,
                FechaModificacion = rutaDisableOrEnableDto.FechaCambio,
                Estatus = rutaDisableOrEnableDto.Status
            };

            result = await _rutaRepository.Remove(rutaToUpdate);

            if (!result.Success)
                return BadRequest(result);


            return Ok(result);
        }
    }
}
