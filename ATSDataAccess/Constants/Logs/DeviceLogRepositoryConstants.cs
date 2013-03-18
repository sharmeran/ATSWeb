using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.Constants.Logs;
using ATSDataAccess.Constants.Common;

namespace ATSDataAccess.Constants.Logs
{
    class DeviceLogRepositoryConstants
    {
        public const string Insert = "DeviceLogInsert";
        public const string Delete = "DeviceLogDelete";
        public const string Update = "DeviceLogUpdate";
        public const string FindByID = "DeviceLogFindByID";
        public const string FindAll = "DeviceLogFindAll";
        public const string FindByErrorType = "DeviceLogFindByErrorType";
        public const string FindByDeviceIDAndErrorTypeAndOperationID = "DeviceLogFindByDeviceIDAndDeviceErrorTypeAndOperationID";
        public const string DeviceLogGetByDate = "DeviceLogGetByDate";
        public const string DeviceLogFindByIP = "DeviceLogFindByIP";
        public const string DeviceLogFindByIPAndDate = "DeviceLogFindByIPAndDate";
        public const string DeviceLogFindByDeviceCategory = "DeviceLogFindByDeviceCategory";

        public const string ID = CommonConstants.SP_Parameter_Post + DeviceLogConstants.ID;
        public const string DeviceID = CommonConstants.SP_Parameter_Post + DeviceLogConstants.DeviceID;
        public const string DeviceLog = CommonConstants.SP_Parameter_Post + DeviceLogConstants.DeviceLog;
        public const string DeviceErrorCode = CommonConstants.SP_Parameter_Post + DeviceLogConstants.DeviceErrorCode;
        public const string DeviceErrorType = CommonConstants.SP_Parameter_Post + DeviceLogConstants.DeviceErrorType;
        public const string DeviceLoggingDate = CommonConstants.SP_Parameter_Post + DeviceLogConstants.DeviceLoggingDate;
        public const string OperationID = CommonConstants.SP_Parameter_Post + DeviceLogConstants.OperationID;
        public const string StartDate = "@StartDate";
        public const string EndDate = "@EndDate";
        public const string IP = "IP";
        public const string Name = "Name";
        public const string IPParameter = "@IP";
        public const string CategoryID = "@CategoryID";

    }
}
