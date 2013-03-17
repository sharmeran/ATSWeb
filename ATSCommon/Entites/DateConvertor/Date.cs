using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSCommon.Entites.DateConvertor
{
    public class Date
    {
        string hijriDate;
        string gerDate;
        string dayName;
        int dayID;


        public int DayID
        {
            get { return dayID; }
            set { dayID = value; }
        }

        public string DayName
        {
            get { return dayName; }
            set { dayName = value; }
        }

        public string GerDate
        {
            get { return gerDate; }
            set { gerDate = value; }
        }

        public string HijriDate
        {
            get { return hijriDate; }
            set { hijriDate = value; }
        }
    }
}