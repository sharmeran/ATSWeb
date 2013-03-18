using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSCommon.BaseClasses;
using ATSCommon.Entites.Devices;
using ATSCommon.Enums;

namespace ATSCommon.Entites.Logs
{
    public class DeviceLog : BaseClass
    {
        Int64 iD;
        Device deviceID;
        string deviceLogEntry;
        string deviceErrorCode;
        LogLevelEnum deviceErrorType;
        DateTime deviceLoggingDate;
        OperationsEnum operationID;

        public OperationsEnum OperationID
        {
            get { return operationID; }
            set { operationID = value; }
        }

        public DateTime DeviceLoggingDate
        {
            get { return deviceLoggingDate; }
            set { deviceLoggingDate = value; }
        }

        public LogLevelEnum DeviceErrorType
        {
            get { return deviceErrorType; }
            set { deviceErrorType = value; }
        }

        public string DeviceErrorCode
        {
            get { return deviceErrorCode; }
            set { deviceErrorCode = value; }
        }

        public string DeviceLogEntry
        {
            get { return deviceLogEntry; }
            set { deviceLogEntry = value; }
        }

        public Device DeviceID
        {
            get { return deviceID; }
            set { deviceID = value; }
        }


        public Int64 ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
