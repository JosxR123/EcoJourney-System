using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToursEcologicos.Domain.Entities
{
    public class Tour : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(300)]
        public string Descripcion { get; set; } = string.Empty;

        public int DuracionHoras { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        public int CapacidadMaxima { get; set; }

        public int RutaId { get; set; }
        public Ruta? Ruta { get; set; }
    }
}