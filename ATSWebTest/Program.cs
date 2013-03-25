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
            ShiftRepository shiftRepository = new ShiftRepository();
            List<Shift> shifts = shiftRepository.FindByUserID(990298, new ActionState());

            DateTime operationDate = DateTime.Now;
            int operationType = 3;

            Console.WriteLine("Add Operation Type");
            operationType = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Add Time");
            string time = Console.ReadLine();

            operationDate = Convert.ToDateTime(time);



            LogMain mainLog = new LogMain();


            if (shifts.Count > 0)
            {
                if (shifts.Count >= 7)
                {
                    if (operationType == 1)
                    {
                        var x = from z in shifts where z.DayID == Convert.ToInt32(operationDate.DayOfWeek) select z;
                        shifts = x.ToList<Shift>().OrderBy(c => c.InTime).ToList<Shift>();
                    }
                    else if (operationType == 3)
                    {
                        var x = from z in shifts where z.DayID == Convert.ToInt32(operationDate.DayOfWeek) select z;
                        shifts = x.ToList<Shift>().OrderByDescending(c => c.OutTime).ToList<Shift>();
                    }
                    if (shifts.Count > 0)
                    {
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
                                    break;
                                }
                                else if (operationDate.TimeOfDay > inMinDateTime.TimeOfDay && operationDate.TimeOfDay < outDateTime.TimeOfDay)
                                {
                                    Console.WriteLine("Shift ID = " + shift.ID + " InDate = " + shift.InTime.ToString("HH:mm:ss") + " CurrentDate = " + operationDate.ToString("HH:mm:ss") + " OutDate " + shift.OutTime.ToString("HH:mm:ss"));
                                    Console.WriteLine("Eaarly Time = " + earlyTime + " Late Time = " + lateTime);
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
                                    break;
                                }
                                else if (operationDate.TimeOfDay > outMaxDateTime.TimeOfDay)
                                {
                                    Console.WriteLine("Shift ID = " + shift.ID + " InDate = " + shift.InTime.ToString("HH:mm:ss") + " CurrentDate = " + operationDate.ToString("HH:mm:ss") + " OutDate " + shift.OutTime.ToString("HH:mm:ss"));
                                    Console.WriteLine("Eaarly Time = " + earlyTime + " Late Time = " + lateTime);
                                    break;
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

                        DateTime outMaxDateTime = shifts[0].OutTime.AddMinutes(maxOut);
                        DateTime outMinDateTime = shifts[0].OutTime.AddMinutes(-minOut);

                        if (operationDate.TimeOfDay > inMaxDateTime.TimeOfDay)
                        {

                        }
                        else if (operationDate.TimeOfDay > inMinDateTime.TimeOfDay && operationDate.TimeOfDay < shifts[0].OutTime.TimeOfDay)
                        {

                        }
                        else if (operationDate.TimeOfDay > shifts[0].OutTime.TimeOfDay)
                        {

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
