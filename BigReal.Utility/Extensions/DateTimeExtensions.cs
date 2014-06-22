using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigReal.Utility.Extensions
{
    /// <summary>
    /// 日期扩展
    /// </summary>
    public static class DateTimeExtensions
    {
        public const string FORMAT_yyyy_MM_dd = "yyyy-MM-dd";
        public const string FORMAT_yyyy_MM_dd_HH_mm_ss = "yyyy-MM-dd HH:mm:ss";
        public const string FORMAT_HH_mm_ss = "HH:mm:ss";

        #region 年、月、周、日 开始~结束

        public static DateTime StartOfYear(this DateTime dt)
        {
            return new DateTime(dt.Year, 1, 1, 0, 0, 0);
        }

        public static DateTime EndOfYear(this DateTime dt)
        {
            return dt.AddYears(1).StartOfYear().AddSeconds(-1);
        }

        public static DateTime StartOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1, 0, 0, 0);
        }

        public static DateTime EndOfMonth(this DateTime dt)
        {
            return dt.AddMonths(1).StartOfMonth().AddSeconds(-1);
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startDayOfWeek = DayOfWeek.Monday)
        {
            var start = dt.StartOfDay();

            if (start.DayOfWeek != startDayOfWeek)
            {
                var d = startDayOfWeek - start.DayOfWeek;
                if (startDayOfWeek <= start.DayOfWeek)
                {
                    return start.AddDays(d);
                }
                else
                {
                    return start.AddDays(-7 + d);
                }
            }

            return start;
        }
        public static DateTime EndOfWeek(this DateTime dt, DayOfWeek startDayOfWeek = DayOfWeek.Monday)
        {
            var end = dt.StartOfDay();
            var endDayOfWeek = startDayOfWeek - 1;
            if (endDayOfWeek < 0)
            {
                endDayOfWeek = DayOfWeek.Saturday;
            }

            if (end.DayOfWeek != endDayOfWeek)
            {
                var d = endDayOfWeek - end.DayOfWeek;
                if (endDayOfWeek == end.DayOfWeek)
                {
                    return end.AddDays(6);
                }
                else if (endDayOfWeek < end.DayOfWeek)
                {
                    return end.AddDays(7 - (end.DayOfWeek - endDayOfWeek));
                }
                else
                {
                    return end.AddDays(endDayOfWeek - end.DayOfWeek);
                }
            }

            return end;
        }

        public static DateTime StartOfDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
        }

        public static DateTime EndOfDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
        }
        #endregion

        public static bool IsWeekend(this DateTime dt)
        {
            return (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday);
        }

        public static bool IsWeekDay(this DateTime dt)
        {
            return !dt.IsWeekend();
        }

        public static double MillisecondsSince1970(this DateTime dt)
        {
            return dt.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
        }

        public static DateTime Max(this DateTime dt, DateTime dtToCompare)
        {
            return dt > dtToCompare ? dt : dtToCompare;
        }
    }
}
