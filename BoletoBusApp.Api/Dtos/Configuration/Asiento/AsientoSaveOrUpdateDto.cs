using System.ComponentModel.DataAnnotations;

namespace BoletoBusApp.Api.Dtos.Configuration.Asiento
{
    public record AsientoSaveOrUpdateDto
    {
        [Required(ErrorMessage = "El Id del asiento es requerido.")]
        public int AsientoId { get; set; }

        [Required(ErrorMessage = "El Id del bus al cual pertenece el asiento es requerido.")]
        public int BusId { get; set; }

        [Required(ErrorMessage = "El número de piso del asiento es requerido.")]
        public int NumeroPiso { get; set; }

        [Required(ErrorMessage = "El numero de asiento es requerido")]
        public int NumeroAsiento { get; set; }

        [Required(ErrorMessage = "El usuario que esta haciendo el registro es requerido.")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "La fecha de creación es requerida.")]
        public DateTime FechaCambio { get; set; }
    }
}
