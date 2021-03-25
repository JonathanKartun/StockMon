using System.Globalization;
using System.Linq;

namespace StockMon.Helpers.Converters
{
    public class CurrencySymbolConverter
    {
        /// <summary>
        /// Converts a Currency Code to it's corresponding Symbol
        /// eg)
        /// USD => $, EUR => €
        /// </summary>
        public static string CurrencyCodeToSymbol(string CurrencyCode)
        {
            string Symbol = "$";
            var CurrencySymbol = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Where(culture => new RegionInfo(culture.LCID).ISOCurrencySymbol == CurrencyCode).Select(xxx => new RegionInfo(xxx.LCID)).DefaultIfEmpty(null).FirstOrDefault();
            if (CurrencySymbol != null)
            {
                Symbol = CurrencySymbol.CurrencySymbol;
            }
            return Symbol;
        }
    }
}
