using System;
using System.Collections.Generic;
using System.Globalization;

namespace UCOMProject.Extension
{
    public static class DateExtension
    {
        public static string GetWeekName(this DateTime source)
        {
            switch (source.DayOfWeek.ToString())
            {
                case "Monday": return "一";
                case "Tuesday": return "二";
                case "Wednesday": return "三";
                case "Thursday": return "四";
                case "Friday": return "五";
                case "Saturday": return "六";
                case "Sunday": return "日";
                default: return "系統無法判斷";
            }
        }

        /// <summary>
        /// 取得起訖日期的所有月份數
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static int GetDifferentMonths(this DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }

        /// <summary>
        /// 檢查傳入日期是否有再指定區間內
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static bool CheckDateInRange(this DateTime dt, DateTime beginDate, DateTime endDate)
        {
            var endDateEnd = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
            return (dt >= beginDate && dt <= endDateEnd);
        }

        public static string GetHourChinessName(this DateTime source)
        {
            if (source.Hour >= 0 && source.Hour <= 12)
                return "上午";
            else
                return "下午";
        }

        public static string ToFormat(this DateTime? source, string format)
        {
            if (source == null)
                return string.Empty;
            else
                return source.Value.ToString(format);
        }

        public static DateTime GetDateJoinTime(this string source, DateTime? date)
        {
            var time = DateTime.Parse(source);
            return DateTime.Parse(date.Value.ToString("yyyy-MM-dd") + ' ' + time.ToString("HH:mm"));
        }

        public static long ToEpochTime(this DateTime dateTime)
        {
            var date = dateTime.ToUniversalTime();
            var ticks = date.Ticks - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).Ticks;
            var ts = ticks / TimeSpan.TicksPerSecond;
            return ts;
        }

        public static long ToEpochMillisecondsTime(this DateTime dateTime)
        {
            var date = dateTime.ToUniversalTime();
            var ticks = date.Ticks - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).Ticks;
            var ts = ticks / TimeSpan.TicksPerMillisecond;
            return ts;
        }

        public static string ToNomalDateString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy\\-MM\\-dd HH:mm:ss");
        }

        public static DateTime ToDatetime(this string date)
        {
            var addDay = false;

            if (date.Contains(" 24:"))
            {
                addDay = true;
                date = date.Replace("24:", "00:");
            }

            if (date.Contains("2400"))
            {
                addDay = true;
                date = date.Replace("24", "00");
            }

            List<string> dateTimeString = new List<string>()
            {
                "yyyyMMdd HHmm",
                "yyyyMMdd",
                "yyyy/M/d",
                "yyyy/M/dd",
                "yyyy-MM-dd",
                "yyyy/MM/dd",
                "yyyy-MM-dd HHmm",
                "yyyy-MM-dd HH:mm",
                "yyyy/MM/dd HH:mm",
                "yyyy/MM/dd HH:mm:ss",
                "yyyy-MM-dd HH:mm:ss",
                "yyyy年M月d日 上午 h:mm:ss",
                "yyyy年M月d日 下午 h:mm:ss",
                "yyyy年M月d日 上午 hh:mm:ss",
                "yyyy年M月d日 下午 hh:mm:ss",
                "yyyy/M/d/ 上午 h:mm:ss",
                "yyyy/M/d/ 下午 h:mm:ss",
                "yyyy/M/dd/ 上午 h:mm:ss",
                "yyyy/M/dd/ 下午 h:mm:ss",
                "yyyy/M/d/ 上午 hh:mm:ss",
                "yyyy/M/d/ 下午 hh:mm:ss",
                "yyyy/M/dd/ 上午 hh:mm:ss",
                "yyyy/M/dd/ 下午 hh:mm:ss",
                "yyyy/M/d 下午 h:mm:ss",
                "yyyy/M/d 上午 h:mm:ss",
                "yyyy/MM/dd/ 上午 hh:mm:ss",
                "yyyy/MM/dd/ 下午 hh:mm:ss",
                "yyyy年M月d日 tt hh:mm:ss",
                "yyyy/M/dd 上午 hh:mm:ss",
                "yyyy/M/dd 下午 hh:mm:ss",
                "yyyy/M/d 上午 hh:mm:ss",
                "yyyy/M/d 下午 hh:mm:ss",
                "yyyy-MM-ddTHH:mm:ss",
                "yyyy-MM-ddTHHmmss",
                "yyyy/MM/ddTHH:mm:ss",
                "yyyy/MM/ddTHHmmss",
            };
            DateTime d = new DateTime();
            foreach (var str in dateTimeString)
            {
                bool valid = DateTime.TryParseExact(date, str,
                                           CultureInfo.InvariantCulture,
                                           DateTimeStyles.None,
                                           out d);

                // 转成UTC时间   2022-04-13T11:05:33
                //DateTime formatStartTime = DateTime.Parse(date, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);

                if (valid)
                {
                    if (addDay)
                        d = d.AddDays(1);
                    return d;
                }
            }

            try
            {
                d = DateTime.Parse(date, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
                return d;
            }
            catch
            {
            }

            throw new FormatException("Convert To Datetime Has Some Error.");
        }
    }
}