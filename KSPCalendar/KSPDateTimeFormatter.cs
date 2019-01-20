using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using KSPPluginFramework;

namespace KSPCalendar
{
    public class KSPDateTimeFormatter
    {
        double dayLen = Planetarium.fetch.Home.solarDayLength;
        const int hoursInDay = 6;
        const float daysInYear = 426.08f;
        const float monthsInYear = 66.23f;
        double yearLen, monthLen;

        double lastTimeChecked = -1;
        string lastDateTimeReturned = "";
        KSPDateTime dt;
        double secKerbinInitial;
            
        //List<KSPMonth> Months;

        public string getDateTimeStr(string format)
        {
            // This uses the class from TriggerAu, as copied from the TransferWindowPlanner
            
            if (lastTimeChecked < 0)
            {
                if (KSPDateStructure.CalendarType == CalendarTypeEnum.KSPStock)
                {
                    // If  we are using the stock calendar, we need to initialize the 
                    // Months list, since the class doesn't do that
                    //
                    yearLen = dayLen * daysInYear;
                    monthLen = yearLen / monthsInYear / dayLen;

                    List<KSPMonth> Months = new List<KSPMonth>();
                    double x = 0;

                    for (int i = 1; i < monthsInYear + 1; i++)
                    {
                        x += monthLen;
                        //if (x > 7) x -= 7f;
                        KSPMonth m1 = new KSPMonth(i.ToString(), (int)x);
                        Months.Add(m1);
                        Debug.Log("month list, i: " + i + ", x: " + ((int)x).ToString());
                    }
                    KSPDateStructure.Months = Months;
                }

                
            }
            if (Math.Abs(Planetarium.GetUniversalTime() - lastTimeChecked) < 1)
                return lastDateTimeReturned;
            secKerbinInitial = Calendar.Instance.dtKerbinInitial.Year * yearLen;
            dt = new KSPDateTime(Planetarium.GetUniversalTime() + secKerbinInitial);
            //KSPDateStructure.Months = Months;
            lastTimeChecked = Planetarium.GetUniversalTime();
            lastDateTimeReturned = dt.ToString(format);
            return lastDateTimeReturned;

        }
    }
}
