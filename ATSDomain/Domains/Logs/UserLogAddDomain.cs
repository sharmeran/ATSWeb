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
using ATSDataAccess.SQLImlementation.Logs;
using ATSDataAccess.SQLImlementation.Shifts;
using ATSDataAccess.SQLImlementation.UserLogs;

namespace ATSDomain.Domains.Logs
{
    class UserLogAddDomain
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
            else if(loggingType == 3)
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
