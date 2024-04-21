using System.Text.RegularExpressions;

namespace Karma.Application.Extensions
{
    public static class PhoneNumberExtension
    {
        public static string ToDefaultFormat(this string value)
        {
            if (!Regex.IsMatch(value, @"^(\+98|0)?9\d{9}$"))
                return value;

            return value.Replace("+98", "0");
        }
    }
}
