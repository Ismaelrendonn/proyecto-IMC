


namespace IMCAPI.Services;


/*public class CalculadoraIMCService
{
    public (double Valor, string Clasificacion) CalcularIMC(double peso, double altura)
    {
        if (peso <= 0 || altura <= 0)
            throw new ArgumentException("Peso y altura deben ser positivos.");

        double imc = peso / (altura * altura);
        string clasificacion = imc switch
        {
            < 18.5 => "Bajo peso",
            >= 18.5 and < 25 => "Normal",
            >= 25 and < 30 => "Sobrepeso",
            >= 30 and < 35 => "Obesidad grado I",
            >= 35 and < 40 => "Obesidad grado II",
            >= 40 => "Obesidad grado III",
            _ => "No clasificado"
        };

        return (imc, clasificacion);
    }
}*/

// En Services/CalculadoraIMCService.cs


public class CalculadoraIMCService
{
   public (double imc, string categoria) CalcularIMC(double peso, double altura, bool alturaEnMetros = true)
{
    if (peso <= 0 || altura <= 0)
        throw new ArgumentException("Peso y altura deben ser positivos.");

    double alturaM = alturaEnMetros ? altura : altura / 100;
    double imc = peso / (alturaM * alturaM);
    
    string clasificacion = imc switch
    {
        < 18.5 => "Bajo peso",
        >= 18.5 and < 25 => "Normal",
        >= 25 and < 30 => "Sobrepeso",
        >= 30 and < 35 => "Obesidad grado I",
        >= 35 and < 40 => "Obesidad grado II",
        >= 40 => "Obesidad grado III",
        _ => "No clasificado"
    };

    return (imc, clasificacion);
}
}
public interface ICalculadoraIMCService
{
    (double imc, string categoria) CalcularIMC(double peso, double altura);
}

