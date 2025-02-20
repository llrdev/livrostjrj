using System;

public class NumeroExtenso
{
    private static readonly string[] unidades = { "", "um", "dois", "três", "quatro", "cinco", "seis", "sete", "oito", "nove" };
    private static readonly string[] especiais = { "dez", "onze", "doze", "treze", "catorze", "quinze", "dezesseis", "dezessete", "dezoito", "dezenove" };
    private static readonly string[] dezenas = { "", "dez", "vinte", "trinta", "quarenta", "cinquenta", "sessenta", "setenta", "oitenta", "noventa" };
    private static readonly string[] centenas = { "", "cem", "duzentos", "trezentos", "quatrocentos", "quinhentos", "seiscentos", "setecentos", "oitocentos", "novecentos" };

    public static string NumeroPorExtenso(decimal valor)
    {
        if (valor == 0) return "zero reais";

        string valorPorExtenso = "";

        long parteInteira = (long)Math.Floor(valor);
        int centavos = (int)((valor - parteInteira) * 100);

        valorPorExtenso = NumeroInteiroPorExtenso(parteInteira) + " reais";

        if (centavos > 0)
        {
            valorPorExtenso += " e " + NumeroInteiroPorExtenso(centavos) + " centavos";
        }

        return valorPorExtenso;
    }

    private static string NumeroInteiroPorExtenso(long numero)
    {
        if (numero < 10)
        {
            return unidades[numero];
        }

        else if (numero < 20)
        {
            return especiais[numero - 10];
        }

        else if (numero < 100)
        {
            return dezenas[(int)(numero / 10)] + (numero % 10 > 0 ? " e " + unidades[numero % 10] : "");
        }
        else if (numero < 1000)
        {
            if (numero == 100)
                return "cem"; // Exceção especial para "cem"
            return centenas[(int)(numero / 100)] + (numero % 100 > 0 ? " e " + NumeroInteiroPorExtenso(numero % 100) : "");
        }
        else if (numero < 1000000) // Até 999.999
        {
            return NumeroMilharesPorExtenso(numero, 1000, "mil");
        }
        else if (numero < 1000000000) // Até 999.999.999
        {
            return NumeroMilharesPorExtenso(numero, 1000000, "milhão", "milhões");
        }
        else if (numero < 1000000000000) // Até 999.999.999.999
        {
            return NumeroMilharesPorExtenso(numero, 1000000000, "bilhão", "bilhões");
        }
        else
        {
            return NumeroMilharesPorExtenso(numero, 1000000000000, "trilhão", "trilhões");
        }
    }

    private static string NumeroMilharesPorExtenso(long numero, long divisor, string singular, string plural = null)
    {
        long quociente = numero / divisor;
        long resto = numero % divisor;

        string prefixo = NumeroInteiroPorExtenso(quociente) + " " + (quociente > 1 && plural != null ? plural : singular);
        string sufixo = resto > 0 ? " e " + NumeroInteiroPorExtenso(resto) : "";

        return prefixo + sufixo;
    }
}