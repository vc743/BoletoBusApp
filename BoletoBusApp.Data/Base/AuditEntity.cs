
namespace BoletoBusApp.Data.Base
{
    /// <summary>
    /// Entidad para manejar la auditoria de las entidades
    /// </summary>
    /// <typeparam name="TType">Tipo de dato del pk de la tabla</typeparam>
    public abstract class AuditEntity<TType> : BaseEntity<TType>
    {
        public override TType Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? UsuarioModificacion { get; set; }
        public bool Estatus { get; set; }
    }
}
