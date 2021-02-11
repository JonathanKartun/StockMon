using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace StockMon.Helpers.Extensions
{
    static class EnumExtensions
    {
        public static string GetDescriptionString(this Enum enumVal)
        {
            FieldInfo fieldInfo = enumVal.GetType().GetField(enumVal.ToString());
            if (fieldInfo == null) return null;
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }
    }
}
