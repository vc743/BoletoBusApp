﻿
namespace BoletoBusApp.Data.Models
{
    public class BusModel
    {
        public int IdBus { get; set; }
        public string? NumeroPlaca { get; set; }
        public string? Nombre { get; set; }
        public int CapacidadPiso1 { get; set; }
        public int CapacidadPiso2 { get; set; }
        public bool Disponible { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
