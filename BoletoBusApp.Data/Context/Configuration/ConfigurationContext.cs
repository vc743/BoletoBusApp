using BoletoBusApp.Data.Entities.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BoletoBusApp.Data.Context
{
    public partial class BoletoContext
    {
        //public DbSet<Status> Statuses { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Asiento> Asientos { get; set; }
    }
}
