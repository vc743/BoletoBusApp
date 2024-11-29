using BoletoBusApp.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BoletoBusApp.Data.Entities.Configuration
{
    [Table("Status")]
    public sealed class Status : BaseEntity<short>
    {
        [Key]
        [Column("StatusId")]
        public override short Id { get; set; }
        public string? Description { get; set; }
    }
}
