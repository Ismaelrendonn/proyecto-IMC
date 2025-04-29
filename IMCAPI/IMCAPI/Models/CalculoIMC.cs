using System.ComponentModel.DataAnnotations;

namespace IMCAPI.Models;

public class CalculoIMC
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El peso es obligatorio")]
    [Range(20, 300, ErrorMessage = "El peso debe estar entre 20 y 300 kg")]
    public double Peso { get; set; }

    [Required(ErrorMessage = "La altura es obligatoria")]
    [Range(100, 250, ErrorMessage = "La altura debe estar entre 100 y 250 cm")]
    public double AlturaCm { get; set; } // Usar AlturaCm para consistencia

    public double ResultadoIMC { get; set; }
    public string? Categoria { get; set; }
    public DateTime FechaCalculo { get; set; }
}