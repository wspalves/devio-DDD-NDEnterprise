using System.Text.RegularExpressions;

namespace DDDNerdStore.Core.DomainObjects;

public static class Validacoes
{
    public static void ValidarSeIgual(object obj1, object obj2, string mensagem)
    {
        if (obj1.Equals(obj2))
            throw new DomainException(mensagem);
    }

    public static void ValidarSeDiferente(object obj1, object obj2, string mensagem)
    {
        if (!obj1.Equals(obj2))
            throw new DomainException(mensagem);
    }

    public static void ValidarCaracteres(string valor, int maximo, string mensagem)
    {
        if (valor.Trim().Length > maximo)
            throw new DomainException(mensagem);
    }

    public static void ValidarCaracteres(string valor, int minimo, int maximo, string mensagem)
    {
        if (valor.Trim().Length < minimo || valor.Trim().Length > maximo)
            throw new DomainException(mensagem);
    }

    public static void ValidarSeNaoExpressao(string pattern, string valor, string mensagem)
    {
        var regex = new Regex(pattern);

        if (!regex.IsMatch(valor))
            throw new DomainException(mensagem);
    }

    public static void ValidarSeVazio(string valor, string mensagem)
    {
        if (string.IsNullOrEmpty(valor))
            throw new DomainException(mensagem);
    }

    public static void ValidarSeNulo(object obj, string mensagem)
    {
        if (obj == null)
            throw new DomainException(mensagem);
    }

    public static void ValidarMinimoMaximo(float valor, float minimo, float maximo, string mensagem)
    {
        if (valor < minimo || valor > maximo)
            throw new DomainException(mensagem);
    }

    public static void ValidarMinimoMaximo(decimal valor, decimal minimo, decimal maximo, string mensagem)
    {
        if (valor < minimo || valor > maximo)
            throw new DomainException(mensagem);
    }

    public static void ValidarSeMenorIgualMinimo(decimal valor, int minimo, string mensagem)
    {
        if (valor <= minimo)
            throw new DomainException(mensagem);
    }

    public static void ValidarSeFalso(bool valor, string mensagem)
    {
        if (!valor)
            throw new DomainException(mensagem);
    }

    public static void ValidarSeVerdadeiro(bool valor, string mensagem)
    {
        if (valor)
            throw new DomainException(mensagem);
    }
}