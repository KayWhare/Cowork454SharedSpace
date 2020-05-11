using System;
using System.Collections.Generic;

namespace CoWork454.Common
{
    public class DatePickerHelper
    {
        //Set the Start Time Value
        public static DateTime start = DateTime.ParseExact("07:00", "HH:mm", null);
        //Set the End Time Value
        public static  DateTime end = DateTime.ParseExact("15:00", "HH:mm", null);
        //Set the interval time 
        public static int interval = 15;

        public static List<string> GenerateIntervals()
        {
            //List to hold the values of intervals
            List<string> lstTimeIntervals = new List<string>();
            //Populate the list with 30 minutes interval values
            for (DateTime i = start; i <= end; i = i.AddMinutes(interval))
                lstTimeIntervals.Add(i.ToString("HH:mm tt"));

            return lstTimeIntervals;
        }

        //https://forums.asp.net/t/1879928.aspx?Drop+down+list+to+display+hours+and+minutes
        public List<DateTime> GetTimeIntervals(DateTime start, DateTime end, TimeSpan interval)
        {
            //Your list of intervals
            List<DateTime> intervals = new List<DateTime>();

            //Rounds your start time to the nearest Interval
            start = RoundUp(start, interval);

            //Begin incrementing until you have reached your end Date
            while (start <= end)
            {
                intervals.Add(start);
                start = start.Add(interval);
            }
            //Returns your Intervals
            return intervals;
        }

        //Useful function to round a DateTime to the nearest specified interval
        DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime(((dt.Ticks + d.Ticks - 1) / d.Ticks) * d.Ticks);
        }
    }
}
