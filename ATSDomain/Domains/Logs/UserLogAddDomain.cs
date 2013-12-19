using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon;
using ATSCommon.Entites.Devices;
using ATSCommon.Entites.HandPunch;
using ATSCommon.Entites.Logs;
using ATSCommon.Entites.Shifts;
using ATSCommon.Entites.UserLogs;
using ATSCommon.Enums;
using ATSDataAccess.Constants.Common;
using ATSDataAccess.SQLImlementation.HandPunch;
using ATSDataAccess.SQLImlementation.Logs;
using ATSDataAccess.SQLImlementation.Shifts;
using ATSDataAccess.SQLImlementation.UserLogs;

namespace ATSDomain.Domains.Logs
{
    public class UserLogAddDomain
    {
        public void AddNewLog(int userID, int loggingType, DateTime loggingDate, int deviceID)
        {

            //insert UserLog 
            LogUserLog(userID, deviceID, loggingType, loggingDate, CommonConstants.Err_CnnotInsertUserLog, OperationSourceEnum.InternalDB);

            ActionState actionState;
            //try get the user shifts
            int day = loggingDate.Day;
            List<Shift> shiftList = new List<Shift>();
            ShiftRepository shiftRepository = new ShiftRepository();
            actionState = new ActionState();
            shiftList = shiftRepository.FindByUserIDAndDate(userID, loggingDate, actionState);
            //if the error happened in shift Loading
            if (actionState.Status != ActionStatusEnum.NoError)
            {
                LogErrors(CommonConstants.Err_CnnotGetUserShift, actionState.Result, OperationSourceEnum.OracleDB, LogLevelEnum.Critical);
            }
            else
            {
                actionState = new ActionState();
                //user has no shifts
                if (shiftList.Count == 0)
                {
                    //try find the default shift
                    shiftList = shiftRepository.FindDefaultByUserID(userID, actionState);
                    if (actionState.Status != ActionStatusEnum.NoError)
                    {
                        LogErrors(CommonConstants.Err_CannotFindUserDefaultShift, actionState.Result, OperationSourceEnum.OracleDB, LogLevelEnum.Critical);
                    }
                    else
                    {
                        // if no shift and no default shift
                        if (shiftList.Count == 0)
                        {
                            LogLostUser(userID, deviceID, loggingType, loggingDate, CommonConstants.Err_ThereIsNoSHift, OperationSourceEnum.OracleDB);

                        }
                        // has at least one default shift
                        else
                        {

                        }
                    }

                }
                //user has at least one shift shift(s)
                else
                {

                }
            }
        }

