using BoletoBusApp.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BoletoBusApp.Data.Entities.Configuration
{
    [Table("Asiento")]
    public sealed class Asiento : AuditEntity<int>
    {
        public Asiento()
        {
            this.Estatus = true;
        }

        [Key]
        [Column("IdAsiento")]
        public override int Id { get; set; }
        public int IdBus { get; set; }
        public int NumeroPiso { get; set; }
        public int NumeroAsiento { get; set; }
    }
}
