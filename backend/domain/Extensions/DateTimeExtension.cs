using System;

namespace Domain.Extensions
{
    public static class DateTimeExtension
    {
        public static double DiffYears(this DateTime start, DateTime end, bool excludeTime = false)
        {
            //1 ano tem aproximados (média) 8760 horas
            TimeSpan timeSpanDiff = excludeTime == true ? end.Date - start.Date : end - start;

            double diff = timeSpanDiff.TotalHours / 8760;

            return diff;
        }

        public static double DiffMonths(this DateTime start, DateTime end, bool excludeTime = false)
        {
            //1 mês tem aproximados (média) 720 horas
            TimeSpan timeSpanDiff = excludeTime == true ? end.Date - start.Date : end - start;

            double diff = timeSpanDiff.TotalHours / 720;

            return diff;
        }
    }
}