        public void FindUserShift(int userID, int deviceID, int operationType, DateTime operationDate, Shift shift, LogMain logMain)
        {
            ShiftRepository shiftRepository = new ShiftRepository();
            List<Shift> shiftCurrentDayList = new List<Shift>();
            List<Shift> shiftYesterDayList = new List<Shift>();
            List<Shift> shiftList = new List<Shift>();
            ActionState actionState = new ActionState();

            List<LogMain> logMainList = new List<LogMain>();
            LogMainRepository logMainRepository = new LogMainRepository();
            shiftList = shiftRepository.FindByUserID(userID, actionState);
            if (actionState.Status != ActionStatusEnum.NoError)
            {
                LogLostUser(userID, deviceID, operationType, operationDate, actionState.Result, OperationSourceEnum.OracleDB);
            }
            else
            {
            //if there is no shift assigned to the user
            A: if (shiftList.Count == 0)
                {
                    actionState = new ActionState();
                    //retrive the default shift values
                    shiftList = shiftRepository.FindDefaultByUserID(userID, actionState);
                    if (actionState.Status != ActionStatusEnum.NoError)
                    {
                        LogLostUser(userID, deviceID, operationType, operationDate, actionState.Result, OperationSourceEnum.OracleDB);
                    }
                    else
                    {
                        if (shiftList.Count == 0)
                        {
                            LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_ThereIsNoSHift, OperationSourceEnum.OracleDB);
                        }
                        else if (shiftList.Count == 1)
                        {
                            shift = shiftList[0];
                            goto A;
                        }
                        else
                        {
                            //Important Add default shift operations
                            var todayList = from z in shiftList where z.DayID == Convert.ToInt32(operationDate.DayOfWeek) select z;
                            shiftCurrentDayList = todayList.ToList<Shift>();

                            var yesterdayList = from z in shiftList where z.DayID == Convert.ToInt32(operationDate.AddDays(-1).DayOfWeek) select z;
                            shiftYesterDayList = yesterdayList.ToList<Shift>();

                            goto A;
                        }
                    }
                }
                else if (shiftList.Count == 1)
                {
                    if (operationType == 1)
                    {
                        Shift yesterdayShift = new Shift();
                        if (shiftYesterDayList.Count > 0)
                        {
                            yesterdayShift = shiftYesterDayList.OrderByDescending(c => c.InTime).First();
                        }
                        if (operationDate.TimeOfDay < shiftList[0].InTime.TimeOfDay && (yesterdayShift.OutTime.TimeOfDay - yesterdayShift.InTime.TimeOfDay).TotalMinutes < 0 && operationDate.TimeOfDay < yesterdayShift.OutTime.TimeOfDay)
                        {
                            shift = yesterdayShift;
                        }
                        else if (operationDate.TimeOfDay < shiftList[0].OutTime.TimeOfDay)
                            shift = shiftList[0];

                        logMainList = logMainRepository.FindByDateAndUserID(operationDate, userID, actionState);
                        if (actionState.Status != ActionStatusEnum.NoError)
                        {
                            LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_ProblemInGetLogMain, OperationSourceEnum.OracleDB);
                        }
                        else
                        {
                            if (logMainList.Count == 0)
                            {
                                if (shift.ID == 0)
                                {
                                    logMain.AttendanceDate = operationDate;
                                    logMain.InDate = operationDate;
                                    logMain.IsClosed = false;
                                    logMain.MissIN = 0;
                                    logMain.MissOut = 0;
                                    logMain.OutDate = null;
                                    logMain.PlusIN = 0;
                                    logMain.PlusOut = 0;
                                    logMain.ShiftID = 0;
                                    logMain.UserID = userID;
                                    actionState = new ActionState();
                                    logMainRepository.Insert(logMain, actionState);
                                    if (actionState.Status == ActionStatusEnum.NoError)
                                    {
                                        LogDetails logDetails = new LogDetails();
                                        logDetails.DeviceID = deviceID;
                                        logDetails.IsWrong = false;
                                        logDetails.LogMainID = logMain.ID;
                                        logDetails.OperationDate = operationDate;
                                        logDetails.OperationType = 1;

                                        LogDetailRepository logDetailRepository = new LogDetailRepository();
                                        logDetailRepository.Insert(logDetails, new ActionState());
                                    }
                                    else
                                    {
                                        LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_CnnotInsertUserLog, OperationSourceEnum.OracleDB);
                                    }
                                }
                                else
                                {
                                    logMain = ShiftCalculations(shift, operationDate, operationType);
                                    logMain.AttendanceDate = operationDate;
                                    logMain.InDate = operationDate;
                                    logMain.IsClosed = false;
                                    logMain.MissOut = 0;
                                    logMain.OutDate = null;
                                    logMain.PlusOut = 0;
                                    logMain.ShiftID = shift.ID;
                                    logMain.UserID = userID;
                                    actionState = new ActionState();
                                    logMainRepository.Insert(logMain, actionState);
                                    if (actionState.Status == ActionStatusEnum.NoError)
                                    {
                                        LogDetails logDetails = new LogDetails();
                                        logDetails.DeviceID = deviceID;
                                        logDetails.IsWrong = false;
                                        logDetails.LogMainID = logMain.ID;
                                        logDetails.OperationDate = operationDate;
                                        logDetails.OperationType = 1;

                                        LogDetailRepository logDetailRepository = new LogDetailRepository();
                                        logDetailRepository.Insert(logDetails, new ActionState());
                                    }
                                    else
                                    {
                                        LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_CnnotInsertUserLog, OperationSourceEnum.OracleDB);
                                    }
                                }
                            }
                            else
                            {
                                logMain = logMainList.OrderByDescending(c => c.InDate).ToList<LogMain>().First();
                                if (shift.ID == 0)
                                {
                                    LogDetails logDetails = new LogDetails();
                                    if (logMain.IsClosed == false)
                                        logDetails.IsWrong = true;
                                    else
                                        logDetails.IsWrong = false;

                                    logMain.AttendanceDate = operationDate;
                                    logMain.InDate = operationDate;
                                    logMain.IsClosed = false;
                                    logMain.MissIN = 0;
                                    logMain.MissOut = 0;
                                    logMain.OutDate = null;
                                    logMain.PlusIN = 0;
                                    logMain.PlusOut = 0;
                                    logMain.ShiftID = 0;
                                    logMain.UserID = userID;
                                    actionState = new ActionState();
                                    logMainRepository.Insert(logMain, actionState);
                                    if (actionState.Status == ActionStatusEnum.NoError)
                                    {

                                        logDetails.DeviceID = deviceID;

                                        logDetails.LogMainID = logMain.ID;
                                        logDetails.OperationDate = operationDate;
                                        logDetails.OperationType = 1;

                                        LogDetailRepository logDetailRepository = new LogDetailRepository();
                                        logDetailRepository.Insert(logDetails, new ActionState());
                                    }
                                    else
                                    {
                                        LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_CnnotInsertUserLog, OperationSourceEnum.OracleDB);
                                    }
                                }
                                else
                                {
                                    LogDetails logDetails = new LogDetails();
                                    if (logMain.IsClosed == false)
                                        logDetails.IsWrong = true;
                                    else
                                        logDetails.IsWrong = false;
                                    if (shift.ID != logMain.ShiftID)
                                    {
                                        logMain = ShiftCalculations(shift, operationDate, operationType);
                                        logMain.AttendanceDate = operationDate;
                                        logMain.InDate = operationDate;
                                        logMain.IsClosed = false;
                                        logMain.MissOut = 0;
                                        logMain.OutDate = null;
                                        logMain.PlusOut = 0;
                                        logMain.ShiftID = shift.ID;
                                        logMain.UserID = userID;
                                        actionState = new ActionState();
                                        logMainRepository.Insert(logMain, actionState);
                                        if (actionState.Status == ActionStatusEnum.NoError)
                                        {

                                            logDetails.DeviceID = deviceID;
                                            logDetails.IsWrong = false;
                                            logDetails.LogMainID = logMain.ID;
                                            logDetails.OperationDate = operationDate;
                                            logDetails.OperationType = 1;

                                            LogDetailRepository logDetailRepository = new LogDetailRepository();
                                            logDetailRepository.Insert(logDetails, new ActionState());
                                        }
                                        else
                                        {
                                            LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_CnnotInsertUserLog, OperationSourceEnum.OracleDB);
                                        }
                                    }
                                    else
                                    {
                                       
                                        if (logMain.InDate == null)
                                        {

                                           // logMain = logMainList.OrderByDescending(c => c.InDate).First();
                                            logMain = ShiftCalculations(shiftList[0], operationDate, operationType);
                                            logMain.OutDate = logMainList.OrderByDescending(c => c.InDate).First().OutDate;
                                            logMain.PlusOut = logMainList.OrderByDescending(c => c.InDate).First().PlusOut;
                                            logMain.MissOut = logMainList.OrderByDescending(c => c.InDate).First().MissOut;
                                            logMain.ID = logMainList.OrderByDescending(c => c.InDate).First().ID;
                                            logMain.UserID = userID;
                                            logMain.IsClosed = true;
                                            logMainRepository.Update(logMain, actionState);
                                            if (actionState.Status == ActionStatusEnum.NoError)
                                            {
                                                logDetails = new LogDetails();
                                                logDetails.IsWrong = true;
                                                logDetails.DeviceID = deviceID;
                                                logDetails.IsWrong = false;
                                                logDetails.LogMainID = logMain.ID;
                                                logDetails.OperationDate = operationDate;
                                                logDetails.OperationType = 1;

                                                LogDetailRepository logDetailRepository = new LogDetailRepository();
                                                logDetailRepository.Insert(logDetails, new ActionState());

                                                List<LogDetails> logDetailsList = new List<LogDetails>();
                                                logDetailsList = logDetailRepository.FindByLogMainID(logMain.ID, new ActionState());
                                                var z = from x in logDetailsList where x.OperationType == 3 select x;
                                                logDetailsList = z.ToList<LogDetails>().OrderByDescending(c => c.OperationDate).ToList<LogDetails>();

                                                logDetails = logDetailsList.First();
                                                logDetails.IsWrong = false;
                                                logDetailRepository.Update(logDetails, new ActionState());

                                            }
                                            else
                                            {
                                                LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_CnnotInsertUserLog, OperationSourceEnum.OracleDB);
                                            }
                                        }
                                        else
                                        {
                                            logDetails = new LogDetails();
                                            logDetails.IsWrong = true;
                                            logDetails.DeviceID = deviceID;
                                            logDetails.IsWrong = false;
                                            logDetails.LogMainID = logMain.ID;
                                            logDetails.OperationDate = operationDate;
                                            logDetails.OperationType = 1;

                                            LogDetailRepository logDetailRepository = new LogDetailRepository();
                                            logDetailRepository.Insert(logDetails, new ActionState());
                                        }
                                    }

                                }
                            }
                        }

                    }
                    else
                    {
                        actionState = new ActionState();
                        logMainList = logMainRepository.FindByDateAndUserIDAndIsNotClosed(operationDate, userID, actionState);
                        if (actionState.Status != ActionStatusEnum.NoError)
                        {
                            LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_ProblemInGetLogMain, OperationSourceEnum.OracleDB);
                        }
                        else
                        {

                            if (logMainList.Count == 0)
                            {
                                actionState = new ActionState();
                                logMainList = logMainRepository.FindByDateAndUserIDAndIsNotClosed(operationDate.AddDays(-1), userID, actionState);
                                if (actionState.Status != ActionStatusEnum.NoError)
                                {
                                    LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_ProblemInGetLogMain, OperationSourceEnum.OracleDB);
                                }
                                else
                                {
                                    if (logMainList.Count == 0)
                                    {
                                        if (shiftList[0].OutTime.AddMinutes(shiftList[0].MinAllowWorkTime).TimeOfDay >= operationDate.TimeOfDay && shiftList[0].InTime.TimeOfDay < operationDate.TimeOfDay)
                                        {
                                            logMain = ShiftCalculations(shiftList[0], operationDate, operationType);
                                            logMain.UserID = userID;
                                            actionState = new ActionState();
                                            logMainRepository.Insert(logMain, actionState);
                                            if (actionState.Status != ActionStatusEnum.NoError)
                                            {
                                                LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_CnnotInsertUserLog, OperationSourceEnum.OracleDB);
                                            }
                                            else
                                            {
                                                LogDetails logDetails = new LogDetails();
                                                logDetails.DeviceID = deviceID;
                                                logDetails.IsWrong = true;
                                                logDetails.LogMainID = logMain.ID;
                                                logDetails.OperationDate = operationDate;
                                                logDetails.OperationType = 3;

                                                LogDetailRepository logDetailRepository = new LogDetailRepository();
                                                logDetailRepository.Insert(logDetails, new ActionState());
                                            }
                                        }
                                        else
                                        {
                                            logMain.UserID = userID;
                                            logMain.AttendanceDate = operationDate;
                                            logMain.OutDate = operationDate;
                                            logMain.IsClosed = false;
                                            logMain.MissIN = 0;
                                            logMain.MissOut = 0;
                                            logMain.PlusIN = 0;
                                            logMain.PlusOut = 0;
                                            logMain.ShiftID = 0;
                                            actionState = new ActionState();
                                            logMainRepository.Insert(logMain, actionState);
                                            if (actionState.Status != ActionStatusEnum.NoError)
                                            {
                                                LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_CnnotInsertUserLog, OperationSourceEnum.OracleDB);
                                            }
                                            else
                                            {
                                                LogDetails logDetails = new LogDetails();
                                                logDetails.DeviceID = deviceID;
                                                logDetails.IsWrong = true;
                                                logDetails.LogMainID = logMain.ID;
                                                logDetails.OperationDate = operationDate;
                                                logDetails.OperationType = 3;

                                                LogDetailRepository logDetailRepository = new LogDetailRepository();
                                                logDetailRepository.Insert(logDetails, new ActionState());
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (operationDate.TimeOfDay <= shiftList[0].InTime.TimeOfDay)
                                        {
                                            logMain.UserID = userID;
                                            logMain.AttendanceDate = operationDate;
                                            logMain.OutDate = operationDate;
                                            logMain.IsClosed = false;
                                            logMain.MissIN = 0;
                                            logMain.MissOut = 0;
                                            logMain.PlusIN = 0;
                                            logMain.PlusOut = 0;
                                            logMain.ShiftID = 0;
                                            actionState = new ActionState();
                                            logMainRepository.Insert(logMain, actionState);
                                            if (actionState.Status != ActionStatusEnum.NoError)
                                            {
                                                LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_CnnotInsertUserLog, OperationSourceEnum.OracleDB);
                                            }
                                            else
                                            {
                                                LogDetails logDetails = new LogDetails();
                                                logDetails.DeviceID = deviceID;
                                                logDetails.IsWrong = true;
                                                logDetails.LogMainID = logMain.ID;
                                                logDetails.OperationDate = operationDate;
                                                logDetails.OperationType = 3;

                                                LogDetailRepository logDetailRepository = new LogDetailRepository();
                                                logDetailRepository.Insert(logDetails, new ActionState());
                                            }
                                        }
                                        else
                                        {

                                            logMain = ShiftCalculations(shiftList[0], operationDate, operationType);
                                            logMain.UserID = userID;
                                            actionState = new ActionState();
                                            logMainRepository.Insert(logMain, actionState);
                                            if (actionState.Status != ActionStatusEnum.NoError)
                                            {
                                                LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_CnnotInsertUserLog, OperationSourceEnum.OracleDB);
                                            }
                                            else
                                            {
                                                LogDetails logDetails = new LogDetails();
                                                logDetails.DeviceID = deviceID;
                                                logDetails.IsWrong = true;
                                                logDetails.LogMainID = logMain.ID;
                                                logDetails.OperationDate = operationDate;
                                                logDetails.OperationType = 3;

                                                LogDetailRepository logDetailRepository = new LogDetailRepository();
                                                logDetailRepository.Insert(logDetails, new ActionState());
                                            }
                                        }

                                    }
                                }
                            }
                            else
                            {
                                logMain = logMainList.OrderByDescending(c => c.InDate).First();

                                if (operationDate.TimeOfDay <= shiftList[0].InTime.TimeOfDay)
                                {
                                    logMain.UserID = userID;
                                    logMain.AttendanceDate = operationDate;
                                    logMain.OutDate = operationDate;
                                    logMain.IsClosed = false;
                                    logMain.MissIN = 0;
                                    logMain.MissOut = 0;
                                    logMain.PlusIN = 0;
                                    logMain.PlusOut = 0;
                                    logMain.ShiftID = 0;
                                    actionState = new ActionState();
                                    logMainRepository.Insert(logMain, actionState);
                                    if (actionState.Status != ActionStatusEnum.NoError)
                                    {
                                        LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_CnnotInsertUserLog, OperationSourceEnum.OracleDB);
                                    }
                                    else
                                    {
                                        LogDetails logDetails = new LogDetails();
                                        logDetails.DeviceID = deviceID;
                                        logDetails.IsWrong = true;
                                        logDetails.LogMainID = logMain.ID;
                                        logDetails.OperationDate = operationDate;
                                        logDetails.OperationType = 3;

                                        LogDetailRepository logDetailRepository = new LogDetailRepository();
                                        logDetailRepository.Insert(logDetails, new ActionState());
                                    }
                                }
                                else if (logMain.InDate != null)
                                {

                                    logMain = ShiftCalculations(shiftList[0], operationDate, operationType);
                                    logMain.InDate = logMainList.OrderByDescending(c => c.InDate).First().InDate;
                                    logMain.PlusIN = logMainList.OrderByDescending(c => c.InDate).First().PlusIN;
                                    logMain.MissIN = logMainList.OrderByDescending(c => c.InDate).First().MissIN;
                                    logMain.ID = logMainList.OrderByDescending(c => c.InDate).First().ID;
                                    logMain.UserID = userID;
                                    logMain.IsClosed = true;
                                    actionState = new ActionState();
                                    logMainRepository.Update(logMain, actionState);
                                    if (actionState.Status != ActionStatusEnum.NoError)
                                    {
                                        LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_CnnotInsertUserLog, OperationSourceEnum.OracleDB);
                                    }
                                    else
                                    {
                                        LogDetails logDetails = new LogDetails();
                                        logDetails.DeviceID = deviceID;
                                        logDetails.IsWrong = false;
                                        logDetails.LogMainID = logMain.ID;
                                        logDetails.OperationDate = operationDate;
                                        logDetails.OperationType = 3;

                                        LogDetailRepository logDetailRepository = new LogDetailRepository();
                                        logDetailRepository.Insert(logDetails, new ActionState());
                                    }
                                }
                                else
                                {
                                    LogDetails logDetails = new LogDetails();
                                    logDetails.DeviceID = deviceID;
                                    logDetails.IsWrong = true;
                                    logDetails.LogMainID = logMain.ID;
                                    logDetails.OperationDate = operationDate;
                                    logDetails.OperationType = 3;

                                    LogDetailRepository logDetailRepository = new LogDetailRepository();
                                    logDetailRepository.Insert(logDetails, new ActionState());

                                }
                            }
                        }
                    }

                }
                else
                {
                    var todayList = from z in shiftList where z.DayID == Convert.ToInt32(operationDate.DayOfWeek) select z;
                    shiftCurrentDayList = todayList.ToList<Shift>();

                    var yesterdayList = from z in shiftList where z.DayID == Convert.ToInt32(operationDate.AddDays(-1).DayOfWeek) select z;
                    shiftYesterDayList = yesterdayList.ToList<Shift>();

                    actionState = new ActionState();
                    logMainList = logMainRepository.FindByDateAndUserID(operationDate, userID, actionState);
                    if (actionState.Status == ActionStatusEnum.NoError)
                    {
                        if (logMainList.Count == 0)
                        {
                            logMainList = logMainRepository.FindByDateAndUserID(operationDate.AddDays(-1), userID, actionState);
                            actionState = new ActionState();
                            if (actionState.Status == ActionStatusEnum.NoError)
                            {
                                if (operationType == 1)
                                {
                                    var todayIn = from z in todayList
                                                  where z.InTime.AddMinutes(-z.MaxAllowWorkTime).TimeOfDay < operationDate.TimeOfDay &&
                                                      z.OutTime.TimeOfDay > operationDate.TimeOfDay
                                                  select z;

                                    List<Shift> shiftDailyList = new List<Shift>();
                                    shiftDailyList = todayIn.ToList<Shift>();
                                    Shift yesterdayShift = yesterdayList.OrderByDescending(c => c.InTime).First();

                                    if (shiftDailyList.Count > 0)
                                    {
                                        shift = shiftDailyList.First();
                                    }
                                    else if (operationDate.TimeOfDay < shiftList[0].InTime.TimeOfDay && (yesterdayShift.OutTime.TimeOfDay - yesterdayShift.InTime.TimeOfDay).TotalMinutes < 0 && operationDate.TimeOfDay < yesterdayShift.OutTime.TimeOfDay)
                                    {
                                        shift = yesterdayShift;
                                    }
                                    else
                                    {
                                        int distance = int.MaxValue;
                                        todayList = todayList.OrderBy(c => c.InTime);
                                        foreach (Shift todayShift in todayList)
                                        {
                                            if ((todayShift.OutTime.TimeOfDay - todayShift.InTime.TimeOfDay).TotalMinutes > 0)
                                            {
                                                if (operationDate.TimeOfDay < todayShift.OutTime.TimeOfDay)
                                                {
                                                    distance = Convert.ToInt32(Math.Abs((todayShift.InTime.TimeOfDay - operationDate.TimeOfDay).TotalMinutes));
                                                    shift = todayShift;
                                                }
                                            }
                                            else
                                            {
                                                if (operationDate.AddHours(-12).TimeOfDay < todayShift.OutTime.AddHours(-12).TimeOfDay)
                                                {
                                                    distance = Convert.ToInt32(Math.Abs((todayShift.InTime.AddHours(-12).TimeOfDay - operationDate.AddHours(-12).TimeOfDay).TotalMinutes));
                                                    shift = todayShift;
                                                }
                                            }
                                        }
                                    }

                                    if (shift.ID == 0)
                                    {
                                        logMain.AttendanceDate = operationDate;
                                        logMain.InDate = operationDate;
                                        logMain.IsClosed = false;
                                        logMain.MissIN = 0;
                                        logMain.MissOut = 0;
                                        logMain.OutDate = null;
                                        logMain.PlusIN = 0;
                                        logMain.PlusOut = 0;
                                        logMain.ShiftID = 0;
                                        logMain.UserID = userID;
                                        actionState = new ActionState();
                                        logMainRepository.Insert(logMain, actionState);
                                        if (actionState.Status == ActionStatusEnum.NoError)
                                        {
                                            LogDetails logDetails = new LogDetails();
                                            logDetails.DeviceID = deviceID;
                                            logDetails.IsWrong = false;
                                            logDetails.LogMainID = logMain.ID;
                                            logDetails.OperationDate = operationDate;
                                            logDetails.OperationType = 1;

                                            LogDetailRepository logDetailRepository = new LogDetailRepository();
                                            logDetailRepository.Insert(logDetails, new ActionState());
                                        }
                                        else
                                        {
                                            LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_CnnotInsertUserLog, OperationSourceEnum.OracleDB);
                                        }
                                    }
                                    else
                                    {

                                        logMain = ShiftCalculations(shift, operationDate, operationType);
                                        logMain.AttendanceDate = operationDate;
                                        logMain.InDate = operationDate;
                                        logMain.IsClosed = false;
                                        logMain.MissOut = 0;
                                        logMain.OutDate = null;
                                        logMain.PlusOut = 0;
                                        logMain.ShiftID = shift.ID;
                                        logMain.UserID = userID;
                                        actionState = new ActionState();
                                        logMainRepository.Insert(logMain, actionState);
                                        if (actionState.Status == ActionStatusEnum.NoError)
                                        {
                                            LogDetails logDetails = new LogDetails();
                                            logDetails.DeviceID = deviceID;
                                            logDetails.IsWrong = false;
                                            logDetails.LogMainID = logMain.ID;
                                            logDetails.OperationDate = operationDate;
                                            logDetails.OperationType = 1;

                                            LogDetailRepository logDetailRepository = new LogDetailRepository();
                                            logDetailRepository.Insert(logDetails, new ActionState());
                                        }
                                        else
                                        {
                                            LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_CnnotInsertUserLog, OperationSourceEnum.OracleDB);
                                        }
                                    }
                                }
                                else
                                {
                                    //add operationType=3 and logMainList.Count == 0

                                  
                                }
                            }
                            else
                            {
                                LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_ProblemInGetLogMain, OperationSourceEnum.OracleDB);
                            }
                        }
                        else
                        {
                            if (operationType == 1)
                            {

                                var todayIn = from z in todayList
                                              where z.InTime.AddMinutes(-z.MaxAllowWorkTime).TimeOfDay < operationDate.TimeOfDay &&
                                                  z.OutTime.TimeOfDay > operationDate.TimeOfDay
                                              select z;

                                List<Shift> shiftDailyList = new List<Shift>();
                                shiftDailyList = todayIn.ToList<Shift>();
                                Shift yesterdayShift = yesterdayList.OrderByDescending(c => c.InTime).First();
                                if (shiftDailyList.Count > 0)
                                {
                                    /////////////////////////////////////////////////////////////////////////////////////////////////
                                    shift = shiftDailyList.First();

                                }
                                else if (operationDate.TimeOfDay < shiftList[0].InTime.TimeOfDay && (yesterdayShift.OutTime.TimeOfDay - yesterdayShift.InTime.TimeOfDay).TotalMinutes < 0 && operationDate.TimeOfDay < yesterdayShift.OutTime.TimeOfDay)
                                {
                                    shift = yesterdayShift;
                                }
                                else
                                {
                                    int distance = int.MaxValue;
                                    todayList = todayList.OrderBy(c => c.InTime);
                                    foreach (Shift todayShift in todayList)
                                    {
                                        if (distance > Math.Abs((todayShift.InTime.TimeOfDay - operationDate.TimeOfDay).TotalMinutes))
                                        {
                                            if ((todayShift.OutTime.TimeOfDay - todayShift.InTime.TimeOfDay).TotalMinutes > 0)
                                            {
                                                if (operationDate.TimeOfDay < todayShift.OutTime.TimeOfDay)
                                                {
                                                    distance = Convert.ToInt32(Math.Abs((todayShift.InTime.TimeOfDay - operationDate.TimeOfDay).TotalMinutes));
                                                    shift = todayShift;
                                                }
                                            }
                                            else
                                            {
                                                if (operationDate.AddHours(-12).TimeOfDay < todayShift.OutTime.AddHours(-12).TimeOfDay)
                                                {
                                                    distance = Convert.ToInt32(Math.Abs((todayShift.InTime.AddHours(-12).TimeOfDay - operationDate.AddHours(-12).TimeOfDay).TotalMinutes));
                                                    shift = todayShift;
                                                }
                                            }
                                        }
                                    }
                                }

                                if (shift.ID == 0)
                                {
                                    logMain = logMainList.OrderByDescending(c => c.InDate).ToList<LogMain>().First();
                                    if (logMain.IsClosed != false)
                                    {
                                        logMain.AttendanceDate = operationDate;
                                        logMain.InDate = operationDate;
                                        logMain.IsClosed = false;
                                        logMain.MissIN = 0;
                                        logMain.MissOut = 0;
                                        logMain.OutDate = null;
                                        logMain.PlusIN = 0;
                                        logMain.PlusOut = 0;
                                        logMain.ShiftID = 0;
                                        logMain.UserID = userID;
                                        actionState = new ActionState();
                                        logMainRepository.Insert(logMain, actionState);
                                        if (actionState.Status == ActionStatusEnum.NoError)
                                        {
                                            LogDetails logDetails = new LogDetails();
                                            logDetails.DeviceID = deviceID;
                                            logDetails.IsWrong = false;
                                            logDetails.LogMainID = logMain.ID;
                                            logDetails.OperationDate = operationDate;
                                            logDetails.OperationType = 1;

                                            LogDetailRepository logDetailRepository = new LogDetailRepository();
                                            logDetailRepository.Insert(logDetails, new ActionState());
                                        }
                                        else
                                        {
                                            LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_CnnotInsertUserLog, OperationSourceEnum.OracleDB);
                                        }
                                    }
                                    else
                                    {
                                        if (shift.ID != logMain.ShiftID)
                                        {
                                            logMain.AttendanceDate = operationDate;
                                            logMain.InDate = operationDate;
                                            logMain.IsClosed = false;
                                            logMain.MissIN = 0;
                                            logMain.MissOut = 0;
                                            logMain.OutDate = null;
                                            logMain.PlusIN = 0;
                                            logMain.PlusOut = 0;
                                            logMain.ShiftID = shift.ID;
                                            logMain.UserID = userID;
                                            actionState = new ActionState();
                                            logMainRepository.Insert(logMain, actionState);
                                            if (actionState.Status == ActionStatusEnum.NoError)
                                            {
                                                LogDetails logDetails = new LogDetails();
                                                logDetails.DeviceID = deviceID;
                                                logDetails.IsWrong = true;
                                                logDetails.LogMainID = logMain.ID;
                                                logDetails.OperationDate = operationDate;
                                                logDetails.OperationType = 1;

                                                LogDetailRepository logDetailRepository = new LogDetailRepository();
                                                logDetailRepository.Insert(logDetails, new ActionState());
                                            }
                                            else
                                            {
                                                LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_CnnotInsertUserLog, OperationSourceEnum.OracleDB);
                                            }
                                        }
                                        else
                                        {
                                            LogDetails logDetails = new LogDetails();
                                            logDetails.DeviceID = deviceID;
                                            logDetails.IsWrong = true;
                                            logDetails.LogMainID = logMain.ID;
                                            logDetails.OperationDate = operationDate;
                                            logDetails.OperationType = 1;

                                            LogDetailRepository logDetailRepository = new LogDetailRepository();
                                            logDetailRepository.Insert(logDetails, new ActionState());
                                        }
                                    }

                                }
                                else
                                {
                                    //if shift.ID != 0 and logMainList.Count!=0
                                    logMain = logMainList.OrderByDescending(c => c.InDate).ToList<LogMain>().First();
                                    if (logMain.IsClosed != false)
                                    {
                                        if (shift.ID != logMain.ShiftID)
                                        {
                                            logMain = ShiftCalculations(shift, operationDate, operationType);
                                            logMain.AttendanceDate = operationDate;
                                            logMain.InDate = operationDate;
                                            logMain.IsClosed = false;
                                            logMain.MissOut = 0;
                                            logMain.OutDate = null;
                                            logMain.PlusOut = 0;
                                            logMain.ShiftID = shift.ID;
                                            logMain.UserID = userID;
                                            actionState = new ActionState();
                                            logMainRepository.Insert(logMain, actionState);
                                            if (actionState.Status == ActionStatusEnum.NoError)
                                            {
                                                LogDetails logDetails = new LogDetails();
                                                logDetails.DeviceID = deviceID;
                                                logDetails.IsWrong = false;
                                                logDetails.LogMainID = logMain.ID;
                                                logDetails.OperationDate = operationDate;
                                                logDetails.OperationType = 1;

                                                LogDetailRepository logDetailRepository = new LogDetailRepository();
                                                logDetailRepository.Insert(logDetails, new ActionState());
                                            }
                                            else
                                            {
                                                LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_CnnotInsertUserLog, OperationSourceEnum.OracleDB);
                                            }
                                        }
                                        else
                                        {
                                            LogDetails logDetails = new LogDetails();
                                            logDetails.DeviceID = deviceID;
                                            logDetails.IsWrong = false;
                                            logDetails.LogMainID = logMain.ID;
                                            logDetails.OperationDate = operationDate;
                                            logDetails.OperationType = 1;

                                            LogDetailRepository logDetailRepository = new LogDetailRepository();
                                            logDetailRepository.Insert(logDetails, new ActionState());
                                        }
                                    }
                                    else
                                    {
                                        if (logMain.ShiftID != shift.ID)
                                        {
                                            logMain = ShiftCalculations(shift, operationDate, operationType);
                                            logMain.AttendanceDate = operationDate;
                                            logMain.InDate = operationDate;
                                            logMain.IsClosed = false;
                                            logMain.MissOut = 0;
                                            logMain.OutDate = null;
                                            logMain.PlusOut = 0;
                                            logMain.ShiftID = shift.ID;
                                            logMain.UserID = userID;
                                            actionState = new ActionState();
                                            logMainRepository.Insert(logMain, actionState);
                                            if (actionState.Status == ActionStatusEnum.NoError)
                                            {
                                                LogDetails logDetails = new LogDetails();
                                                logDetails.DeviceID = deviceID;
                                                logDetails.IsWrong = true;
                                                logDetails.LogMainID = logMain.ID;
                                                logDetails.OperationDate = operationDate;
                                                logDetails.OperationType = 1;

                                                LogDetailRepository logDetailRepository = new LogDetailRepository();
                                                logDetailRepository.Insert(logDetails, new ActionState());
                                            }
                                            else
                                            {
                                                LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_CnnotInsertUserLog, OperationSourceEnum.OracleDB);
                                            }
                                        }
                                        else
                                        {
                                            LogDetails logDetails = new LogDetails();
                                            logDetails.DeviceID = deviceID;
                                            logDetails.IsWrong = true;
                                            logDetails.LogMainID = logMain.ID;
                                            logDetails.OperationDate = operationDate;
                                            logDetails.OperationType = 1;

                                            LogDetailRepository logDetailRepository = new LogDetailRepository();
                                            logDetailRepository.Insert(logDetails, new ActionState());
                                        }
                                    }
                                }
                            }
                            else
                            {
                                /////////////////////////////////////
                            }
                        }
                    }
                    else
                    {
                        LogLostUser(userID, deviceID, operationType, operationDate, CommonConstants.Err_ProblemInGetLogMain, OperationSourceEnum.OracleDB);
                    }


                }


            }


        }

        public LogMain ShiftCalculations(Shift userShift, DateTime operationDate, int operationType)
        {
            LogMain logmain = new LogMain();

            int shiftDeference = (int)(userShift.OutTime.TimeOfDay - userShift.InTime.TimeOfDay).TotalMinutes;

            if (shiftDeference < 0)
            {
                userShift.OutTime = userShift.OutTime.AddHours(-12);
                userShift.InTime = userShift.InTime.AddHours(-12);
                operationDate = operationDate.AddHours(-12);
            }
            if (operationType == 1)
            {
                int inAllowTime = userShift.InAllowTime;
                int maxInTime = userShift.MaxAllowWorkTime;


                DateTime shiftMaxInDate = userShift.InTime.AddMinutes(-maxInTime);
                DateTime shiftMinInDate = userShift.InTime.AddMinutes(inAllowTime);


                int inLate = 0;
                int inEarly = 0;


                inLate = (int)(operationDate.TimeOfDay - shiftMinInDate.TimeOfDay).TotalMinutes;
                inEarly = (int)(shiftMaxInDate.TimeOfDay - operationDate.TimeOfDay).TotalMinutes;



                if (shiftDeference >= 0)
                {
                    logmain.InDate = operationDate;
                    logmain.AttendanceDate = operationDate;
                }
                else
                {
                    logmain.InDate = operationDate.AddHours(12);
                    logmain.AttendanceDate = operationDate.AddHours(12);
                }
                logmain.IsClosed = false;
                logmain.MissIN = 0;
                logmain.PlusIN = 0;
                if (inLate > 0)
                    logmain.MissIN = inLate;
                if (inEarly > 0)
                    logmain.PlusIN = inEarly;
                logmain.ShiftID = userShift.ID;
            }
            else if (operationType == 3)
            {
                int outLate = 0;
                int outEarly = 0;

                int maxOutTime = userShift.MinAllowWorkTime;
                int outAllowTime = userShift.OutAllowTime;

                DateTime shiftMaxOutDate = userShift.OutTime.AddMinutes(maxOutTime);
                DateTime shiftMinOutDate = userShift.OutTime.AddMinutes(-outAllowTime);

                outEarly = (int)(shiftMinOutDate.TimeOfDay - operationDate.TimeOfDay).TotalMinutes;
                outLate = (int)(operationDate.TimeOfDay - shiftMaxOutDate.TimeOfDay).TotalMinutes;


                if (shiftDeference >= 0)
                {
                    logmain.OutDate = operationDate;
                    logmain.AttendanceDate = operationDate;
                }
                else
                {
                    logmain.OutDate = operationDate.AddHours(12);
                    logmain.AttendanceDate = operationDate.AddHours(12);
                }
                logmain.IsClosed = false;
                logmain.MissOut = 0;
                logmain.PlusOut = 0;
                logmain.PlusIN = 0;
                logmain.MissIN = 0;

                if (outEarly > 0)
                    logmain.MissOut = outEarly;
                if (outLate > 0)
                    logmain.PlusOut = outLate;
                logmain.ShiftID = userShift.ID;


            }
            return logmain;
        }

        private void LogLostUser(int userID, int deviceID, int loggingType, DateTime loggingDate, string errorName, OperationSourceEnum errorTarget)
        {
            LostUsersLog lostUserLog = new LostUsersLog();
            ActionState actionState = new ActionState();
            lostUserLog.Device = new Device();
            lostUserLog.Device.ID = deviceID;
            lostUserLog.LoggingDate = loggingDate;
            lostUserLog.LoggingType = (UserLogTypeEnum)loggingType;
            lostUserLog.UserID = userID;
            LostUsersLogRepository lostUsersLogRepository = new LostUsersLogRepository();
            lostUsersLogRepository.Insert(lostUserLog, actionState);
            if (actionState.Status != ActionStatusEnum.NoError)
            {
                XMLSerelizeDomain<LostUsersLog> lostUserLogLogXmlLogging = new XMLSerelizeDomain<LostUsersLog>();
                lostUserLogLogXmlLogging.SaveXMLFile(lostUserLog, XMLLoggingTypeEnum.LostUserLog);

                GeneralLog generalLog = new GeneralLog();
                generalLog.ErrorDate = DateTime.Now;
                generalLog.ErrorDescription = actionState.Result;
                generalLog.ErrorName = errorName;
                generalLog.ErrorTarget = errorTarget;
                generalLog.ErrorType = LogLevelEnum.Critical;
                GeneralLogRepository generalLogRepository = new GeneralLogRepository();
                actionState = new ActionState();
                generalLogRepository.Insert(generalLog, actionState);
                if (actionState.Status != ActionStatusEnum.NoError)
                {
                    XMLSerelizeDomain<GeneralLog> generalLogXmlLogging = new XMLSerelizeDomain<GeneralLog>();
                    generalLogXmlLogging.SaveXMLFile(generalLog, XMLLoggingTypeEnum.ErrorLogging);
                }

            }
        }

        private void LogErrors(string errorName, string errorDescription, OperationSourceEnum errorTarget, LogLevelEnum errorType)
        {
            GeneralLog generalLog = new GeneralLog();
            generalLog.ErrorDate = DateTime.Now;
            generalLog.ErrorDescription = errorDescription;
            generalLog.ErrorName = errorName;
            generalLog.ErrorTarget = errorTarget;
            generalLog.ErrorType = errorType;
            GeneralLogRepository generalLogRepository = new GeneralLogRepository();
            ActionState actionState = new ActionState();
            generalLogRepository.Insert(generalLog, actionState);
            if (actionState.Status != ActionStatusEnum.NoError)
            {
                XMLSerelizeDomain<GeneralLog> generalLogXmlLogging = new XMLSerelizeDomain<GeneralLog>();
                generalLogXmlLogging.SaveXMLFile(generalLog, XMLLoggingTypeEnum.ErrorLogging);
            }

        }

        private void LogUserLog(int userID, int deviceID, int loggingType, DateTime loggingDate, string errorName, OperationSourceEnum errorTarget)
        {
            UserLog userLog = new UserLog();
            ActionState actionState = new ActionState();
            userLog.DeviceID = new Device();
            userLog.DeviceID.ID = deviceID;
            userLog.LogingDateTime = loggingDate;
            userLog.LoggingType = (UserLogTypeEnum)loggingType;
            userLog.UserID = userID;
            UserLogRepository userLogRepository = new UserLogRepository();
            userLogRepository.Insert(userLog, actionState);
            if (actionState.Status != ActionStatusEnum.NoError)
            {
                XMLSerelizeDomain<UserLog> userLogXmlLogging = new XMLSerelizeDomain<UserLog>();
                userLogXmlLogging.SaveXMLFile(userLog, XMLLoggingTypeEnum.LostUserLog);

                LogLostUser(userID, deviceID, loggingType, loggingDate, CommonConstants.Err_CnnotInsertUserLog, OperationSourceEnum.InternalDB);

                GeneralLog generalLog = new GeneralLog();
                generalLog.ErrorDate = DateTime.Now;
                generalLog.ErrorDescription = actionState.Result;
                generalLog.ErrorName = errorName;
                generalLog.ErrorTarget = errorTarget;
                generalLog.ErrorType = LogLevelEnum.Critical;
                GeneralLogRepository generalLogRepository = new GeneralLogRepository();
                actionState = new ActionState();
                generalLogRepository.Insert(generalLog, actionState);
                if (actionState.Status != ActionStatusEnum.NoError)
                {
                    XMLSerelizeDomain<GeneralLog> generalLogXmlLogging = new XMLSerelizeDomain<GeneralLog>();
                    generalLogXmlLogging.SaveXMLFile(generalLog, XMLLoggingTypeEnum.ErrorLogging);
                }

            }
        }

        private int GetShiftID(List<Shift> shiftList, int loggingType, DateTime loggingDate, int userID, int deviceID, LogMain logMain)
        {
            if (loggingType == 1)
            {
                if (shiftList.Count == 1)
                {
                    if (shiftList[0].IsNormalShift == true)
                    {
                        int inAllowTime = shiftList[0].InAllowTime;
                        int maxInTime = shiftList[0].MaxAllowWorkTime;
                        int maxOutTime = shiftList[0].MinAllowWorkTime;
                        int outAllowTime = shiftList[0].OutAllowTime;

                        DateTime shiftMaxInDate = shiftList[0].InTime.AddMinutes(-maxInTime);
                        DateTime shiftMinInDate = shiftList[0].InTime.AddMinutes(inAllowTime);
                        DateTime shiftMaxOutDate = shiftList[0].OutTime.AddMinutes(maxOutTime);
                        DateTime shiftMinOutDate = shiftList[0].OutTime.AddMinutes(-outAllowTime);

                        if (loggingDate.TimeOfDay < shiftMaxInDate.TimeOfDay)
                        {
                            logMain.AttendanceDate = loggingDate.Date;
                            logMain.InDate = loggingDate;
                            logMain.IsClosed = false;
                            logMain.MissIN = 0;
                            logMain.MissOut = 0;
                            logMain.OutDate = null;
                            logMain.PlusIN = (loggingDate.TimeOfDay - shiftMaxInDate.TimeOfDay).Minutes;
                            logMain.PlusOut = 0;
                            logMain.ShiftID = shiftList[0].ID;
                            return 0;

                        }
                        if (loggingDate.TimeOfDay > shiftMinInDate.TimeOfDay)
                        {
                            logMain.AttendanceDate = loggingDate.Date;
                            logMain.InDate = loggingDate;
                            logMain.IsClosed = false;
                            logMain.MissIN = (loggingDate.TimeOfDay - shiftMinInDate.TimeOfDay).Minutes;
                            logMain.MissOut = 0;
                            logMain.OutDate = null;
                            logMain.PlusIN = 0;
                            logMain.PlusOut = 0;
                            logMain.ShiftID = shiftList[0].ID;
                            return 0;
                        }
                        else
                        {
                            logMain.AttendanceDate = loggingDate.Date;
                            logMain.InDate = loggingDate;
                            logMain.IsClosed = false;
                            logMain.MissIN = 0;
                            logMain.MissOut = 0;
                            logMain.OutDate = null;
                            logMain.PlusIN = 0;
                            logMain.PlusOut = 0;
                            logMain.ShiftID = shiftList[0].ID;
                            return 0;
                        }
                    }
                    else
                    {
                        LogLostUser(userID, deviceID, loggingType, loggingDate, CommonConstants.Err_ShiftsMustEqualToSeven, OperationSourceEnum.OracleDB);
                        return 0;
                    }

                }
                else
                {
                    if (shiftList.Count >= 7)
                    {
                        int dayID = loggingDate.Day;
                        var shiftListByDate = from z in shiftList where z.DayID == dayID select z;
                        shiftList = shiftListByDate.ToList<Shift>();
                        if (shiftList.Count != 0)
                        {
                            int inAllowTime = shiftList[0].InAllowTime;
                            int maxInTime = shiftList[0].MaxAllowWorkTime;
                            int outAllowTime = shiftList[0].OutAllowTime;
                            int maxOutTime = shiftList[0].MinAllowWorkTime;

                            DateTime shiftMaxInDate = shiftList[0].InTime.AddMinutes(-maxInTime);
                            DateTime shiftMinInDate = shiftList[0].InTime.AddMinutes(inAllowTime);
                            if (loggingDate.TimeOfDay < shiftMaxInDate.TimeOfDay)
                            {
                                logMain.AttendanceDate = loggingDate.Date;
                                logMain.InDate = loggingDate;
                                logMain.IsClosed = false;
                                logMain.MissIN = 0;
                                logMain.MissOut = 0;
                                logMain.OutDate = null;
                                logMain.PlusIN = (loggingDate.TimeOfDay - shiftMaxInDate.TimeOfDay).Minutes;
                                logMain.PlusOut = 0;
                                logMain.ShiftID = shiftList[0].ID;
                                return 0;

                            }
                            if (loggingDate.TimeOfDay > shiftMinInDate.TimeOfDay)
                            {
                                logMain.AttendanceDate = loggingDate.Date;
                                logMain.InDate = loggingDate;
                                logMain.IsClosed = false;
                                logMain.MissIN = (loggingDate.TimeOfDay - shiftMinInDate.TimeOfDay).Minutes;
                                logMain.MissOut = 0;
                                logMain.OutDate = null;
                                logMain.PlusIN = 0;
                                logMain.PlusOut = 0;
                                logMain.ShiftID = shiftList[0].ID;
                                return 0;
                            }
                            else
                            {
                                logMain.AttendanceDate = loggingDate.Date;
                                logMain.InDate = loggingDate;
                                logMain.IsClosed = false;
                                logMain.MissIN = 0;
                                logMain.MissOut = 0;
                                logMain.OutDate = null;
                                logMain.PlusIN = 0;
                                logMain.PlusOut = 0;
                                logMain.ShiftID = shiftList[0].ID;
                                return 0;
                            }

                        }
                        else
                        {
                            LogLostUser(userID, deviceID, loggingType, loggingDate, CommonConstants.Err_ShiftsMustEqualToSeven, OperationSourceEnum.OracleDB);
                            return 0;
                        }
                    }
                    else
                    {
                        LogLostUser(userID, deviceID, loggingType, loggingDate, CommonConstants.Err_ShiftsMustEqualToSeven, OperationSourceEnum.OracleDB);
                        return 0;
                    }
                }
            }
            else if (loggingType == 3)
            {

            }
            else
            {
                return -1;
            }

            return 0;
        }

    }
}
