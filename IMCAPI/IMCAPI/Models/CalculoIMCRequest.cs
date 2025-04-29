using System.ComponentModel.DataAnnotations;

namespace IMCAPI.Models;

public record CalculoIMCRequest(
    [Required(ErrorMessage = "El peso es obligatorio")]
    [Range(20, 300, ErrorMessage = "El peso debe estar entre 20 y 300 kg")]
    double Peso,

    [Required(ErrorMessage = "La altura es obligatoria")]
    [Range(100, 250, ErrorMessage = "La altura debe estar entre 100 y 250 cm")]
    double AlturaCm
);
