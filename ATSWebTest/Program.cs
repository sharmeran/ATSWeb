using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon;
using ATSCommon.Entites.HandPunch;
using ATSCommon.Entites.Shifts;
using ATSCommon.Entites.UserLogs;
using ATSDataAccess.SQLImlementation.HandPunch;
using ATSDataAccess.SQLImlementation.Shifts;
using ATSDataAccess.SQLImlementation.UserLogs;
using ATSDomain.Domains.Logs;

namespace ATSWebTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int UserID=0;
            while (UserID != -1)
            {
                ShiftRepository shiftRepository = new ShiftRepository();
                Console.WriteLine("Add User ID");

                UserID = Convert.ToInt32(Console.ReadLine());
                List<Shift> shifts = shiftRepository.FindByUserID(UserID, new ActionState());

                DateTime operationDate = DateTime.Now;
                int operationType = 3;

                Console.WriteLine("Add Operation Type");
                operationType = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Add Time");
                string time = Console.ReadLine();

                operationDate = Convert.ToDateTime(time);






                if (shifts.Count > 0)
                {
                    if (shifts.Count >= 7)
                    {
                        if (operationType == 1)
                        {
                            var x = from z in shifts where z.DayID == Convert.ToInt32(operationDate.DayOfWeek) select z;
                            shifts = x.ToList<Shift>().OrderBy(c => c.InTime.TimeOfDay).ToList<Shift>();
                        }
                        else if (operationType == 3)
                        {
                            var x = from z in shifts where z.DayID == Convert.ToInt32(operationDate.DayOfWeek) select z;
                            shifts = x.ToList<Shift>().OrderByDescending(c => c.OutTime.TimeOfDay).ToList<Shift>();
                        }
                        if (shifts.Count > 0)
                        {



                            int count = 0;
                            foreach (Shift shift in shifts)
                            {
                                int maxIn = shift.MaxAllowWorkTime;
                                int minIn = shift.InAllowTime;
                                int maxOut = shift.MinAllowWorkTime;
                                int minOut = shift.OutAllowTime;

                                DateTime inMaxDateTime = shift.InTime.AddMinutes(-maxIn);
                                DateTime inMinDateTime = shift.InTime.AddMinutes(minIn);
                                DateTime inDateTime = shift.InTime;
                                DateTime outMaxDateTime = shift.OutTime.AddMinutes(maxOut);
                                DateTime outMinDateTime = shift.OutTime.AddMinutes(-minOut);
                                DateTime outDateTime = shift.OutTime;
                                if (operationDate.DayOfWeek == DayOfWeek.Friday || operationDate.DayOfWeek == DayOfWeek.Thursday)
                                {
                                    Console.WriteLine(operationDate.DayOfWeek.ToString());
                                    count++;
                                }
                                else
                                {
                                    if (operationType == 1)
                                    {
                                        int earlyTime = 0;
                                        int lateTime = 0;
                                        earlyTime = Convert.ToInt32((inMaxDateTime.TimeOfDay - operationDate.TimeOfDay).TotalMinutes);
                                        lateTime = Convert.ToInt32((operationDate.TimeOfDay - inMinDateTime.TimeOfDay).TotalMinutes);
                                        if (earlyTime < 0)
                                            earlyTime = 0;
                                        if (lateTime < 0)
                                            lateTime = 0;
                                        if (operationDate.TimeOfDay < inMaxDateTime.TimeOfDay)
                                        {


                                            Console.WriteLine("Shift ID = " + shift.ID + " InDate = " + shift.InTime.ToString("HH:mm:ss") + " CurrentDate = " + operationDate.ToString("HH:mm:ss") + " OutDate " + shift.OutTime.ToString("HH:mm:ss"));
                                            Console.WriteLine("Eaarly Time = " + earlyTime + " Late Time = " + lateTime);
                                            count++;
                                            break;
                                        }
                                        else if (operationDate.TimeOfDay > inMaxDateTime.TimeOfDay && operationDate.TimeOfDay < outDateTime.TimeOfDay)
                                        {
                                            Console.WriteLine("Shift ID = " + shift.ID + " InDate = " + shift.InTime.ToString("HH:mm:ss") + " CurrentDate = " + operationDate.ToString("HH:mm:ss") + " OutDate " + shift.OutTime.ToString("HH:mm:ss"));
                                            Console.WriteLine("Eaarly Time = " + earlyTime + " Late Time = " + lateTime);
                                            count++;
                                            break;
                                        }
                                    }
                                    else if (operationType == 3)
                                    {
                                        int earlyTime = 0;
                                        int lateTime = 0;
                                        TimeSpan tm = (outMinDateTime.TimeOfDay - operationDate.TimeOfDay);
                                        earlyTime = Convert.ToInt32((outMinDateTime.TimeOfDay - operationDate.TimeOfDay).TotalMinutes);
                                        lateTime = Convert.ToInt32((operationDate.TimeOfDay - outMaxDateTime.TimeOfDay).TotalMinutes);
                                        if (earlyTime < 0)
                                            earlyTime = 0;
                                        if (lateTime < 0)
                                            lateTime = 0;

                                        if (operationDate.TimeOfDay > inDateTime.TimeOfDay && operationDate.TimeOfDay < outMaxDateTime.TimeOfDay)
                                        {
                                            Console.WriteLine("Shift ID = " + shift.ID + " InDate = " + shift.InTime.ToString("HH:mm:ss") + " CurrentDate = " + operationDate.ToString("HH:mm:ss") + " OutDate " + shift.OutTime.ToString("HH:mm:ss"));
                                            Console.WriteLine("Eaarly Time = " + earlyTime + " Late Time = " + lateTime);
                                            count++;

                                            break;
                                        }
                                        else if (operationDate.TimeOfDay > outMaxDateTime.TimeOfDay)
                                        {
                                            Console.WriteLine("Shift ID = " + shift.ID + " InDate = " + shift.InTime.ToString("HH:mm:ss") + " CurrentDate = " + operationDate.ToString("HH:mm:ss") + " OutDate " + shift.OutTime.ToString("HH:mm:ss"));
                                            Console.WriteLine("Eaarly Time = " + earlyTime + " Late Time = " + lateTime);
                                            count++;
                                            break;
                                        }


                                    }
                                }

                            }
                            if (count == 0)
                            {
                                int maxIn = shifts[0].MaxAllowWorkTime;
                                int minIn = shifts[0].InAllowTime;
                                int maxOut = shifts[0].MinAllowWorkTime;
                                int minOut = shifts[0].OutAllowTime;
                                DateTime inMaxDateTime = shifts[0].InTime.AddMinutes(-maxIn);
                                DateTime inMinDateTime = shifts[0].InTime.AddMinutes(minIn);
                                DateTime inDateTime = shifts[0].InTime;
                                DateTime outMaxDateTime = shifts[0].OutTime.AddMinutes(maxOut);
                                DateTime outMinDateTime = shifts[0].OutTime.AddMinutes(-minOut);
                                DateTime outDateTime = shifts[0].OutTime;
                                int earlyTime = 0;
                                int lateTime = 0;
                                if (operationType == 1)
                                {



                                    earlyTime = Convert.ToInt32((operationDate.TimeOfDay - outMinDateTime.TimeOfDay).TotalMinutes);
                                    lateTime = 0;
                                    Console.WriteLine("Shift ID = " + shifts[0].ID + " InDate = " + shifts[0].InTime.ToString("HH:mm:ss") + " CurrentDate = " + operationDate.ToString("HH:mm:ss") + " OutDate " + shifts[0].OutTime.ToString("HH:mm:ss"));
                                    Console.WriteLine("Eaarly Time = " + earlyTime + " Late Time = " + lateTime);

                                }
                                else if (operationType == 3)
                                {
                                    DateTime currentDate = new DateTime(operationDate.Year, operationDate.Month, operationDate.Day, operationDate.Hour, operationDate.Minute, operationDate.Second);
                                    DateTime yesterday = new DateTime(operationDate.Year, operationDate.Month, operationDate.Day, shifts[0].OutTime.Hour, shifts[0].OutTime.Minute, shifts[0].OutTime.Second);
                                    yesterday = yesterday.AddDays(-1);
                                    TimeSpan timeSpan = currentDate - yesterday;

                                    var c = from z in shifts select z;
                                    shifts = c.ToList<Shift>().OrderByDescending(k => k.OutTime.TimeOfDay).ToList<Shift>();
                                    if (shifts.Count > 0)
                                    {
                                        earlyTime = 0;
                                        lateTime = 0;
                                        lateTime = Convert.ToInt32(timeSpan.TotalMinutes);
                                        Console.WriteLine("Shift ID = " + shifts[0].ID + " InDate = " + shifts[0].InTime.ToString("HH:mm:ss") + " CurrentDate = " + operationDate.ToString("HH:mm:ss") + " OutDate " + shifts[0].OutTime.ToString("HH:mm:ss"));
                                        Console.WriteLine("Eaarly Time = " + earlyTime + " Late Time = " + lateTime);
                                    }
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("Error No Shift InThis Day");
                        }
                    }
                    else if (shifts.Count == 1)
                    {
                        if (shifts[0].IsNormalShift)
                        {
                            int maxIn = shifts[0].MaxAllowWorkTime;
                            int minIn = shifts[0].InAllowTime;
                            int maxOut = shifts[0].MinAllowWorkTime;
                            int minOut = shifts[0].OutAllowTime;

                            DateTime inMaxDateTime = shifts[0].InTime.AddMinutes(-maxIn);
                            DateTime inMinDateTime = shifts[0].InTime.AddMinutes(minIn);
                            DateTime inDateTime = shifts[0].InTime;
                            DateTime outMaxDateTime = shifts[0].OutTime.AddMinutes(maxOut);
                            DateTime outMinDateTime = shifts[0].OutTime.AddMinutes(-minOut);
                            DateTime outDateTime = shifts[0].OutTime;


                            if (operationDate.DayOfWeek == DayOfWeek.Friday || operationDate.DayOfWeek == DayOfWeek.Thursday)
                            {
                                Console.WriteLine(operationDate.DayOfWeek.ToString());
                                //count++;
                            }
                            else
                            {

                                if (operationType == 1)
                                {
                                    int earlyTime = 0;
                                    int lateTime = 0;
                                    earlyTime = Convert.ToInt32((inMaxDateTime.TimeOfDay - operationDate.TimeOfDay).TotalMinutes);
                                    lateTime = Convert.ToInt32((operationDate.TimeOfDay - inMinDateTime.TimeOfDay).TotalMinutes);
                                    if (earlyTime < 0)
                                        earlyTime = 0;
                                    if (lateTime < 0)
                                        lateTime = 0;



                                    if (operationDate.TimeOfDay < inMaxDateTime.TimeOfDay)
                                    {

                                        List<LogMain> mainLogList = new List<LogMain>();
                                        LogMainRepository LogMainRepository = new LogMainRepository();
                                        mainLogList = LogMainRepository.FindByDateAndUserID(operationDate, UserID, new ActionState());



                                        Console.WriteLine("Shift ID = " + shifts[0].ID + " InDate = " + shifts[0].InTime.ToString("HH:mm:ss") + " CurrentDate = " + operationDate.ToString("HH:mm:ss") + " OutDate " + shifts[0].OutTime.ToString("HH:mm:ss"));
                                        Console.WriteLine("Eaarly Time = " + earlyTime + " Late Time = " + lateTime);

                                    }
                                    else if (operationDate.TimeOfDay > inMinDateTime.TimeOfDay && operationDate.TimeOfDay < outDateTime.TimeOfDay)
                                    {
                                        Console.WriteLine("Shift ID = " + shifts[0].ID + " InDate = " + shifts[0].InTime.ToString("HH:mm:ss") + " CurrentDate = " + operationDate.ToString("HH:mm:ss") + " OutDate " + shifts[0].OutTime.ToString("HH:mm:ss"));
                                        Console.WriteLine("Eaarly Time = " + earlyTime + " Late Time = " + lateTime);

                                    }

                                    else
                                    {

                                        earlyTime = Convert.ToInt32((operationDate.TimeOfDay - inMaxDateTime.TimeOfDay).TotalMinutes);
                                        lateTime = 0;
                                        Console.WriteLine("Shift ID = " + shifts[0].ID + " InDate = " + shifts[0].InTime.ToString("HH:mm:ss") + " CurrentDate = " + operationDate.ToString("HH:mm:ss") + " OutDate " + shifts[0].OutTime.ToString("HH:mm:ss"));
                                        Console.WriteLine("Eaarly Time = " + earlyTime + " Late Time = " + lateTime);

                                    }

                                }
                                else if (operationType == 3)
                                {

                                    if ((operationDate.TimeOfDay - outDateTime.TimeOfDay).TotalMinutes < 0)
                                    {
                                        DateTime currentDate = new DateTime(operationDate.Year, operationDate.Month, operationDate.Day, operationDate.Hour, operationDate.Minute, operationDate.Second);
                                        DateTime yesterday = new DateTime(operationDate.Year, operationDate.Month, operationDate.Day, shifts[0].OutTime.Hour, shifts[0].OutTime.Minute, shifts[0].OutTime.Second);
                                        yesterday = yesterday.AddDays(-1);
                                        TimeSpan timeSpan = currentDate - yesterday;

                                        var c = from z in shifts select z;
                                        shifts = c.ToList<Shift>().OrderByDescending(k => k.OutTime.TimeOfDay).ToList<Shift>();
                                        if (shifts.Count > 0)
                                        {
                                            int earlyTime = 0;
                                            int lateTime = 0;
                                            lateTime = Convert.ToInt32(timeSpan.TotalMinutes);
                                            Console.WriteLine("Shift ID = " + shifts[0].ID + " InDate = " + shifts[0].InTime.ToString("HH:mm:ss") + " CurrentDate = " + operationDate.ToString("HH:mm:ss") + " OutDate " + shifts[0].OutTime.ToString("HH:mm:ss"));
                                            Console.WriteLine("Eaarly Time = " + earlyTime + " Late Time = " + lateTime);
                                        }
                                    }
                                    else
                                    {
                                        int earlyTime = 0;
                                        int lateTime = 0;
                                        TimeSpan tm = (outMinDateTime.TimeOfDay - operationDate.TimeOfDay);
                                        earlyTime = Convert.ToInt32((outMinDateTime.TimeOfDay - operationDate.TimeOfDay).TotalMinutes);
                                        lateTime = Convert.ToInt32((operationDate.TimeOfDay - outMaxDateTime.TimeOfDay).TotalMinutes);
                                        if (earlyTime < 0)
                                            earlyTime = 0;
                                        if (lateTime < 0)
                                            lateTime = 0;

                                        if (operationDate.TimeOfDay > inDateTime.TimeOfDay && operationDate.TimeOfDay < outMaxDateTime.TimeOfDay)
                                        {
                                            Console.WriteLine("Shift ID = " + shifts[0].ID + " InDate = " + shifts[0].InTime.ToString("HH:mm:ss") + " CurrentDate = " + operationDate.ToString("HH:mm:ss") + " OutDate " + shifts[0].OutTime.ToString("HH:mm:ss"));
                                            Console.WriteLine("Eaarly Time = " + earlyTime + " Late Time = " + lateTime);

                                        }
                                        else if (operationDate.TimeOfDay > outMaxDateTime.TimeOfDay)
                                        {
                                            Console.WriteLine("Shift ID = " + shifts[0].ID + " InDate = " + shifts[0].InTime.ToString("HH:mm:ss") + " CurrentDate = " + operationDate.ToString("HH:mm:ss") + " OutDate " + shifts[0].OutTime.ToString("HH:mm:ss"));
                                            Console.WriteLine("Eaarly Time = " + earlyTime + " Late Time = " + lateTime);

                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("Error User Must Has Normal Shift");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error Shifts Counts");
                    }
                }
                else
                {
                    Console.WriteLine("Error No Shifts");
                }
            }
        }
    }
}
