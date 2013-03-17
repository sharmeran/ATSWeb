using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSCommon.Entites.DateConvertor
{
    public class Year
    {
        int yearID;
        int hijriYearID;
        List<Month> monthList;

        public List<Month> MonthList
        {
            get { return monthList; }
            set { monthList = value; }
        }

        public int HijriYearID
        {
            get { return hijriYearID; }
            set { hijriYearID = value; }
        }

        public int YearID
        {
            get { return yearID; }
            set { yearID = value; }
        }
    }
}
