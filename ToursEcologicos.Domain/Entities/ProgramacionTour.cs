using System.ComponentModel.DataAnnotations;

namespace ToursEcologicos.Domain.Entities
{
    public class ProgramacionTour : BaseEntity
    {
        public int TourId { get; set; }
        public Tour? Tour { get; set; }

        public int GuiaId { get; set; }
        public Guia? Guia { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public TimeSpan HoraSalida { get; set; }

        public int CuposDisponibles { get; set; }
    }
}