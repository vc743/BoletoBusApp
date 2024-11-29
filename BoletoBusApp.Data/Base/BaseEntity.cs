
namespace BoletoBusApp.Data.Base
{
    /// <summary>
    /// Entidad base que deben heradar todas las entidades
    /// </summary>
    /// <typeparam name="TType">tipo de dato del pk de la tabla</typeparam>
    public abstract class BaseEntity<TType>
    {
        public abstract TType Id { get; set; }
    }
}
