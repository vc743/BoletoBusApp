using System.ComponentModel.DataAnnotations;

namespace BoletoBusApp.Api.Dtos.Configuration.Ruta
{
    public record RutaDisableOrEnableDto
    {
        [Required(ErrorMessage = "El Id de la ruta es requerido.")]
        public int RutaId { get; set; }

        [Required(ErrorMessage = "El Estatus es requerido.")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "El usuario que esta haciendo el registro es requerido.")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "La fecha creación es requerida.")]
        public DateTime FechaCambio { get; set; }
    }
}
