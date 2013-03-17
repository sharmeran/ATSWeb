using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Entites.DateConvertor;
using Oracle.DataAccess.Client;

namespace ATSDomain.Domains.DateConvert
{
    public static class DateConvertor
    {
        static List<HejriCommonDate> list = new List<HejriCommonDate>();
        private static void GetCommonData()
        {
            List<HejriCommonDate> hejriCommonDateList = new List<HejriCommonDate>();
            OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            OracleCommand com = new OracleCommand("VW_HEJRA_DATES_FIND_ALL", conn);
            try
            {
                conn.Open();
                com.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter parameter = new OracleParameter();
                parameter.Direction = System.Data.ParameterDirection.InputOutput;
                parameter.OracleDbType = OracleDbType.RefCursor;
                com.Parameters.Add(parameter);
                OracleDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    HejriCommonDate hejriCommonDate = new HejriCommonDate();
                    hejriCommonDate = HejriCommonDateHelper(reader);
                    if (hejriCommonDate != null)
                        hejriCommonDateList.Add(hejriCommonDate);
                }


            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
                conn.Clone();
                com.Clone();
            }

            list = hejriCommonDateList;
        }


        private static HejriCommonDate HejriCommonDateHelper(OracleDataReader reader)
        {
            HejriCommonDate hejriCommonDate = new HejriCommonDate();
            hejriCommonDate.GregorianDate = Convert.ToDateTime(reader["GREGORIAN_DATE"]);
            hejriCommonDate.HijriDay = Convert.ToInt32(reader["HEJRA_DD"]);
            hejriCommonDate.HijriMonth = Convert.ToInt32(reader["HEJRA_MM"]);
            hejriCommonDate.HijriYear = Convert.ToInt32(reader["HEJRA_YYYY"]);
            return hejriCommonDate;
        }
        public static string FromGerToHijri(string date)
        {
            GetCommonData();
            string[] dateArr = date.Split(' ');

            if (dateArr.Count() == 2)
            {
                date = dateArr[0];

            }
            string[] dateSplit = date.Split('/');


            DateTime dt = new DateTime(Convert.ToInt32(dateSplit[2]), Convert.ToInt32(dateSplit[1]), Convert.ToInt32(dateSplit[0]));
            var x = from z in list where z.GregorianDate.Date >= dt.Date select z;
            List<HejriCommonDate> dtList = x.ToList<HejriCommonDate>();
            if (dtList != null)
            {
                DateTime dateTime = dtList[dtList.Count - 1].GregorianDate;
                if (dt.Date == dateTime.Date)
                {
                    if (dateArr.Count() == 2)
                    {
                        return dtList[dtList.Count - 1].HijriDay + "/" + dtList[dtList.Count - 1].HijriMonth + "/" + dtList[dtList.Count - 1].HijriYear + " " + dateArr[1];
                    }
                    else
                    {
                        return dtList[dtList.Count - 1].HijriDay + "/" + dtList[dtList.Count - 1].HijriMonth + "/" + dtList[dtList.Count - 1].HijriYear;
                    }
                }
                else
                {
                    if (dateArr.Count() == 2)
                    {
                        TimeSpan timeSpan = dateTime.Date - dt.Date;
                        return dtList[dtList.Count - 1].HijriDay - timeSpan.Days + "/" + dtList[dtList.Count - 1].HijriMonth + "/" + dtList[dtList.Count - 1].HijriYear + " " + dateArr[1];
                    }
                    else
                    {
                        TimeSpan timeSpan = dateTime.Date - dt.Date;
                        return dtList[dtList.Count - 1].HijriDay - timeSpan.Days + "/" + dtList[dtList.Count - 1].HijriMonth + "/" + dtList[dtList.Count - 1].HijriYear;
                    }
                }
            }
            else
            {
                // dtList = 
                return "";
            }

        }

        public static string FromHijriToGer(string date)
        {
            GetCommonData();
            string[] dateArrr;
            string[] timeArray = date.Split(' ');
            if (timeArray.Count() == 2)
            {
                dateArrr = timeArray[0].Split('/');
            }
            else
            {
                dateArrr = date.Split('/');
            }



            if (dateArrr.Count() == 3)
            {

                var x = from z in list where z.HijriDay >= Convert.ToInt32(dateArrr[0]) && z.HijriMonth >= Convert.ToInt32(dateArrr[1]) && z.HijriYear >= Convert.ToInt32(dateArrr[2]) select z;
                List<HejriCommonDate> dtList = x.ToList<HejriCommonDate>();
                dtList = x.ToList<HejriCommonDate>();
                if (dtList != null)
                {
                    HejriCommonDate hejriCommonDate = new HejriCommonDate();
                    hejriCommonDate = dtList[dtList.Count - 1];
                    int result = Convert.ToInt32(dateArrr[0]) - hejriCommonDate.HijriDay;
                    if (timeArray.Count() == 2)
                    {
                        return hejriCommonDate.GregorianDate.AddDays(result).Date.ToString("d/M/yyyy") + " " + timeArray[1];
                    }
                    else
                    {
                        return hejriCommonDate.GregorianDate.AddDays(result).Date.ToString("d/M/yyyy");
                    }
                }
                else
                {
                    return "لاتوجد معلومات بالبحث المطلوب";
                }
            }
            else
            {
                return "حدث خطأ في تحويل التاريخ الرجاء التأكد من صيغة التاريخ";
            }

        }

