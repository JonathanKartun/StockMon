using System;
using Xamarin.Forms;

namespace StockMon.Helpers.Converters
{
    public class ColorConverter
    {
        //https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.color.fromhex?view=xamarin-forms
        public static string ColorToHexConverter(Color color)
        {
            return color.ToHex();
        }

        public static Color HexColorToColorConverter(string hexColor)
        {
            return Color.FromHex(hexColor);
        }
    }
}
