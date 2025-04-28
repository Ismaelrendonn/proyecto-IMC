/*namespace IMCAPI.Models
{
    public class CalculoIMC
    {
        public int Id { get; set; }
        public double Peso { get; set; }  // en kilogramos
        public double Altura { get; set; } // en centímetros
        public double ResultadoIMC { get; set; }
        public string Categoria { get; set; }
        public DateTime FechaCalculo { get; set; } = DateTime.Now;
    }
}*/

// En Models/CalculoIMC.cs
namespace IMCAPI.Models;
using System.ComponentModel.DataAnnotations;

public class CalculoIMC
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El peso es obligatorio")]
    [Range(20, 300, ErrorMessage = "El peso debe estar entre 20 y 300 kg")]
    public double Peso { get; set; }

    [Required(ErrorMessage = "La altura es obligatoria")]
    [Range(100, 250, ErrorMessage = "La altura debe estar entre 100 y 250 cm")]
    public double Altura { get; set; }

    public double ResultadoIMC { get; set; }
    public string? Categoria { get; set; }
    public DateTime FechaCalculo { get; set; } = DateTime.Now;
}
/*namespace IMCAPI.Models;
using System.ComponentModel.DataAnnotations;

public class CalculoIMC
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El peso es obligatorio")]
    [Range(20, 300, ErrorMessage = "El peso debe ser entre {1} y {2} kg")]
    public double Peso { get; set; }

    [Required(ErrorMessage = "La altura es obligatoria")]
    [Range(0.5, 2.5, ErrorMessage = "La altura debe ser entre {1} y {2} metros")]
    public double Altura { get; set; }

    public double ResultadoIMC { get; set; }
    public string? Categoria { get; set; }
    public DateTime FechaCalculo { get; set; } = DateTime.UtcNow;
}*/