        public static Cal PrepareCalender()
        {
            GetCommonData();
            Cal calender = new Cal();
            Year year = new Year();
            year.MonthList = new List<Month>();
            Month month = new Month();
            Date date = new Date();
            calender.YearList = new List<Year>();
            int yearID = 0;
            List<Month> monthList = new List<Month>();
            for (int i = 0; i < list.Count; i++)
            {

                DateTime dt = list[i].GregorianDate;
                month = new Month();
                month.DateList = new List<Date>();
                month.HijriID = list[i].HijriMonth;
                month.HijriName = GetHijriMonthName(list[i].HijriMonth);
                month.MonthID = dt.Month;
                month.MonthLength = list[i].HijriDay;
                month.MonthName = GetMonthName(dt.Month);


                if (i == 0)
                {
                    yearID = list[i].HijriYear;
                    year.HijriYearID = list[i].HijriYear;
                    year.YearID = dt.Year;

                }
                else
                {
                    if (yearID != list[i].HijriYear)
                    {

                        calender.YearList.Add(year);
                        yearID = list[i].HijriYear;
                        year = new Year();
                        year.MonthList = new List<Month>();

                        month = new Month();
                        month.DateList = new List<Date>();

                        dt = list[i].GregorianDate;
                        month.DateList = new List<Date>();
                        month.HijriID = list[i].HijriMonth;
                        month.HijriName = GetHijriMonthName(list[i].HijriMonth);
                        month.MonthID = dt.Month;
                        month.MonthLength = list[i].HijriDay;
                        month.MonthName = GetMonthName(dt.Month);
                        year.HijriYearID = list[i].HijriYear;
                        year.YearID = dt.Year;
                    }

                }

                int count = 0;
                for (int j = list[i].HijriDay; j > 0; j--)
                {
                    if (count == 0)
                    {

                        date.DayID = Convert.ToInt32(dt.DayOfWeek);
                        date.DayName = GenDayName(date.DayID);
                        date.GerDate = dt.ToString("dd/M/yyyy");
                        date.HijriDate = list[i].HijriDay + "/" + list[i].HijriMonth + "/" + list[i].HijriYear;
                        month.DateList.Add(date);
                        count++;
                    }
                    else
                    {
                        date = new Date();
                        dt = dt.AddDays(-1);
                        date.DayID = Convert.ToInt32(dt.DayOfWeek);
                        date.DayName = GenDayName(date.DayID);
                        date.GerDate = dt.ToString("dd/M/yyyy");
                        date.HijriDate = list[i].HijriDay - count + "/" + list[i].HijriMonth + "/" + yearID;
                        month.DateList.Add(date);
                        count++;
                    }
                }
                year.MonthList.Add(month);
            }
            return calender;
        }

        static string GetMonthName(int monthID)
        {
            string monthName = "";
            switch (monthID)
            {
                case 1:
                    monthName = "كانون الثاني ";
                    break;
                case 2:
                    monthName = "شباط";
                    break;
                case 3:
                    monthName = "اذار";
                    break;
                case 4:
                    monthName = "نيسان";
                    break;
                case 5:
                    monthName = "ايار";
                    break;
                case 6:
                    monthName = "حزيران";
                    break;
                case 7:
                    monthName = "تموز";
                    break;
                case 8:
                    monthName = "اب";
                    break;
                case 9:
                    monthName = "ايلول";
                    break;
                case 10:
                    monthName = "تشرين الاول";
                    break;
                case 11:
                    monthName = "تشرين الثاني";
                    break;
                case 12:
                    monthName = "كانون اول";
                    break;

            }
            return monthName;
        }

        static string GetHijriMonthName(int monthID)
        {
            string monthName = "";
            switch (monthID)
            {
                case 1:
                    monthName = "محرم";
                    break;
                case 2:
                    monthName = "صفر";
                    break;
                case 3:
                    monthName = "ربيع الاول";
                    break;
                case 4:
                    monthName = "ربيع الثاني";
                    break;
                case 5:
                    monthName = "جمادى الاولى";
                    break;
                case 6:
                    monthName = "جمادى الاخرة";
                    break;
                case 7:
                    monthName = "رجب";
                    break;
                case 8:
                    monthName = "شعبان";
                    break;
                case 9:
                    monthName = "رمضان";
                    break;
                case 10:
                    monthName = "شوال";
                    break;
                case 11:
                    monthName = "ذو القعدة";
                    break;
                case 12:
                    monthName = "ذو الحجة";
                    break;

            }
            return monthName;
        }

        static string GenDayName(int dayID)
        {
            string dayName = "";
            switch (dayID)
            {
                case 0:
                    dayName = "الاحد";
                    break;
                case 1:
                    dayName = "الاثنين";
                    break;
                case 2:
                    dayName = "الثلاثاء";
                    break;
                case 3:
                    dayName = "الاربعاء";
                    break;
                case 4:
                    dayName = "الخميس";
                    break;
                case 5:
                    dayName = "الجمعة";
                    break;
                case 6:
                    dayName = "السبت";
                    break;

            }
            return dayName;
        }

      
    }
}

