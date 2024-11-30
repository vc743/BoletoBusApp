using BoletoBusApp.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoletoBusApp.Data.Entities.Configuration
{
    [Table("Ruta", Schema = "dbo")]
    public sealed class Ruta : AuditEntity<int>
    {
        public Ruta()
        {
            this.Estatus = true;
        }

        [Key]
        [Column("IdRuta")]
        public override int Id { get; set; }
        public string? Origen { get; set; }
        public string? Destino { get; set; }
    }
}
