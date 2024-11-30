using BoletoBusApp.Data.Base;
using BoletoBusApp.Data.Context;
using BoletoBusApp.Data.Entities.Configuration;
using BoletoBusApp.Data.Interfaces;
using BoletoBusApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BoletoBusApp.Data.Repositories
{
    public sealed class RutaRepository : IRutaRepository
    {
        private readonly BoletoContext _boletoContext;
        private readonly ILogger<RutaRepository> _logger;

        public RutaRepository(BoletoContext boletoContext, ILogger<RutaRepository> logger)
        {
            _boletoContext = boletoContext;
            _logger = logger;
        }

        public async Task<bool> Exists(Expression<Func<Ruta, bool>> filter)
        {
            return await _boletoContext.Rutas.AnyAsync(filter);
        }

        public async Task<OperationResult<List<RutaModel>>> GetAll()
        {
            OperationResult<List<RutaModel>> operationResult = new OperationResult<List<RutaModel>>();

            try
            {
                var rutas = await _boletoContext.Rutas
                                         .Where(cd => cd.Estatus == true)
                                         .Select(cd => new RutaModel()
                                         {
                                             IdRuta = cd.Id,
                                             Origen = cd.Origen,
                                             Destino = cd.Destino,
                                             FechaCreacion = cd.FechaCreacion,
                                         }).ToListAsync();


                operationResult.Result = rutas;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error obteniendo las rutas.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async Task<OperationResult<List<RutaModel>>> GetAll(Expression<Func<Ruta, bool>> filter)
        {
            OperationResult<List<RutaModel>> operationResult = new OperationResult<List<RutaModel>>();

            try
            {
                var rutas = await _boletoContext.Rutas
                                         .Where(filter)
                                         .Select(cd => new RutaModel()
                                         {
                                             IdRuta = cd.Id,
                                             Origen = cd.Origen,
                                             Destino = cd.Destino,
                                             FechaCreacion = cd.FechaCreacion,
                                         }).ToListAsync();


                operationResult.Result = rutas;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error obteniendo las rutas.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async Task<OperationResult<RutaModel>> GetEntityBy(int Id)
        {
            OperationResult<RutaModel> operationResult = new OperationResult<RutaModel>();
            try
            {
                if (Id <= 0)
                {
                    operationResult.Success = false;
                    operationResult.Message = "El id de la ruta es inválido";
                    return operationResult;
                }


                var ruta = await _boletoContext.Rutas.FindAsync(Id);

                if (ruta is null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "La ruta no se encuentra registrada.";
                    return operationResult;
                }

                operationResult.Result = new RutaModel()
                {
                    IdRuta = ruta.Id,
                    Origen = ruta.Origen,
                    Destino = ruta.Destino,
                    FechaCreacion = ruta.FechaCreacion,
                };
            }
            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error obteniendo la ruta.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async Task<OperationResult<RutaModel>> Remove(Ruta entity)
        {
            OperationResult<RutaModel> operationResult = new OperationResult<RutaModel>();
            try
            {
                if (entity is null)
                {
                    operationResult.Message = "La entidad ruta no puede ser nula";
                    operationResult.Success = false;
                    return operationResult;
                }

                var ruta = await _boletoContext.Rutas.FindAsync(entity.Id);

                if (ruta is null)
                {
                    operationResult.Message = "La ruta no se encuentra registrada.";
                    operationResult.Success = false;
                    return operationResult;
                }

                ruta.Estatus = entity.Estatus;
                ruta.FechaModificacion = ruta.FechaModificacion;
                ruta.UsuarioModificacion = ruta.UsuarioModificacion;

                _boletoContext.Rutas.Update(ruta);
                await _boletoContext.SaveChangesAsync();

                operationResult.Message = $"La ruta con origen \"{entity.Origen}\" a \"{entity.Destino}\" fue desactivada correctamente.";


            }
            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error removiendo la ruta.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async Task<OperationResult<RutaModel>> Save(Ruta entity)
        {
            OperationResult<RutaModel> operationResult = new OperationResult<RutaModel>();
            try
            {
                if (entity is null)
                {
                    operationResult.Message = "La entidad ruta no puede ser nula";
                    operationResult.Success = false;
                    return operationResult;
                }


                if (string.IsNullOrWhiteSpace(entity.Destino))
                {
                    operationResult.Message = "El destino de la ruta es requerido.";
                    operationResult.Success = false;
                    return operationResult;
                }

                _boletoContext.Rutas.Add(entity);
                await _boletoContext.SaveChangesAsync();

                operationResult.Message = $"La ruta con origen \"{entity.Origen}\" a \"{entity.Destino}\" fue agregada correctamente.";
            }
            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error guardando la ruta.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async Task<OperationResult<RutaModel>> Update(Ruta entity)
        {
            OperationResult<RutaModel> operationResult = new OperationResult<RutaModel>();

            try
            {
                if (entity is null)
                {
                    operationResult.Message = "La entidad ruta no puede ser nula";
                    operationResult.Success = false;
                    return operationResult;
                }

                var ruta = await _boletoContext.Rutas.FindAsync(entity.Id);

                if (ruta is null)
                {
                    operationResult.Message = "La ruta no se encuentra registrada.";
                    operationResult.Success = false;
                    return operationResult;
                }

                ruta.Origen = entity.Origen;
                ruta.Destino = entity.Destino;
                ruta.FechaModificacion = entity.FechaModificacion;
                ruta.UsuarioModificacion = entity.UsuarioModificacion;


                _boletoContext.Rutas.Update(ruta);
                await _boletoContext.SaveChangesAsync();

                operationResult.Message = $"La ruta con origen \"{entity.Origen}\" a \"{entity.Destino}\" fue actualizada correctamente.";

                operationResult.Result = new RutaModel()
                {
                    IdRuta = ruta.Id,
                    Origen = ruta.Origen,
                    Destino = ruta.Destino,
                    FechaCreacion = ruta.FechaModificacion.Value,
                };

            }
            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error actualizando la ruta.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
    }
}
