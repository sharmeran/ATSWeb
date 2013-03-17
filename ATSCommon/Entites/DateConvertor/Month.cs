using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSCommon.Entites.DateConvertor
{
    public class Month
    {
        string monthName;
        int monthID;
        int monthLength;
        string hijriName;
        int hijriID;
        List<Date> dateList;

        public List<Date> DateList
        {
            get { return dateList; }
            set { dateList = value; }
        }

        public int HijriID
        {
            get { return hijriID; }
            set { hijriID = value; }
        }

        public string HijriName
        {
            get { return hijriName; }
            set { hijriName = value; }
        }

        public int MonthLength
        {
            get { return monthLength; }
            set { monthLength = value; }
        }

        public int MonthID
        {
            get { return monthID; }
            set { monthID = value; }
        }

        public string MonthName
        {
            get { return monthName; }
            set { monthName = value; }
        }
    }
}
