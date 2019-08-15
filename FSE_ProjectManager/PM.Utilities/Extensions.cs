using Newtonsoft.Json;
using System;
using System.Globalization;

namespace PM.Utilities
{
    public static class Extensions
    {
        public static T ToObject<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string ToJson(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static DateTime? YYYYMMDDToDate(this string dateStr)
        {
            if (string.IsNullOrWhiteSpace(dateStr)) return null;
            return DateTime.ParseExact(dateStr, "yyyyMMdd", CultureInfo.InvariantCulture);
        }

        public static string DateToYYYYMMDD(this DateTime? date)
        {
            if (date == null) return string.Empty;

            return ((DateTime)date).ToString("yyyyMMdd");
        }
    }
}