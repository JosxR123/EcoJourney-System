using System.ComponentModel.DataAnnotations;

namespace ToursEcologicos.Domain.Entities
{
    public class Cliente : Persona
    {
        [StringLength(100)]
        public string PreferenciaEcologica { get; set; } = string.Empty;
    }
}