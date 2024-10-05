using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Application.Helpers
{
    public class PersianMonthHelper
    {
        public static string GetMonth(int month)
        {
            if (month > 12 || month < 1)
                throw new Exception("Invalid Range");

            return month switch
            {
                1 => "فروردین",
                2 => "اردیبهشت",
                3 => "خرداد",
                4 => "تیر",
                5 => "مرداد",
                6 => "شهریور",
                7 => "مهر",
                8 => "آبان",
                9 => "آذر",
                10 => "دی",
                11 => "بهمن",
                12 => "اسفند",
                _ => throw new Exception("Invalid Range")
            };

        }
    }
}
