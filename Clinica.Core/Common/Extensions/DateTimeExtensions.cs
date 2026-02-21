namespace Clinica.Core.Common.Extensions;

public static class DateTimeExtensions
{
    public static DateTime ObterPrimeiroDia(this DateTime data, int? ano = null, int? mes = null)
    {
        return new DateTime(ano ?? data.Year, mes ?? data.Month, 1);
    }

    public static DateTime ObterUltimoDia(this DateTime data, int? ano = null, int? mes = null)
    {
        var novaData = new DateTime(ano ?? data.Year, mes ?? data.Month, 1);
        return novaData.AddMonths(1).AddDays(-1);
    }
}
