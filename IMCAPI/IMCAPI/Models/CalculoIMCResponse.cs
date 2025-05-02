using System;

namespace IMCAPI.Models
{
    public class CalculoIMCResponse
    {
        public double Imc { get; set; }
        public string Categoria { get; set; }
        public int Id { get; set; }
        public DateTime FechaCalculo { get; set; }
    }
}
