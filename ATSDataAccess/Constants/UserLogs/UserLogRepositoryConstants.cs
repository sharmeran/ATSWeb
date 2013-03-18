using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.UserLogs;
using ATSDataAccess.Constants.Common;

namespace ATSDataAccess.Constants.UserLogs
{
    public static class UserLogRepositoryConstants
    {
        public const string Insert = "UserLogInsert";
        public const string Delete = "UserLogDelete";
        public const string Update = "UserLogUpdate";
        public const string FindByID = "DevicesFindByID";
        public const string FindAll = "UserLogFindAll";
        public const string UserLogFindByUserID = "UserLogFindByUserID";
        public const string UserLogFindByUserIDAndLoggingDate = "UserLogFindByUserIDAndLoggingDate";
        public const string UserLogFindByDate = "UserLogFindByDate";
        public const string UserLogFindCountByDate = "UserLogFindCountByDate";
        public const string UserLogIsExist = "UserLogIsExist";
        public const string UserLogByDeviceCategory = "UserLogByDeviceCategory";
        public const string UserLogFindByDeviceIP = "UserLogFindByDeviceIP";

        public const string ID = CommonConstants.SP_Parameter_Post + UserLogConstants.ID;
        public const string UserID = CommonConstants.SP_Parameter_Post + UserLogConstants.UserID;
        public const string LogingType = CommonConstants.SP_Parameter_Post + UserLogConstants.LogingType;
        public const string LogingDateTime = CommonConstants.SP_Parameter_Post + UserLogConstants.LogingDateTime;
        public const string DeviceID = CommonConstants.SP_Parameter_Post + UserLogConstants.DeviceID;
        public const string StartDateTime = "@StartDateTime";
        public const string EndDateTime = "@EndDateTime";
        public const string DeviceCategoryID = "@DeviceCategoryID";
        public const string IPParameter = "@IP";
        public const string Name = "Name";
        public const string IP = "IP";



    }
}
