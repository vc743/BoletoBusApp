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
    public sealed class BusRepository : IBusRepository
    {
        private readonly BoletoContext _boletoContext;
        private readonly ILogger<BusRepository> _logger;

        public BusRepository(BoletoContext boletoContext, ILogger<BusRepository> logger)
        {
            _boletoContext = boletoContext;
            _logger = logger;
        }

        public async Task<bool> Exists(Expression<Func<Bus, bool>> filter)
        {
            return await _boletoContext.Buses.AnyAsync(filter);
        }

        public async Task<OperationResult<List<BusModel>>> GetAll()
        {
            OperationResult<List<BusModel>> operationResult = new OperationResult<List<BusModel>>();

            try
            {
                var buses = await _boletoContext.Buses
                                         .Where(cd => cd.Estatus == true)
                                         .Select(cd => new BusModel()
                                         {
                                             IdBus = cd.Id,
                                             CapacidadPiso1 = cd.CapacidadPiso1,
                                             CapacidadPiso2 = cd.CapacidadPiso2,
                                             Disponible = cd.Disponible,
                                             FechaCreacion = cd.FechaCreacion,
                                             Nombre = cd.Nombre,
                                             NumeroPlaca = cd.NumeroPlaca,
                                         }).ToListAsync();


                operationResult.Result = buses;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error obteniendo los buses.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;

        }

        public async Task<OperationResult<List<BusModel>>> GetAll(Expression<Func<Bus, bool>> filter)
        {
            OperationResult<List<BusModel>> operationResult = new OperationResult<List<BusModel>>();

            try
            {
                var buses = await _boletoContext.Buses
                                         .Where(filter)
                                         .Select(cd => new BusModel()
                                         {
                                             IdBus = cd.Id,
                                             CapacidadPiso1 = cd.CapacidadPiso1,
                                             CapacidadPiso2 = cd.CapacidadPiso2,
                                             Disponible = cd.Disponible,
                                             FechaCreacion = cd.FechaCreacion,
                                             Nombre = cd.Nombre,
                                             NumeroPlaca = cd.NumeroPlaca,
                                         }).ToListAsync();


                operationResult.Result = buses;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error obteniendo los buses.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async Task<OperationResult<BusModel>> GetEntityBy(int Id)
        {
            OperationResult<BusModel> operationResult = new OperationResult<BusModel>();
            try
            {
                if (Id <= 0)
                {
                    operationResult.Success = false;
                    operationResult.Message = "El id del bus es inválido";
                    return operationResult;
                }


                var bus = await _boletoContext.Buses.FindAsync(Id);

                if (bus is null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "El bus no se encuentra registrado.";
                    return operationResult;
                }

                operationResult.Result = new BusModel()
                {
                    CapacidadPiso1 = bus.CapacidadPiso1,
                    CapacidadPiso2 = bus.CapacidadPiso2,
                    Disponible = bus.Disponible,
                    FechaCreacion = bus.FechaCreacion,
                    IdBus = bus.Id,
                    Nombre = bus.Nombre,
                    NumeroPlaca = bus.NumeroPlaca
                };
            }
            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error obteniendo el bus.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async Task<OperationResult<BusModel>> Remove(Bus entity)
        {
            OperationResult<BusModel> operationResult = new OperationResult<BusModel>();
            try
            {
                if (entity is null)
                {
                    operationResult.Message = "La entidad bus no puede ser nula";
                    operationResult.Success = false;
                    return operationResult;
                }

                var bus = await _boletoContext.Buses.FindAsync(entity.Id);

                if (bus is null)
                {
                    operationResult.Message = "El bus no se encuentra registrado.";
                    operationResult.Success = false;
                    return operationResult;
                }

                bus.Estatus = entity.Estatus;
                bus.FechaModificacion = bus.FechaModificacion;
                bus.UsuarioModificacion = bus.UsuarioModificacion;

                _boletoContext.Buses.Update(bus);
                await _boletoContext.SaveChangesAsync();

                operationResult.Message = $"El bus {entity.Nombre} fue desactivado correctamente.";


            }
            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error removiendo el bus.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async Task<OperationResult<BusModel>> Save(Bus entity)
        {
            OperationResult<BusModel> operationResult = new OperationResult<BusModel>();
            try
            {
                if (entity is null)
                {
                    operationResult.Message = "La entidad bus no puede ser nula";
                    operationResult.Success = false;
                    return operationResult;
                }


                if (string.IsNullOrWhiteSpace(entity.NumeroPlaca))
                {
                    operationResult.Message = "El número es requerido.";
                    operationResult.Success = false;
                    return operationResult;
                }

                _boletoContext.Buses.Add(entity);
                await _boletoContext.SaveChangesAsync();

                operationResult.Message = $"El bus {entity.Nombre} fue agregado correctamente.";
            }
            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error guardando el bus.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }

        public async Task<OperationResult<BusModel>> Update(Bus entity)
        {
            OperationResult<BusModel> operationResult = new OperationResult<BusModel>();

            try
            {
                if (entity is null)
                {
                    operationResult.Message = "La entidad bus no puede ser nula";
                    operationResult.Success = false;
                    return operationResult;
                }

                var bus = await _boletoContext.Buses.FindAsync(entity.Id);

                if (bus is null)
                {
                    operationResult.Message = "El bus no se encuentra registrado.";
                    operationResult.Success = false;
                    return operationResult;
                }

                bus.NumeroPlaca = entity.NumeroPlaca;
                bus.CapacidadPiso1 = entity.CapacidadPiso1;
                bus.CapacidadPiso2 = entity.CapacidadPiso2;
                bus.Nombre = entity.Nombre;
                bus.Disponible = entity.Disponible;
                bus.FechaModificacion = entity.FechaModificacion;
                bus.UsuarioModificacion = entity.UsuarioModificacion;


                _boletoContext.Buses.Update(bus);
                await _boletoContext.SaveChangesAsync();

                operationResult.Message = $"El bus {entity.Nombre} fue actualizado correctamente.";

                operationResult.Result = new BusModel()
                {
                    IdBus = bus.Id,
                    CapacidadPiso1 = bus.CapacidadPiso1,
                    CapacidadPiso2 = bus.CapacidadPiso2,
                    Disponible = bus.Disponible,
                    Nombre = bus.Nombre,
                    NumeroPlaca = bus.NumeroPlaca,
                    FechaCreacion = bus.FechaModificacion.Value,
                };

            }
            catch (Exception ex)
            {

                operationResult.Success = false;
                operationResult.Message = "Ocurrió un error actualizando el bus.";
                _logger.LogError(operationResult.Message, ex.ToString());
            }
            return operationResult;
        }
    }
}
