using System.ComponentModel.DataAnnotations;

namespace ToursEcologicos.Domain.Entities
{
    public class Guia : Persona
    {
        [Required]
        [StringLength(100)]
        public string Especialidad { get; set; } = string.Empty;

        public int AniosExperiencia { get; set; }
    }
}