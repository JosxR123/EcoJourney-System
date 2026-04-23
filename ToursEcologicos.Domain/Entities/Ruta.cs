using System.ComponentModel.DataAnnotations;

namespace ToursEcologicos.Domain.Entities
{
    public class Ruta : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string NombreRuta { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Ubicacion { get; set; } = string.Empty;

        public double DistanciaKm { get; set; }

        [Required]
        [StringLength(50)]
        public string Dificultad { get; set; } = string.Empty;
    }
}