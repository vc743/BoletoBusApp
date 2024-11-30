using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace BoletoBusApp.Api.Dtos.Configuration.Ruta
{
    public record RutaSaveOrUpdateDto
    {
        [Required(ErrorMessage = "El Id de la ruta es requerido.")]
        public int IdRuta { get; set; }

        [Required(ErrorMessage = "El origen de la ruta es requerido")]
        [StringLength(50, ErrorMessage = "La longitud del origen es inválida.")]
        public string? Origen { get; set; }

        [Required(ErrorMessage = "El destino de la ruta es requerido")]
        [StringLength(50, ErrorMessage = "La longitud del destino es invalida")]
        public string? Destino { get; set; }

        [Required(ErrorMessage = "El usuario que esta haciendo el registro es requerido.")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "La fecha creación es requerida.")]
        public DateTime FechaCambio { get; set; }
    }
}
