//using System;
//namespace StockMon.Models.ViewModels.Converters
//{
//    public class CurrencyValueConverter
//    {
//        public CurrencyValueConverter()
//        {
//        }
//    }
//}


using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace StockMon.Models.ViewModels.Converters
{
    public class CurrencyValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Contains(null)){ return ""; }

            string symbol = (string)values[0];
            double currencyValue = (double)values[1];

            string combinedData = $"{symbol}{currencyValue.ToString("0.000#######")}";
            return combinedData;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}