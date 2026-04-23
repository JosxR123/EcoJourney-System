using System.ComponentModel.DataAnnotations;

namespace ToursEcologicos.Domain.Entities
{
    public abstract class Persona : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Cedula { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        public virtual string MostrarInformacion()
        {
            return $"{Nombre} - {Cedula}";
        }

        public virtual string MostrarInformacion(bool mostrarContacto)
        {
            if (mostrarContacto)
                return $"{Nombre} - {Cedula} - {Telefono} - {Correo}";

            return $"{Nombre} - {Cedula}";
        }
    }
}