using BoletoBusApp.Data.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BoletoBusApp.Data.Entities.Configuration
{
    [Table("Bus", Schema = "dbo")]
    public sealed class Bus : AuditEntity<int>
    {
        public Bus()
        {
            this.Estatus = true;
        }

        [Key]
        [Column("IdBus")]
        public override int Id { get; set; }
        public string? NumeroPlaca { get; set; }
        public string? Nombre { get; set; }
        public int CapacidadPiso1 { get; set; }
        public int CapacidadPiso2 { get; set; }
        public bool Disponible { get; set; }
    }
}
