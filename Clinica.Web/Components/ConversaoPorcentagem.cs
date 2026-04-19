using MudBlazor;
using System.Globalization;
using System.Numerics;

namespace Clinica.Web.Components;

public class ConversaoPorcentagem<T> : DefaultConverter<T>
{
    protected override T ConvertFromString(string value)
    {
        bool isPercent = Format != null && Format.StartsWith("P", StringComparison.OrdinalIgnoreCase);
        if (isPercent)
            value = value.Replace(Culture.NumberFormat.PercentSymbol, string.Empty);

        var parsedValue = base.ConvertFromString(value);

        if (isPercent)
        {
            if (parsedValue is decimal decimalValue)
            {
                return (T)(object)(decimalValue / 100M);
            }
            else if (parsedValue is double doubleValue)
            {
                return (T)(object)(doubleValue / 100D);
            }
            else if (parsedValue is float floatValue)
            {
                return (T)(object)(floatValue / 100F);
            }
        }

        return parsedValue;
    }
}