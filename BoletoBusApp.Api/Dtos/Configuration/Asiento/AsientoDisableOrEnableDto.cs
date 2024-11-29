using System.ComponentModel.DataAnnotations;

namespace BoletoBusApp.Api.Dtos.Configuration.Asiento
{
    public record AsientoDisableOrEnableDto
    {
        [Required(ErrorMessage = "El Id del asiento es requerido.")]
        public int AsientoId { get; set; }

        [Required(ErrorMessage = "El Estatus es requerido.")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "El usuario que esta haciendo el registro es requerido.")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "La fecha creación es requerida.")]
        public DateTime FechaCambio { get; set; }
    }
}
