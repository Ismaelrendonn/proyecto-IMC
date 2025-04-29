namespace IMCAPI.Services;

public class CalculadoraIMCService
{
    public (double imc, string categoria) CalcularIMC(double peso, double alturaEnMetros) // Espera altura en metros
    {
        if (peso <= 0 || alturaEnMetros <= 0)
            throw new ArgumentException("Peso y altura deben ser positivos.");

        double imc = peso / (alturaEnMetros * alturaEnMetros);

        string categoria = imc switch
        {
            < 18.5 => "Bajo peso",
            >= 18.5 and < 25 => "Normal",
            >= 25 and < 30 => "Sobrepeso",
            >= 30 and < 35 => "Obesidad grado I",
            >= 35 and < 40 => "Obesidad grado II",
            >= 40 => "Obesidad grado III",
            _ => "No clasificado"
        };

        return (imc, categoria);
    }
}
