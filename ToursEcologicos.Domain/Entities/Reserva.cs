using System.ComponentModel.DataAnnotations;

namespace ToursEcologicos.Domain.Entities
{
    public class Reserva : BaseEntity
    {
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public int ProgramacionTourId { get; set; }
        public ProgramacionTour? ProgramacionTour { get; set; }

        [Range(1, 100)]
        public int CantidadPersonas { get; set; }

        public DateTime FechaReserva { get; set; } = DateTime.Now;

        [Required]
        [StringLength(30)]
        public string Estado { get; set; } = "Activa";
    }
}