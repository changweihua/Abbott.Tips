using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Framework.Util
{
    /// <summary>
    /// 日期辅助类
    /// </summary>
    public sealed class DateUtil
    {
        /// <summary>
        /// 获取两个时间年份跨度
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static double GetYears(string beginDate, string endDate)
        {
            int years = 0;

            var bdate = Convert.ToDateTime(beginDate);
            var edate = Convert.ToDateTime(endDate);

            var totalDays = Math.Abs((bdate - edate).TotalDays);

            var diffYear = Math.Abs(bdate.Year - edate.Year);
            var diffMonth = Math.Abs(bdate.Month - edate.Month);

            //当前两个日期年份等于0
            if (diffYear == 0)
            {
                // 当前跨度天数/总天数
                if (DateTime.IsLeapYear(bdate.Year))
                {
                    return totalDays / 366;
                }
                return totalDays / 365;
            }
            //当前两个日期年份等于1
            else if (diffYear == 1)
            {
                var bYearDays = 365;
                var eYearDays = 365;
                if (DateTime.IsLeapYear(bdate.Year))
                {
                    bYearDays = 366;
                }
                if (DateTime.IsLeapYear(edate.Year))
                {
                    eYearDays = 366;
                }

                var bdays = (new DateTime(bdate.Year, 12, 31) - bdate).TotalDays;
                var edays = (edate - new DateTime(edate.Year, 1, 1)).TotalDays;

                return bdays / bYearDays + edays / eYearDays;
            }
            //当前两个日期年份大于1
            else
            {
                var bYearDays = 365;
                var eYearDays = 365;
                if (DateTime.IsLeapYear(bdate.Year))
                {
                    bYearDays = 366;
                }
                if (DateTime.IsLeapYear(edate.Year))
                {
                    eYearDays = 366;
                }

                var bdays = (new DateTime(bdate.Year, 12, 31) - bdate).TotalDays;
                var edays = (edate - new DateTime(edate.Year, 1, 1)).TotalDays;

                return bdays / bYearDays + edays / eYearDays + (diffYear - 1);
            }
        }
    }
}
