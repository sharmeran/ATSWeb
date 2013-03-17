using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSCommon.Entites.DateConvertor
{
    public class HejriCommonDate
    {
        int hijriDay;
        int hijriMonth;
        int hijriYear;
        DateTime gregorianDate;

        public DateTime GregorianDate
        {
            get { return gregorianDate; }
            set { gregorianDate = value; }
        }

        public int HijriYear
        {
            get { return hijriYear; }
            set { hijriYear = value; }
        }

        public int HijriMonth
        {
            get { return hijriMonth; }
            set { hijriMonth = value; }
        }

        public int HijriDay
        {
            get { return hijriDay; }
            set { hijriDay = value; }
        }

    }
}
