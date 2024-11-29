using BoletoBusApp.Data.Base;
using BoletoBusApp.Data.Context;
using BoletoBusApp.Data.Entities.Configuration;
using BoletoBusApp.Data.Interfaces;
using BoletoBusApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Linq.Expressions;

namespace BoletoBusApp.Data.Repositories
{
    public class AsientoRepository : IAsientoRepository
    {
        private readonly BoletoContext _boletoContext;
        private readonly ILogger<AsientoRepository> _logger;

        public AsientoRepository(BoletoContext boletoContext, ILogger<AsientoRepository> logger)
        {
            _boletoContext = boletoContext;
            _logger = logger;
        }

        public async Task<bool> Exists(Expression<Func<Asiento, bool>> filter)
        {
            return await _boletoContext.Asientos.AnyAsync(filter);
        }

        public async Task<OperationResult<List<AsientoBusModel>>> GetAll()
        {
            OperationResult<List<AsientoBusModel>> operationResult = new OperationResult<List<AsientoBusModel>>();

            try
            {
                var asientos = await _boletoContext.Asientos
                                            .Where(cd => cd.Estatus == true)
                                            .Select(cd => new AsientoBusModel()
                                            {
                                                AsientoId = cd.Id,
                                                BusId = cd.IdBus,
                                                NumeroPiso = cd.NumeroPiso,
                                                NumeroAsiento = cd.NumeroAsiento,
                                                FechaCreacion = cd.FechaCreacion,
                                                Bus = _boletoContext.Buses.FirstOrDefault(b => b.Id == cd.IdBus).Nombre
                                            }).ToListAsync();
                operationResult.Result = asientos;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error obteniendo los asientos.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }
        public async Task<OperationResult<List<AsientoBusModel>>> GetAsientosByBus(int idBus)
        {
            OperationResult<List<AsientoBusModel>> result = new OperationResult<List<AsientoBusModel>>();

            try
            {
                var query = await (from asiento in _boletoContext.Asientos
                                   join bus in _boletoContext.Buses on asiento.IdBus equals bus.Id
                                   where asiento.Estatus == true
                                   orderby asiento.FechaCreacion descending
                                   select new AsientoBusModel()
                                   {
                                       AsientoId = asiento.Id,
                                       BusId = bus.Id,
                                       Bus = bus.Nombre,
                                       FechaCreacion = asiento.FechaCreacion,
                                       NumeroAsiento = asiento.NumeroAsiento,
                                       NumeroPiso = asiento.NumeroPiso,
                                       FechaModificacion = asiento.FechaModificacion,
                                       UsuarioModificacion = asiento.UsuarioModificacion
                                   }).ToListAsync();


                result.Result = query;

            }
            catch (Exception ex)
            {

                result.Message = "Error obteniendo los asientos de un autobus.";
                result.Success = false;
                _logger.LogError($"{result.Message}", ex.ToString());
            }

            return result;
        }

        public async Task<OperationResult<List<AsientoBusModel>>> GetAll(Expression<Func<Asiento, bool>> filter)
        {
            OperationResult<List<AsientoBusModel>> operationResult = new OperationResult<List<AsientoBusModel>>();

            try
            {
                var asientos = await _boletoContext.Asientos
                                         .Where(filter)
                                         .Select(cd => new AsientoBusModel()
                                         {
                                             AsientoId = cd.Id,
                                             BusId = cd.IdBus,
                                             NumeroPiso = cd.NumeroPiso,
                                             NumeroAsiento = cd.NumeroAsiento,
                                             FechaCreacion = cd.FechaCreacion,
                                         }).ToListAsync();


                operationResult.Result = asientos;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error obteniendo los asientos.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

        public async Task<OperationResult<AsientoBusModel>> GetEntityBy(int Id)
        {
            OperationResult<AsientoBusModel> operationResult = new OperationResult<AsientoBusModel>();
            try
            {
                if (Id <= 0)
                {
                    operationResult.Success = false;
                    operationResult.Message = "El id del asiento es inválido";
                    return operationResult;
                }


                var asiento = await _boletoContext.Asientos.FindAsync(Id);

                if (asiento is null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "El asiento no se encuentra registrado.";
                    return operationResult;
                }

                operationResult.Result = new AsientoBusModel()
                {
                    AsientoId = asiento.Id,
                    BusId = asiento.IdBus,
                    Bus = _boletoContext.Buses.FirstOrDefault(b => b.Id == asiento.IdBus).Nombre,
                    NumeroPiso = asiento.NumeroPiso,
                    NumeroAsiento = asiento.NumeroAsiento,
                    FechaCreacion = asiento.FechaCreacion,
                };
            }
            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error obteniendo el asiento.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async Task<OperationResult<AsientoBusModel>> Remove(Asiento entity)
        {
            OperationResult<AsientoBusModel> operationResult = new OperationResult<AsientoBusModel>();
            try
            {
                if (entity is null)
                {
                    operationResult.Message = "La entidad asiento no puede ser nula";
                    operationResult.Success = false;
                    return operationResult;
                }

                var asiento = await _boletoContext.Asientos.FindAsync(entity.Id);

                if (asiento is null)
                {
                    operationResult.Message = "El asiento no se encuentra registrado.";
                    operationResult.Success = false;
                    return operationResult;
                }

                asiento.Estatus = entity.Estatus;
                asiento.FechaModificacion = asiento.FechaModificacion;
                asiento.UsuarioModificacion = asiento.UsuarioModificacion;

                _boletoContext.Asientos.Update(asiento);
                await _boletoContext.SaveChangesAsync();

                operationResult.Message = $"El asiento fue desactivado correctamente.";


            }
            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error removiendo el asiento.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async Task<OperationResult<AsientoBusModel>> Save(Asiento entity)
        {
            OperationResult<AsientoBusModel> operationResult = new OperationResult<AsientoBusModel>();
            try
            {
                if (entity is null)
                {
                    operationResult.Message = "La entidad asiento no puede ser nula";
                    operationResult.Success = false;
                    return operationResult;
                }


                if (entity.NumeroAsiento <= 0)
                {
                    operationResult.Message = "El número de asiento no puede ser menor o igual a 0";
                    operationResult.Success = false;
                    return operationResult;
                }

                _boletoContext.Asientos.Add(entity);
                await _boletoContext.SaveChangesAsync();

                operationResult.Message = $"El asiento {entity.NumeroAsiento} fue agregado correctamente.";
            }
            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error guardando el asiento.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async Task<OperationResult<AsientoBusModel>> Update(Asiento entity)
        {
            OperationResult<AsientoBusModel> operationResult = new OperationResult<AsientoBusModel>();

            try
            {
                if (entity is null)
                {
                    operationResult.Message = "La entidad asiento no puede ser nula";
                    operationResult.Success = false;
                    return operationResult;
                }

                var asiento = await _boletoContext.Asientos.FindAsync(entity.Id);

                if (asiento is null)
                {
                    operationResult.Message = "El asiento no se encuentra registrado.";
                    operationResult.Success = false;
                    return operationResult;
                }

                asiento.IdBus = entity.IdBus;
                asiento.NumeroAsiento = entity.NumeroAsiento;
                asiento.NumeroPiso = entity.NumeroPiso;
                asiento.FechaModificacion = entity.FechaModificacion;
                asiento.UsuarioModificacion = entity.UsuarioModificacion;


                _boletoContext.Asientos.Update(asiento);
                await _boletoContext.SaveChangesAsync();

                operationResult.Message = $"El asiento {entity.NumeroAsiento} fue actualizado correctamente.";

                operationResult.Result = new AsientoBusModel()
                {
                    AsientoId = asiento.Id,
                    BusId = asiento.IdBus,
                    NumeroAsiento = asiento.NumeroAsiento,
                    NumeroPiso = asiento.NumeroPiso,
                    FechaCreacion = asiento.FechaModificacion.Value,
                };

            }
            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error actualizando el asiento.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
    }
}
